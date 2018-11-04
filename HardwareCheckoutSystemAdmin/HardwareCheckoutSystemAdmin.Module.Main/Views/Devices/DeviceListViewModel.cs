using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data;
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

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class DeviceListViewModel : BindableBase, IRegionManagerAware
  {
    public IRegionManager RegionManager { get; set; }
    private readonly IDeviceService _devices;
    private readonly IShellService _service;
    public List<DeviceItem> DeviceItems { get; set; }
    public DelegateCommand AddDeviceCommand => new DelegateCommand(AddDeviceAction);    

    public DeviceListViewModel(IDeviceService devices, IShellService service)
    {
      _devices = devices;
      _service = service;
      DeviceItems = new List<DeviceItem>();
      LoadDevices();
    }

    private async void LoadDevices()
    {
      var devices = await _devices.FindAll();
      DeviceItems.AddRange(devices.Select(d => new DeviceItem(d)));
    }

    private void AddDeviceAction()
    {
      _service.ShowShell(nameof(AddDeviceView));
    }
  }
}
