using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.BrandViewElements
{
    class BrandListViewModel:BindableBase, INavigationAware
    {

        private IBrandService _brandService;


        private List<BrandViewItem> _brands = new List<BrandViewItem>();
        public List<BrandViewItem> Brands
        {
            get { return _brands; }
            set { SetProperty(ref _brands, value); }
        }

        private BrandViewItem _selectedBrand;
        public BrandViewItem SelectedBrand
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

        private async void UpdateData()
        {
            Brands = new List<BrandViewItem>();
            List<Brand> temp = await _brandService.FindAll();
            foreach (var item in temp)
            {
                Brands.Add(new BrandViewItem(item));
            }
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

        public async void DeleteBrandAction()
        {
            await _brandService.DeleteBrandById(SelectedBrand.GetId());
            
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
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
