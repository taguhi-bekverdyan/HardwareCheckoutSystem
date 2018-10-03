using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Common.Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views
{
    public class DeviceListViewModel : BindableBase, IRegionManagerAware
    {
        private readonly IShellService _service;
        public DeviceListViewModel(IShellService service)
        {
            _service = service;
        }


        private DelegateCommand _editDeviceCommand;
        public DelegateCommand EditDeviceCommand => _editDeviceCommand ?? (_editDeviceCommand = new DelegateCommand(EditDeviceAction));

        public void EditDeviceAction()
        {
            //include new user control in region
            //var parameters = new NavigationParameters { { "request", new PartsPickerRequest(vendorId.Value) } };
            var parameters = new NavigationParameters { { "request", 15 } };
            RegionManager.RequestNavigate(RegionNames.DocumentsRegion,nameof(EditDeviceView),parameters);
        }

        private DelegateCommand _editDeviceNewWindowCommand;
        public DelegateCommand EditDeviceNewWindowCommand => _editDeviceNewWindowCommand ?? (_editDeviceNewWindowCommand = new DelegateCommand(EditDeviceNewWindowAction));

        public void EditDeviceNewWindowAction()
        {
            var parameters = new NavigationParameters {{"request", 12}};
            _service.ShowShell(nameof(EditDeviceView),parameters);
        }



        public IRegionManager RegionManager { get; set; }
    }
}
