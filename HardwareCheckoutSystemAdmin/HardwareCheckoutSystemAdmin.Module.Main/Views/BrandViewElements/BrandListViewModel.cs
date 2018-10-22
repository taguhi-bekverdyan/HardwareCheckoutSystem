using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.BrandViewElements
{
    class BrandListViewModel:BindableBase, INavigationAware
    {

        private IBrandService _brandService;


        private List<Brand> _brands = new List<Brand>();
        public List<Brand> Brands
        {
            get { return _brands; }
            set { SetProperty(ref _brands, value); }
        }

        private Brand _selectedBrand;
        public Brand SelectedBrand
        {
            get { return _selectedBrand; }
            set
            {
                SetProperty(ref _selectedBrand, value);
                DeleteBrand.RaiseCanExecuteChanged();
            }
        }


        public BrandListViewModel(IBrandService service)
        {
            _brandService = service;
            
            DeleteBrand = new DelegateCommand(DeleteBrandAction, CanUpdateOrDelete);
            AddBrand = new DelegateCommand(AddBrandAction);
            UpdateBrand = new DelegateCommand(UpdateBrandAction, CanUpdateOrDelete);
        }


        public DelegateCommand AddBrand { get; private set; }

        public void AddBrandAction()
        {           
            MessageBox.Show("TODO");
        }

        public DelegateCommand UpdateBrand { get; private set; }

        public void UpdateBrandAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand DeleteBrand { get; private set; }

        public void DeleteBrandAction()
        {
            MessageBox.Show("TODO");
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedBrand == null)
            {
                return false;
            }
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new System.NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        private async void UpdateData()
        {
            Brands = await _brandService.FindAll();
        }

    }
}
