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

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements
{
    class DeviceListViewModel:BindableBase, INavigationAware
    {

        private readonly IEventAggregator _eventAggregator;
        private readonly IDeviceService _deviceService;
        private readonly IShellService _shellService;

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
            set
            {
                SetProperty(ref _selectedDevice, value);
                DeleteDevice.RaiseCanExecuteChanged();
                UpdateDevice.RaiseCanExecuteChanged();
            }
        }

        public DeviceListViewModel(IDeviceService deviceService,IShellService shellService, IEventAggregator eventAggregator)
        {
            _deviceService = deviceService;
            _shellService = shellService;
            _eventAggregator = eventAggregator;
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


            _eventAggregator.GetEvent<DeviceAddedOrEditedEvent>().Subscribe(DeviceAddedEventHandler, ThreadOption.UIThread);
            _shellService.ShowShell(nameof(AddDeviceView), 450, 450,param);


            //todo change
            SelectedDevice = null;
        }

        private async void DeviceAddedEventHandler(DeviceAddedOrEditedEventArgs args)
        {
            if (args.Device != null)
            {
                await UpdateData();
            }
            _eventAggregator.GetEvent<DeviceAddedOrEditedEvent>().Unsubscribe(DeviceAddedEventHandler);
        }



        public DelegateCommand UpdateDevice { get; private set; }

        public void UpdateDeviceAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand DeleteDevice { get; private set; }
            
        public async void DeleteDeviceAction()
        {
            await _deviceService.DeleteDeviceById(SelectedDevice.GetId());
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedDevice == null)
            {
                return false;
            }
            return true;
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await UpdateData();
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
            Devices = new List<DeviceViewItem>();
            var temp = await _deviceService.FindAll();
            foreach (var item in temp)
            {
                Devices.Add(new DeviceViewItem(item));
            }
        }
    }
}
