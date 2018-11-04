using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class DeviceListViewModel : BindableBase, IRegionManagerAware
  {
    public IRegionManager RegionManager { get; set; }
    private readonly IDeviceService _devices;
    public List<DeviceItem> DeviceItems { get; set; }

    public DeviceListViewModel(IDeviceService devices)
    {
      _devices = devices;
      DeviceItems = new List<DeviceItem>();
      LoadDevices();
    }

    private async void LoadDevices()
    {
      var devices = await _devices.FindAll();
      DeviceItems.AddRange(devices.Select(d => new DeviceItem(d)));
    }
  }
}
