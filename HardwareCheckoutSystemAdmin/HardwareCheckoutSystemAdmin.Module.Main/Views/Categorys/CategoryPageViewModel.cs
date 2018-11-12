﻿using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Categorys
{
    class CategoryPageViewModel : BindableBase, IRegionManagerAware
    {
        public IRegionManager RegionManager { get; set; }
        private readonly ICategoryService _categorys;
        public List<string> CategoryItems { get; set; }
        public ObservableCollection<string> CategoryNames { get; set; }
        public string CategoryName { get; set; }
        public string SelectedCategoryItem { get; set; }
        public DelegateCommand AddCommand => new DelegateCommand(AddCategoryPageAction);
        public DelegateCommand DelateCommand => new DelegateCommand(DelatCategoryPageAction);
        public CategoryPageViewModel(ICategoryService categorys)
        {
            _categorys = categorys;
            CategoryItems = new List<string>();
            CategoryNames = new ObservableCollection<string>();
            LoadCategorys();
        }

        private async void LoadCategorys()
        {
            var categorys = await _categorys.FindAll();
            CategoryNames.AddRange(categorys.Select(b => b.Name));
        }
        private async void AddCategoryPageAction()
        {
            Category b = new Category { Id = Guid.NewGuid(), Name = CategoryName };
            CategoryNames.Add(CategoryName);
            await _categorys.Insert(b);
        }
        private async void DelatCategoryPageAction()
        {

            var categorys = await _categorys.FindAll();
            foreach (var b in categorys)
            {
                if (b.Name == SelectedCategoryItem)
                {

                    await _categorys.Delete(b.Id);
                }
            }
            CategoryNames.Remove(SelectedCategoryItem);
        }
    }
}
