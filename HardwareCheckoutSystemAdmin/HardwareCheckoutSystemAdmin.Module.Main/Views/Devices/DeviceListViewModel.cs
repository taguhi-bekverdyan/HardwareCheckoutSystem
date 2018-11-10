﻿using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Common.Views;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class DeviceListViewModel : BindableBase, IRegionManagerAware
  {
    public IRegionManager RegionManager { get; set; }
    private readonly IDeviceService _devices;
    private readonly IShellService _shellService;
    private readonly IEventAggregator _eventAggregator;
    private ShellView _addDeviceView;

    private List<DeviceItem> _deviceItems;
    public List<DeviceItem> DeviceItems
    {
      get => _deviceItems;
      set => SetProperty(ref _deviceItems, value);
    }

    public DelegateCommand AddDeviceCommand => new DelegateCommand(AddDeviceAction);

    public DeviceListViewModel(IDeviceService devices, IShellService shellService, IEventAggregator eventAggregator)
    {
      _devices = devices;
      _shellService = shellService;
      _eventAggregator = eventAggregator;

      DeviceItems = new List<DeviceItem>();
      LoadDevices();
    }

    private async void LoadDevices()
    {
      var devices = await _devices.FindAll();
      DeviceItems.AddRange(devices.Select(d => new DeviceItem(d)));
    }

    private void UpdateDevices()
    {
      // To refresh the datagrid need to use the DeviceItems setter method
      //DeviceItems.Clear(); this method does not call the setter
      DeviceItems = new List<DeviceItem>();
      LoadDevices();
    }

    private void AddDeviceAction()
    {
      _addDeviceView = _shellService.ShowShell(nameof(AddDeviceView));
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Subscribe(AddDeviceEventHandler);
    }

    private void AddDeviceEventHandler(UpdateDevicesEventArgs args)
    {
      _addDeviceView.Close();

      if (args != null)
      {
        UpdateDevices();
      }
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Unsubscribe(AddDeviceEventHandler);
    }
  }
}
