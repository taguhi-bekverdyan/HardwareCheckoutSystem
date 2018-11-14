using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Categories
{
    public class CategoriesViewModel : BindableBase, INavigationAware
    {
        private readonly IShellService _ishellservice;
        private readonly ICategoryService _icategoryservice;

        public CategoriesViewModel(IShellService shellservice, ICategoryService categoryservice)
        {
            _ishellservice = shellservice;
            _icategoryservice = categoryservice;
            Categories = new List<Category>();
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

        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }

            set { SetProperty(ref _categories, value); }
        }

        private Category _selectedcategory;
        public Category SelectedCategory
        {
            get { return _selectedcategory; }

            set { SetProperty(ref _selectedcategory, value); }
        }

        public async void FindAll()
        {
            Categories = await _icategoryservice.FindAll();
        }

        private DelegateCommand _DeleteCategoryCommand;
        public DelegateCommand DeleteCategoryCommand => _DeleteCategoryCommand ?? (_DeleteCategoryCommand = new DelegateCommand(DeleteCategoryAction));

        public async void DeleteCategoryAction()
        {
            await _icategoryservice.Delete(SelectedCategory);
            FindAll();

        }

        private DelegateCommand _AddCategoryCommand;
        public DelegateCommand AddCategoryCommand => _AddCategoryCommand ?? (_AddCategoryCommand = new DelegateCommand(AddCategoryAction));

        public void AddCategoryAction()
        {
            _ishellservice.ShowShell(nameof(AddCategoryView),280,200);

        }

        private DelegateCommand _EditCategoryCommand;
        public DelegateCommand EditCategoryCommand => _EditCategoryCommand ?? (_EditCategoryCommand = new DelegateCommand(EditCategoryAction));

        public void EditCategoryAction()
        {
            NavigationParameters CategoryToEdit = new NavigationParameters { { "request_for_edit", SelectedCategory } };
            _ishellservice.ShowShell(nameof(AddCategoryView),280,200);

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
