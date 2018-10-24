using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements
{
    class DeviceListViewModel:BindableBase, INavigationAware
    {

        private IDeviceService _deviceService;
        private IShellService _shellService;

        private List<DeviceViewItem> _devices;
        public List<DeviceViewItem> Devices
        {
            get { return _devices; }
            set { SetProperty(ref _devices,value); }
        }

        private DeviceViewItem _selectedDevice;
        public DeviceViewItem SelectedDevice
        {
            get { return _selectedDevice; }
            set { SetProperty(ref _selectedDevice, value); }
        }

        public DeviceListViewModel(IDeviceService deviceService,IShellService shellService)
        {
            _deviceService = deviceService;
            _shellService = shellService;
            DeleteDevice = new DelegateCommand(DeleteDeviceAction, CanUpdateOrDelete);
            AddDevice = new DelegateCommand(AddDeviceAction);
            UpdateDevice = new DelegateCommand(UpdateDeviceAction, CanUpdateOrDelete);
        }

        
        public DelegateCommand AddDevice { get; private set; }

        public void AddDeviceAction()
        {
            NavigationParameters param;
            if (SelectedDevice == null)
            {
                SelectedDevice = new DeviceViewItem();
                param = new NavigationParameters { { "request", new Parameter(Mode.Add,SelectedDevice) } };
            }
            else
            {
                param = new NavigationParameters { { "request", new Parameter(Mode.Edit,SelectedDevice) } };
            }
            
            _shellService.ShowShell(nameof(AddDeviceView), 450, 450,param);
        }

        public DelegateCommand UpdateDevice { get; private set; }

        public void UpdateDeviceAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand DeleteDevice { get; private set; }
            
        public void DeleteDeviceAction()
        {
            MessageBox.Show("TODO");
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedDevice == null)
            {
                return false;
            }
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private async void UpdateData()
        {
            Devices = new List<DeviceViewItem>();
            List<Device> temp = await _deviceService.FindAll();
            foreach (var item in temp)
            {
                Devices.Add(new DeviceViewItem(item));
            }
        }

    }
}
