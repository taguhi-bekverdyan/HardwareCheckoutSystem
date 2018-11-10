using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Brands;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categories;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Devices;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views
{
    public class MainWindowViewModel : BindableBase, IRegionManagerAware
    {

        public IRegionManager RegionManager { get; set; }
        private readonly IShellService _ishellservice;
        public MainWindowViewModel(IShellService service)
        {
            _ishellservice = service;
        }


        private DelegateCommand _DevicesCommand;
        public DelegateCommand DevicesCommand => _DevicesCommand ?? (_DevicesCommand = new DelegateCommand(DevicesAction));

        public void DevicesAction()
        {
            //include new user control in region
            //var parameters = new NavigationParameters { { "request", new PartsPickerRequest(vendorId.Value) } };

            _ishellservice.ShowShell(nameof(DevicesView));

        }

        private DelegateCommand _BrandsCommand;
        public DelegateCommand BrandsCommand => _BrandsCommand ?? (_BrandsCommand = new DelegateCommand(BrandsAction));

        public void BrandsAction()
        {
            _ishellservice.ShowShell(nameof(BrandsView));
        }

        private DelegateCommand _CategoriesCommand;
        public DelegateCommand CategoriesCommand => _CategoriesCommand ?? (_CategoriesCommand = new DelegateCommand(CategoriesAction));

        public void CategoriesAction()
        {
            _ishellservice.ShowShell(nameof(CategoriesView));
        }

    }
}

