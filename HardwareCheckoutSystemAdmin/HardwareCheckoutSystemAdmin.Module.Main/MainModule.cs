using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Module.Main.Views;
using HardwareCheckoutSystemAdmin.Module.Main.Views.BrandViewElements;
using HardwareCheckoutSystemAdmin.Module.Main.Views.CategoryViewElements;
using HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements;
using HardwareCheckoutSystemAdmin.Module.Main.Views.RequestViewElements;
using HardwareCheckoutSystemAdmin.Module.Main.Views.ResponseViewElements;
using HardwareCheckoutSystemAdmin.Module.Main.Views.UserViewElements;
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
            _regionManager.RegisterViewWithRegion(RegionNames.WindowContentRegion, typeof(MainView));

            //register views
            _unityContainer.RegisterType(typeof(object), typeof(MainView), nameof(MainView));
            _unityContainer.RegisterType(typeof(object), typeof(EditDeviceView), nameof(EditDeviceView));
            _unityContainer.RegisterType(typeof(object),typeof(DeviceListView), nameof(DeviceListView));
            _unityContainer.RegisterType(typeof(object), typeof(CategoryListView), nameof(CategoryListView));
            _unityContainer.RegisterType(typeof(object), typeof(BrandListView), nameof(BrandListView));
            _unityContainer.RegisterType(typeof(object), typeof(UserListView), nameof(UserListView));
            _unityContainer.RegisterType(typeof(object), typeof(RequestListView), nameof(RequestListView));
            _unityContainer.RegisterType(typeof(object), typeof(ResponseListView), nameof(ResponseListView));
            _unityContainer.RegisterType(typeof(object), typeof(AddDeviceView), nameof(AddDeviceView));
            _unityContainer.RegisterType(typeof(object), typeof(AddUserView), nameof(AddUserView));
            _unityContainer.RegisterType(typeof(object), typeof(SendResponseView), nameof(SendResponseView));
        }
    }
}
