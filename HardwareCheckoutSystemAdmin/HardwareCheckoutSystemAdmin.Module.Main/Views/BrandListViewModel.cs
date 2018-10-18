using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views
{
    class BrandListViewModel:BindableBase
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
            Brands = _brandService.FindAll().Result;
            DeleteBrand = new DelegateCommand(DeleteBrandAction, CanUpdateOrDelete);
            AddBrand = new DelegateCommand(AddBrandAction);
            UpdateBrand = new DelegateCommand(UpdateBrandAction, CanUpdateOrDelete);
        }


        public DelegateCommand AddBrand { get; private set; }

        public void AddBrandAction()
        {
            //Category cat = new Category() { Id = Guid.NewGuid(),Name = "Phone"};
            //_categoryService.Insert(cat);
            //Categories = _categoryService.FindAll().Result;
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
            _brandService.Delete(SelectedBrand);
            SelectedBrand = null;
            Brands = _brandService.FindAll().Result;
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedBrand == null)
            {
                return false;
            }
            return true;
        }

    }
}
