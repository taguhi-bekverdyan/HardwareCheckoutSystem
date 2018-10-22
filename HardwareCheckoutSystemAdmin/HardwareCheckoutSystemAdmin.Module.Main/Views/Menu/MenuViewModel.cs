using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Devices;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Menu
{
  public class MenuViewModel : BindableBase, IRegionManagerAware
  {
    //private readonly IShellService _service;
    public IRegionManager RegionManager { get ; set; }
    public DelegateCommand DeviceCommand => new DelegateCommand(OpenDevicesPageAction);

    private void OpenDevicesPageAction()
    {
      RegionManager.RequestNavigate(RegionNames.WindowContentRegion, nameof(DevicesPageView));
    }

    public MenuViewModel(IShellService service)
    {
      //_service = service;
    }
  }
}
