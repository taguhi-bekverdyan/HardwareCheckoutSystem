using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.BrandViewElements
{
    class BrandListViewModel:BindableBase, INavigationAware
    {

        private IBrandService _brandService;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }


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

       
        public DelegateCommand AddBrand { get; private set; }

        private async void AddBrandAction()
        {
            
            Brand newbrand = new Brand();
            newbrand.Id = Guid.NewGuid();
            newbrand.Name = Name;
            await _brandService.Insert(newbrand);
            await UpdateData();
            Name = null;

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
            SelectedBrand = null;
            await UpdateData();

        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedBrand == null)
            {
                return false;
            }
            return true;
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsBusy = true;
            try
            {
                await UpdateData();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
           
            IsBusy = false;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateData()
        {
            List<BrandViewItem> brands = new List<BrandViewItem>();
            List<Brand> temp = await _brandService.FindAll();
            foreach (var item in temp)
            {
                brands.Add(new BrandViewItem(item));
            }
            Brands = brands;
        }
    }
}
