using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Brands;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categories;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Devices;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Users;
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

            _ishellservice.ShowShell(nameof(DevicesView),550,550);

        }

        private DelegateCommand _BrandsCommand;
        public DelegateCommand BrandsCommand => _BrandsCommand ?? (_BrandsCommand = new DelegateCommand(BrandsAction));

        public void BrandsAction()
        {
            _ishellservice.ShowShell(nameof(BrandsView),300,450);
        }

        private DelegateCommand _CategoriesCommand;
        public DelegateCommand CategoriesCommand => _CategoriesCommand ?? (_CategoriesCommand = new DelegateCommand(CategoriesAction));

        public void CategoriesAction()
        {
            _ishellservice.ShowShell(nameof(CategoriesView),300,450);
        }

        private DelegateCommand _UsersCommand;
        public DelegateCommand UsersCommand => _UsersCommand ?? (_UsersCommand = new DelegateCommand(UsersAction));

        public void UsersAction()
        {
            _ishellservice.ShowShell(nameof(UsersView),550,550);
        }

        private DelegateCommand _ResponsesCommand;
        public DelegateCommand ResponsesCommand => _ResponsesCommand ?? (_ResponsesCommand = new DelegateCommand(ResponsesAction));

        public void ResponsesAction()
        {
            //_ishellservice.ShowShell(nameof(ResponsesView));
        }

        private DelegateCommand _RequestsCommand;
        public DelegateCommand RequestsCommand => _RequestsCommand ?? (_RequestsCommand = new DelegateCommand(RequestsAction));

        public void RequestsAction()
        {
            //_ishellservice.ShowShell(nameof(CategoriesView));
        }
    }
}

