using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.HelperClasses;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Brands
{
    public class BrandsViewModel : BindableBase, INavigationAware
    {
        private readonly IShellService _ishellservice;
        private readonly IBrandService _ibrandservice;

        public BrandsViewModel(IShellService shellservice, IBrandService brandservice)
        {
            _ishellservice = shellservice;
            _ibrandservice = brandservice;
            Brands = new List<Brand>();
            FindAll();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedToAsync(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private List<Brand> _brands;
        public List<Brand> Brands
        {
            get { return _brands; }

            set { SetProperty(ref _brands, value); }
        }

        private Brand _selectedbrand;
        public Brand SelectedBrand
        {
            get { return _selectedbrand; }

            set
            {
                SetProperty(ref _selectedbrand, value);
                EditBrandCommand.RaiseCanExecuteChanged();
                DeleteBrandCommand.RaiseCanExecuteChanged();
            }
        }

        public async void FindAll()
        {
            Brands = await _ibrandservice.FindAll();
        }

        private DelegateCommand _DeleteBrandCommand;
        public DelegateCommand DeleteBrandCommand => _DeleteBrandCommand ?? (_DeleteBrandCommand = new DelegateCommand(DeleteBrandAction));

        public async void DeleteBrandAction()
        {
            await _ibrandservice.Delete(SelectedBrand);
            FindAll();

        }

        private DelegateCommand _AddBrandCommand;
        public DelegateCommand AddBrandCommand => _AddBrandCommand ?? (_AddBrandCommand = new DelegateCommand(AddBrandAction));

        public void AddBrandAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<Brand>(ViewMode.Add,new Brand()) } };
            _ishellservice.ShowShell(nameof(AddBrandView), param);
        }

        private DelegateCommand _EditBrandCommand;
        public DelegateCommand EditBrandCommand => _EditBrandCommand ?? (_EditBrandCommand = new DelegateCommand(AddBrandAction));

        public void EditBrandAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request",new Param<Brand>(ViewMode.Edit,SelectedBrand) } };
            _ishellservice.ShowShell(nameof(AddBrandView), param);

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
