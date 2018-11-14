using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Data.Services;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Brands;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categories;
using HardwareCheckoutSystemAdmin.Module.Main.Views.HelperClasses;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public DevicesViewModel(IEventAggregator eventaggregator, IDeviceService deviceservice, IShellService shellservice, IBrandService brandservice, ICategoryService categoryService)
        {
            _ideviceservice = deviceservice;
            _ieventaggregator = eventaggregator;
            _ishellservice = shellservice;
            _ibrandservice = brandservice;
            _icategoryservice = categoryService;
            UpdateDevicesData();
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

        #endregion

        #region [BUTTONS]
        private DelegateCommand _EditDeviceCommand;
        public DelegateCommand EditDeviceCommand => _EditDeviceCommand ?? (_EditDeviceCommand = new DelegateCommand(EditDeviceAction));

        public void EditDeviceAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<DeviceViewItem>(ViewMode.Edit, SelectedItem) } };
            _ishellservice.ShowShell(nameof(AddDeviceView), param, 450, 450);
            _ieventaggregator.GetEvent<DeviceAddedOrEditedEvent>().Subscribe(DeviceAddedOrEditedEventHandler, ThreadOption.UIThread);

        }

        private DelegateCommand _AddDeviceCommand;
        public DelegateCommand AddDeviceCommand => _AddDeviceCommand ?? (_AddDeviceCommand = new DelegateCommand(AddDeviceAction));

        public void AddDeviceAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<DeviceViewItem>(ViewMode.Add, new DeviceViewItem()) } };
            _ieventaggregator.GetEvent<DeviceAddedOrEditedEvent>().Subscribe(DeviceAddedOrEditedEventHandler, ThreadOption.UIThread);
            _ishellservice.ShowShell(nameof(AddDeviceView), param, 450,450);
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
            //_ishellservice.CloseShell(nameof(MainWindowView));
        }

        private DelegateCommand _DeleteDeviceCommand;
        public DelegateCommand DeleteDeviceCommand => _DeleteDeviceCommand ?? (_DeleteDeviceCommand = new DelegateCommand(DeleteDeviceAction));

        public void DeleteDeviceAction()
        {
            _ideviceservice.DeleteBySerialNumber(SelectedItem.SerialNumber);
            UpdateDevicesData();
        }

        #endregion

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateDevicesData();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        private async void UpdateDevicesData()
        {
            Devices = new List<DeviceViewItem>();
            var temp = await _ideviceservice.FindAll();
            foreach (var i in temp)
            {
                Devices.Add(new DeviceViewItem(i));
            }
        }

        private void DeviceAddedOrEditedEventHandler(DeviceAddedOrEditedEventArgs args)
        {
            UpdateDevicesData();
            _ieventaggregator.GetEvent<DeviceAddedOrEditedEvent>().Unsubscribe(DeviceAddedOrEditedEventHandler);
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
