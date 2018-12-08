using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Prism;
using Prism.Events;
using HardwareCheckoutSystemAdmin.Views;
using HardwareCheckoutSystemAdmin.Module.Main.Views.CategoryViewElements;
using HardwareCheckoutSystemAdmin.Module.Main.Views.BrandViewElements;
using HardwareCheckoutSystemAdmin.Models;
using System.Windows.Media.Imaging;
using System.IO;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements
{
    class DeviceListViewModel:BindableBase, INavigationAware
    {

        private const int Width = 450;
        private const int Height = 420;

        private readonly IEventAggregator _eventAggregator;
        private readonly IDeviceService _deviceService;
        private readonly IShellService _shellService;
        private readonly ICategoryService _categoryService;

        private ShellView _addDeviceView;


        public DeviceListViewModel()
        {
        
        }

        private List<DeviceViewItem> _devices;
        public List<DeviceViewItem> Devices
        {
            get { return _devices; }
            set { SetProperty(ref _devices,value); }
        }
        ////
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                SetProperty(ref _selectedCategory, value);
                AddDevice.RaiseCanExecuteChanged();
            }
        }

        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }
        ////
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private DeviceViewItem _selectedDevice;
        public DeviceViewItem SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                SetProperty(ref _selectedDevice, value);
                DeleteDevice.RaiseCanExecuteChanged();
                UpdateDevice.RaiseCanExecuteChanged();
            }
        }

        public DeviceListViewModel(IDeviceService deviceService,IShellService shellService
            , IEventAggregator eventAggregator,ICategoryService categoryService)
        {
            _deviceService = deviceService;
            _categoryService = categoryService;
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _isBusy = true;

            DeleteDevice = new DelegateCommand(DeleteDeviceAction, CanUpdateOrDelete);
            AddDevice = new DelegateCommand(AddDeviceAction);
            UpdateDevice = new DelegateCommand(UpdateDeviceAction, CanUpdateOrDelete);
            OpenCategories = new DelegateCommand(OpenCategoriesAction);
            OpenBrands = new DelegateCommand(OpenBrandsAction);
            _addDeviceView = null;
        }

        
        public DelegateCommand AddDevice { get; private set; }

        public void AddDeviceAction()
        {
            NavigationParameters param;          
            param = new NavigationParameters { { "request", new DeviceParameter(Mode.Add,null) } };
            
            _eventAggregator.GetEvent<DeviceAddedOrEditedEvent>().Subscribe(DeviceAddedEventHandler, ThreadOption.UIThread);
            _addDeviceView = _shellService.ShowShell(nameof(AddDeviceView), 450, 520,param);
            //todo change
            SelectedDevice = null;
        }

        private async void DeviceAddedEventHandler(DeviceAddedOrEditedEventArgs args)
        {
            _addDeviceView.Close();
            await UpdateData();            
            _eventAggregator.GetEvent<DeviceAddedOrEditedEvent>().Unsubscribe(DeviceAddedEventHandler);
        }



        public DelegateCommand UpdateDevice { get; private set; }

        private void UpdateDeviceAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new DeviceParameter(Mode.Edit, SelectedDevice) } };
            _eventAggregator.GetEvent<DeviceAddedOrEditedEvent>().Subscribe(DeviceAddedEventHandler, ThreadOption.UIThread);
            _addDeviceView = _shellService.ShowShell(nameof(AddDeviceView), 450, 520, param);
            SelectedDevice = null;
        }


        public async void SaveDeviceChanges(object deviceRowObject)
        {
            var deviceRow = deviceRowObject as DeviceViewItem;
            if (deviceRow != null)
            {
                //save
                var device = new Device();


                device.Id = deviceRow.GetId();
                device.BrandId = deviceRow.Brand.Id;
                device.CategoryId = deviceRow.Category.Id;
                device.Model = deviceRow.Model;
                device.Description = deviceRow.Description;
                device.Permission = deviceRow.Permission;
                device.SerialNumber = deviceRow.SerialNumber;
                device.Image = GetBytesFromBitmap(deviceRow.BitmapImage);

                //device.Id = Guid.NewGuid();
                //device.BrandId = SelectedBrand.Id;
                //device.CategoryId = SelectedCategory.Id;
                //device.Model = Model;
                //device.Description = Description;
                //device.Permission = Permission;
                //device.SerialNumber = SerialNumber;
                //device.Image = GetBytesFromImage(ImagePath);
                await _deviceService.Update(device);
            }

        }

        private byte[] GetBytesFromBitmap(BitmapSource bitmapImage)
        {
            byte[] res;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.QualityLevel = 100;
            // byte[] bit = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);
                res = stream.ToArray();
                stream.Close();
            }
            return res;
        }

        public DelegateCommand DeleteDevice { get; private set; }
            
        private async void DeleteDeviceAction()
        {
            await _deviceService.DeleteDeviceById(SelectedDevice.GetId());
            await UpdateData();
        }

        public DelegateCommand OpenCategories { get; private set; }

        private void OpenCategoriesAction()
        {
            _shellService.ShowShell(nameof(CategoryListView), Width, Height);
        }

        public DelegateCommand OpenBrands { get; private set; }

        private void OpenBrandsAction()
        {
            _shellService.ShowShell(nameof(BrandListView), Width, Height);
        }

        private bool CanUpdateOrDelete()
        {
            if (SelectedDevice == null)
            {
                return false;
            }
            return true;
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsBusy = true;
            try
            {
                await UpdateData();
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection error "+e.ToString());
            }            
            IsBusy = false;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateData()
        {
            Categories = await _categoryService.FindAll();
            List<DeviceViewItem> tempDevices = new List<DeviceViewItem>();
            List<Device> temp = await _deviceService.FindAll();
            foreach (var item in temp)
            {
                tempDevices.Add(new DeviceViewItem(item));
            }
            Devices = tempDevices;
        }
    }
}
