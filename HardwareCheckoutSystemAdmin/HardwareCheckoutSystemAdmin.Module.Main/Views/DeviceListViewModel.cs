using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views
{
    class DeviceListViewModel:BindableBase
    {

        private IDeviceService _deviceService;

        private List<Device> _devices = new List<Device>();
        public List<Device> Devices
        {
            get { return _devices; }
            set { SetProperty(ref _devices,value); }
        }

        private Device _selectedDevice;
        public Device SelectedDevice
        {
            get { return _selectedDevice; }
            set { SetProperty(ref _selectedDevice, value); }
        }

        public DeviceListViewModel(IDeviceService deviceService)
        {
            _deviceService = deviceService;
            Devices = _deviceService.FindAll().Result;
            DeleteDevice = new DelegateCommand(DeleteDeviceAction, CanUpdateOrDelete);
            AddDevice = new DelegateCommand(AddDeviceAction);
            UpdateDevice = new DelegateCommand(UpdateDeviceAction, CanUpdateOrDelete);
        }

        
        public DelegateCommand AddDevice { get; private set; }

        public void AddDeviceAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand UpdateDevice { get; private set; }

        public void UpdateDeviceAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand DeleteDevice { get; private set; }
            
        public void DeleteDeviceAction()
        {
            _deviceService.Delete(SelectedDevice);
            SelectedDevice = null;
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedDevice == null)
            {
                return false;
            }
            return true;
        }
        

    }
}
