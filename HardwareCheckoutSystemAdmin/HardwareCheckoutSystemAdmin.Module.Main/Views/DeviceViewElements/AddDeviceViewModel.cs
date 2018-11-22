using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Events;
using HardwareCheckoutSystemAdmin.Common.Prism;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements
{
    class AddDeviceViewModel:BindableBase, INavigationAware
    {
        #region Private fields

        
        private readonly IEventAggregator _eventAggregator;
        private readonly IShellService _shellService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IDeviceService _deviceService;


        private Mode _mode;
        private Guid _selectedDeviceId;

        #endregion

        #region Props



        private string _serialNumber;
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                SetProperty(ref _serialNumber,value);
                AddDevice.RaiseCanExecuteChanged();
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                SetProperty(ref _description, value);
                AddDevice.RaiseCanExecuteChanged();
            }
        }

        private string _model;
        public string Model
        {
            get
            {
                return _model;
            }
            set
            {
                SetProperty(ref _model, value);
                AddDevice.RaiseCanExecuteChanged();
            }
        }

        private Permission _permission;
        public Permission Permission
        {
            get { return _permission; }
            set
            {
                SetProperty(ref _permission,value);
                AddDevice.RaiseCanExecuteChanged();
            }
        }

        private List<Brand> _brands;
        public List<Brand> Brands
        {
            get { return _brands; }
            set { SetProperty(ref _brands, value); }
        }
        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }


        private Brand _selectedBrand;
        public Brand SelectedBrand
        {
            get { return _selectedBrand; }
            set
            {
                SetProperty(ref _selectedBrand,value);
                AddDevice.RaiseCanExecuteChanged();
            }
        }

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

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                SetProperty(ref _imagePath, value);
                AddDevice.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Ctors

        
        public AddDeviceViewModel(IBrandService brandService,
            ICategoryService categoryService,
            IDeviceService deviceService,
            IEventAggregator eventAggregator,
            IShellService shellService)
        {
            _brandService = brandService;
            _categoryService = categoryService;
            _deviceService = deviceService;
            _eventAggregator = eventAggregator;
            _shellService = shellService;
            AddDevice = new DelegateCommand(AddDeviceAction,CanAdd);
            Cancel = new DelegateCommand(CancelAction);
            ChooseImage = new DelegateCommand(ChooseImageAction);
        }
        #endregion

        #region Commands

       
        public DelegateCommand AddDevice { get; private set; }
        private async void AddDeviceAction()
        {
            Device device = new Device();
            if (_mode == Mode.Add)
            {
                device.Id = Guid.NewGuid();
                device.BrandId = SelectedBrand.Id;
                device.CategoryId = SelectedCategory.Id;
                device.Model = Model;
                device.Description = Description;
                device.Permission = Permission;
                device.SerialNumber = SerialNumber;
                device.Image = GetBytesFromImage(ImagePath);
                await _deviceService.Insert(device);
            }
            else if(_mode == Mode.Edit)
            {
                device.Id = _selectedDeviceId;
                device.BrandId = SelectedBrand.Id;
                device.CategoryId = SelectedCategory.Id;
                device.Model = Model;
                device.Description = Description;
                device.Permission = Permission;
                device.SerialNumber = SerialNumber;
                device.Image = GetBytesFromImage(ImagePath);
                await _deviceService.Update(device);
            }
            _eventAggregator.GetEvent<DeviceAddedOrEditedEvent>().Publish(new DeviceAddedOrEditedEventArgs{Device = device});           
        }

        public DelegateCommand Cancel { get; private set; }
        private void CancelAction()
        {
            _eventAggregator.GetEvent<DeviceAddedOrEditedEvent>().Publish(new DeviceAddedOrEditedEventArgs { Device = null });
        }

        public DelegateCommand ChooseImage { get; private set; }
        private void ChooseImageAction()
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            fileChooser.FilterIndex = 1;
            fileChooser.Multiselect = true;

            if (fileChooser.ShowDialog() == DialogResult.OK)
            {
                //System.Windows.MessageBox.Show(fileChooser.FileName);
                ImagePath = fileChooser.FileName;
            }

        }

        private bool CanAdd()
        {
            if (SelectedBrand != null && SelectedCategory != null &&
                SerialNumber.Length >= 15 && Model.Length != 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Navigation

        
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetData();
            DeviceParameter param = (DeviceParameter)navigationContext.Parameters["request"];
            DeviceViewItem device = param.Device;
            _mode = param.Mode;
            if (_mode.Equals(Mode.Add))
            {
                Description = string.Empty;
                Model = string.Empty;
                SerialNumber = string.Empty;
                ImagePath = string.Empty;
            }
            else
            {
                ImagePath = string.Empty;
                Description = device.Description;
                Model = device.Model;
                SerialNumber = device.SerialNumber;
                SelectedBrand = device.Brand;
                SelectedCategory = device.Category;
                _selectedDeviceId = device.GetId();
            }            
        }

        private async void GetData()
        {
            Brands = await _brandService.FindAll();
            Categories = await _categoryService.FindAll();
        }
        #endregion

        #region Helpers
        private byte[] GetBytesFromImage(string path)
        {
            if (path != string.Empty) {
                //return File.ReadAllBytes(path);
                Bitmap image = new Bitmap(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms,ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            return null;
        }
        #endregion

    }

    public class DeviceAddedOrEditedEvent : PubSubEvent<DeviceAddedOrEditedEventArgs> { }

    public class DeviceAddedOrEditedEventArgs
    {
        public Device Device { get; set; }
    }

}
