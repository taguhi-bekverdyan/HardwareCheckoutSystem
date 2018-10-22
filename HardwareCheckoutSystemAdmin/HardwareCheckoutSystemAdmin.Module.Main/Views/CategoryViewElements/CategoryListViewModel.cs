using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.CategoryViewElements
{
    class CategoryListViewModel:BindableBase, INavigationAware
    {
        private ICategoryService _categoryService;


        private List<Category> _categories = new List<Category>();
        public List<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
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

        public void AddCategoryAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand UpdateCategory { get; private set; }

        public void UpdateCategoryAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand DeleteCategory { get; private set; }

        public void DeleteCategoryAction()
        {
            MessageBox.Show("TODO");
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedCategory == null)
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
            throw new System.NotImplementedException();
        }

        private async void UpdateData()
        {
            Categories = await _categoryService.FindAll();
        }

    }
}
