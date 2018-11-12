using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Brands;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categorys;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Devices;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Users;
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
    private readonly IShellService _service;
    public IRegionManager RegionManager { get; set; }
    public DelegateCommand BrandCommand => new DelegateCommand(OpenBrandsPageAction);
    public DelegateCommand CategoryCommand => new DelegateCommand(OpenCategorysPageAction);
    public DelegateCommand UserCommand => new DelegateCommand(OpenUsersPageAction);
    public DelegateCommand DeviceCommand => new DelegateCommand(OpenDeviceListAction);

    private void OpenDeviceListAction()
    {
      // Show in the main window
      //RegionManager.RequestNavigate(RegionNames.WindowContentRegion, nameof(DeviceListView));

      // Show in a new window
      _service.ShowShell(nameof(DeviceListView));
    }

    private void OpenUsersPageAction()
  {
    _service.ShowShell(nameof(UserPageView));
  }

    private void OpenBrandsPageAction()
  {
     _service.ShowShell(nameof(BrandPageView));
  }
   private void OpenCategorysPageAction()
  {
    _service.ShowShell(nameof(CategoryPageView));
  }
   public MenuViewModel(IShellService service)
  {
    _service = service;
  }
  }
}
