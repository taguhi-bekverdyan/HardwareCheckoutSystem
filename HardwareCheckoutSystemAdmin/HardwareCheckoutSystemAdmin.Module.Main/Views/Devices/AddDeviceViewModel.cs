using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class AddDeviceViewModel : BindableBase, IRegionManagerAware
  {
    private readonly IDeviceService _devices;
    public IRegionManager RegionManager { get; set; }
    public Device Device { get; set; }
    public DelegateCommand SaveDeviceCommand => new DelegateCommand(SaveDeviceAction);

    public AddDeviceViewModel(IDeviceService devices)
    {
      _devices = devices;
      Device = new Device();
    }

    private async void SaveDeviceAction()
    {
      await _devices.Insert(Device);
    }
  }
}
