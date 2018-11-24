﻿using HardwareCheckoutSystemAdmin.Common;
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
    private IShellService _service;
    public IRegionManager RegionManager { get ; set; }
    public DelegateCommand DeviceCommand => new DelegateCommand(OpenDevicesPageAction);
    public DelegateCommand BrandCommand => new DelegateCommand(OpenBrandsPageAction);
    public DelegateCommand CategoryCommand => new DelegateCommand(OpenCategorysPageAction);
    public DelegateCommand UserCommand => new DelegateCommand(OpenUsersPageAction);

    private void OpenDevicesPageAction()
  {
    RegionManager.RequestNavigate(RegionNames.WindowContentRegion, nameof(DevicesPageView));
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
