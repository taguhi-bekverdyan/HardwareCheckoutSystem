using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Data.Services;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Brands;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categories;
using HardwareCheckoutSystemAdmin.Module.Main.Views.HelperClasses;
using HardwareCheckoutSystemAdmin.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using static HardwareCheckoutSystemAdmin.Module.Main.Views.Devices.AddDeviceViewModel;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
    public class DevicesViewModel : BindableBase, INavigationAware
    {
        private readonly IDeviceService _ideviceservice;
        private readonly IBrandService _ibrandservice;
        private readonly ICategoryService _icategoryservice;
        private readonly IShellService _ishellservice;
        private readonly IEventAggregator _ieventaggregator;
        private ShellView addDeviceView;

        public DevicesViewModel(IEventAggregator eventaggregator, IDeviceService deviceservice, IShellService shellservice, IBrandService brandservice, ICategoryService categoryService)
        {
            _ideviceservice = deviceservice;
            _ieventaggregator = eventaggregator;
            _ishellservice = shellservice;
            _ibrandservice = brandservice;
            _icategoryservice = categoryService;
        }

        #region [TYPES]

        private List<DeviceViewItem> _devices;
        public List<DeviceViewItem> Devices
        {
            get { return _devices; }

            set { SetProperty(ref _devices, value); }
        }

        private DeviceViewItem _selecteddevice;
        public DeviceViewItem SelectedItem
        {
            get { return _selecteddevice; }

            set
            {
                SetProperty(ref _selecteddevice, value);
                DeleteDeviceCommand.RaiseCanExecuteChanged();
                EditDeviceCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isbusy;
        public bool IsBusy
        {
            get { return _isbusy; }

            set { SetProperty(ref _isbusy, value); }
        }

        #endregion

        #region BUTTONS
        private DelegateCommand _EditDeviceCommand;
        public DelegateCommand EditDeviceCommand => _EditDeviceCommand ?? (_EditDeviceCommand = new DelegateCommand(EditDeviceAction));

        public void EditDeviceAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<DeviceViewItem>(ViewMode.Edit, SelectedItem) } };
            addDeviceView = _ishellservice.ShowShell(nameof(AddDeviceView), param, 450, 450);
            _ieventaggregator.GetEvent<DeviceAddedOrEditedEvent>().Subscribe(DeviceAddedOrEditedEventHandlerAsync, ThreadOption.UIThread);

        }

        private DelegateCommand _AddDeviceCommand;
        public DelegateCommand AddDeviceCommand => _AddDeviceCommand ?? (_AddDeviceCommand = new DelegateCommand(AddDeviceAction));

        public void AddDeviceAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<DeviceViewItem>(ViewMode.Add, new DeviceViewItem()) } };
            _ieventaggregator.GetEvent<DeviceAddedOrEditedEvent>().Subscribe(DeviceAddedOrEditedEventHandlerAsync, ThreadOption.UIThread);
           addDeviceView = _ishellservice.ShowShell(nameof(AddDeviceView), param, 450,450);
        }
        private DelegateCommand _OpenBrandsCommand;
        public DelegateCommand OpenBrandsCommand => _OpenBrandsCommand ?? (_OpenBrandsCommand = new DelegateCommand(OpenBrandsAction));

        public void OpenBrandsAction()
        {
            _ishellservice.ShowShell(nameof(BrandsView),350,450);
        }

        private DelegateCommand _OpenCategoriesCommand;
        public DelegateCommand OpenCategoriesCommand => _OpenCategoriesCommand ?? (_OpenCategoriesCommand = new DelegateCommand(OpenBrandsAction));

        public void OpenCategoriesAction()
        {
            _ishellservice.ShowShell(nameof(CategoriesView), 350, 450);
        }

        private DelegateCommand _DeleteDeviceCommand;
        public DelegateCommand DeleteDeviceCommand => _DeleteDeviceCommand ?? (_DeleteDeviceCommand = new DelegateCommand(DeleteDeviceActionAsync));

        public async void DeleteDeviceActionAsync()
        {
            await _ideviceservice.DeleteBySerialNumber(SelectedItem.SerialNumber);
            await UpdateDevicesData();
        }

        #endregion

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsBusy = true;
            await UpdateDevicesData();
            IsBusy = false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private async Task  UpdateDevicesData()
        {
            List<DeviceViewItem> _Devices = new List<DeviceViewItem>();
            try
            {
                var temp= await _ideviceservice.FindAll();
                foreach (var item in temp)
            {
                _Devices.Add(new DeviceViewItem(item));
            }
            Devices = _Devices;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
        }

        

        private async void DeviceAddedOrEditedEventHandlerAsync(DeviceAddedOrEditedEventArgs args)
        {
            addDeviceView.Close();
            await UpdateDevicesData();
            _ieventaggregator.GetEvent<DeviceAddedOrEditedEvent>().Unsubscribe(DeviceAddedOrEditedEventHandlerAsync);
        }

       
    }
}
