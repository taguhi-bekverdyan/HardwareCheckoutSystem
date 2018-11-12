using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Brands;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categorys;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Devices;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Menu;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Users;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using IUnityContainer = Microsoft.Practices.Unity.IUnityContainer;


namespace HardwareCheckoutSystemAdmin.Module.Main
{
  public class MainModule : IModule
  {
    private readonly IRegionManager _regionManager;
    private readonly IUnityContainer _unityContainer;

    public MainModule(IRegionManager regionManager, IUnityContainer container)
    {
      _regionManager = regionManager;
      _unityContainer = container;
    }

    public void Initialize()
    {
      //register first view
      _regionManager.RegisterViewWithRegion(RegionNames.WindowContentRegion, typeof(MenuView));

      //register views
      _unityContainer.RegisterType(typeof(object), typeof(DeviceListView), nameof(DeviceListView));
     _unityContainer.RegisterType(typeof(object), typeof(BrandPageView), nameof(BrandPageView));
      _unityContainer.RegisterType(typeof(object), typeof(CategoryPageView), nameof(CategoryPageView));
      _unityContainer.RegisterType(typeof(object), typeof(UserPageView), nameof(UserPageView));
      _unityContainer.RegisterType(typeof(object), typeof(UserAddPageView), nameof(UserAddPageView));
      _unityContainer.RegisterType(typeof(object), typeof(AddDeviceView), nameof(AddDeviceView));
    }
  }
}
