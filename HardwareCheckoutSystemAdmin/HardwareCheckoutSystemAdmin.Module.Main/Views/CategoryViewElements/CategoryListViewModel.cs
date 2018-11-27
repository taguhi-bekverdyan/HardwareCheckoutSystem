using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.CategoryViewElements
{
    class CategoryListViewModel:BindableBase, INavigationAware
    {
        private ICategoryService _categoryService;

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

        private List<CategoryViewItem> _categories = new List<CategoryViewItem>();
        public List<CategoryViewItem> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private CategoryViewItem _selectedCategory;
        public CategoryViewItem SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                SetProperty(ref _selectedCategory, value);
                DeleteCategory.RaiseCanExecuteChanged();
            }
        }


        public CategoryListViewModel(ICategoryService service)
        {
            _categoryService = service;
            
            DeleteCategory = new DelegateCommand(DeleteCategoryAction, CanUpdateOrDelete);
            AddCategory = new DelegateCommand(AddCategoryAction);
            UpdateCategory = new DelegateCommand(UpdateCategoryAction, CanUpdateOrDelete);
        }


        public DelegateCommand AddCategory { get; private set; }

        public async void AddCategoryAction()
        {
            Category newCategory = new Category();
            newCategory.Id = Guid.NewGuid();
            newCategory.Name = Name;
            await _categoryService.Insert(newCategory);
            await UpdateData();
            Name = null;
        }

        public DelegateCommand UpdateCategory { get; private set; }

        public void UpdateCategoryAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand DeleteCategory { get; private set; }

        public async void DeleteCategoryAction()
        {
            await _categoryService.DeleteCategoryById(SelectedCategory.GetId());
            SelectedCategory = null;
            await UpdateData();
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedCategory == null)
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
                MessageBox.Show(e.ToString());
            }
            
            IsBusy = false;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new System.NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new System.NotImplementedException();
        }

        private async Task UpdateData()
        {
            List<CategoryViewItem> categories = new List<CategoryViewItem>();
            List<Category> temp = await _categoryService.FindAll();
            foreach (var item in temp)
            {
                categories.Add(new CategoryViewItem(item));
            }
            Categories = categories;
        }

    }
}
