using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HardwareCheckoutSystemAdmin
{
    public class ShellService : IShellService
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public ShellService(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }



        public void ShowShell(string uri, NavigationParameters navigationParameters)
        {
           var shell = _container.Resolve<ShellView>();
            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);
            scopedRegion.RequestNavigate(RegionNames.WindowContentRegion, uri, navigationParameters);
            shell.Show();
        }

        public ShellView ShowShell(string uri, NavigationParameters navigationParameters, int w, int h)
        {
            ShellView shell = _container.Resolve<ShellView>();
            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);
            scopedRegion.RequestNavigate(RegionNames.WindowContentRegion, uri, navigationParameters);
            shell.Width = w;
            shell.Height = h;
            shell.Show();
            return shell;
        }

        public void ShowShell(string uri, int w, int h)
        {
            var shell = _container.Resolve<ShellView>();
            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);
            scopedRegion.RequestNavigate(RegionNames.WindowContentRegion, uri);
            shell.Width = w;
            shell.Height = h;
            shell.Show();
        }

        public ShellView ShowShell(string uri)
        {
            ShellView shell = _container.Resolve<ShellView>();
            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);
            scopedRegion.RequestNavigate(RegionNames.WindowContentRegion, uri);
            shell.Show();
            return shell;
        }
    }
}
