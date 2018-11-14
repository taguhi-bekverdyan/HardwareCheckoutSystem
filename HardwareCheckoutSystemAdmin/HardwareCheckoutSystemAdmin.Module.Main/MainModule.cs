using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Module.Main.Views;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Brands;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categories;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Devices;
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
            _regionManager.RegisterViewWithRegion(RegionNames.WindowContentRegion, typeof(MainWindowView));

            //register views
            _unityContainer.RegisterType(typeof(object), typeof(MainWindowView), nameof(MainWindowView));
            _unityContainer.RegisterType(typeof(object), typeof(DevicesView), nameof(DevicesView));
            _unityContainer.RegisterType(typeof(object), typeof(BrandsView), nameof(BrandsView));
            _unityContainer.RegisterType(typeof(object), typeof(AddBrandView), nameof(AddBrandView));
            _unityContainer.RegisterType(typeof(object), typeof(AddDeviceView), nameof(AddDeviceView));
            _unityContainer.RegisterType(typeof(object), typeof(AddCategoryView), nameof(AddCategoryView));
            _unityContainer.RegisterType(typeof(object), typeof(CategoriesView), nameof(CategoriesView));
            _unityContainer.RegisterType(typeof(object), typeof(AddUserView), nameof(AddUserView));
            _unityContainer.RegisterType(typeof(object), typeof(UsersView), nameof(UsersView));
        }
    }
}
