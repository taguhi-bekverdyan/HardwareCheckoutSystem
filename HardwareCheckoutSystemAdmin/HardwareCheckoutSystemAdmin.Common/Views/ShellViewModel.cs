using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Common.Prism;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;

namespace HardwareCheckoutSystemAdmin.Views
{


    public class ShellViewModel : BindableBase, IRegionManagerAware
    {
        private readonly IShellService _service;


        private string _title = "HCS Admin Panel";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ShellViewModel(IShellService service)
        {
            _service = service;
            //RegionManager.RequestNavigate(RegionNames.WindowContentRegion, nameof(DeviceListView));
        }

        public IRegionManager RegionManager { get; set; }
    }
}
