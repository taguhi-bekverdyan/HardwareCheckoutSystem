﻿using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Common.Prism;
using Prism.Events;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements
{
    class AddDeviceViewModel:BindableBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IShellService _shellService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IDeviceService _deviceService;


        private DeviceViewItem _device;
        public DeviceViewItem Device
        {
            get { return _device; }
            set { SetProperty(ref _device, value); }
        }

        private Mode _mode;

        private List<Brand> _brands;
        public List<Brand> Brands
        {
            get { return _brands; }
            set { SetProperty(ref _brands, value); }
        }
        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }


        private Brand _selectedBrand;
        public Brand SelectedBrand
        {
            get { return _selectedBrand; }
            set { SetProperty(ref _selectedBrand,value); }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }

        public AddDeviceViewModel(IBrandService brandService,
            ICategoryService categoryService,
            IDeviceService deviceService,
            IEventAggregator eventAggregator,
            IShellService shellService)
        {
            _brandService = brandService;
            _categoryService = categoryService;
            _deviceService = deviceService;
            _eventAggregator = eventAggregator;
            _shellService = shellService;
            AddDevice = new DelegateCommand(AddDeviceAction);
        }


        public DelegateCommand AddDevice { get; private set; }
        private async void AddDeviceAction()
        {
            Device newDevice = (Device)Device;
            newDevice.BrandId = await GetBrandGuid();
            newDevice.CategoryId = await GetCategoryGuid();
            if (_mode == Mode.Add)
            {
                await _deviceService.Insert(newDevice);
            }
            else if(_mode == Mode.Edit)
            {
                await _deviceService.Update(newDevice);
            }

            _eventAggregator.GetEvent<DeviceAddedOrEditedEvent>().Publish(new DeviceAddedOrEditedEventArgs{Device = newDevice});


            
        }

        private async Task<Guid> GetBrandGuid()
        {
            Brand b = await _brandService.FindBrandByName(SelectedBrand.Name);
            return b.Id;
        }

        private async Task<Guid> GetCategoryGuid()
        {
            Category c = await _categoryService.FindCategoryByName(SelectedCategory.Name);
            return c.Id;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetData();
            Parameter param = (Parameter)navigationContext.Parameters["request"];
            Device = param.Device;
            SelectedBrand = Device.Brand;
            SelectedCategory = Device.Category;
            _mode = param.Mode;
        }

        private async void GetData()
        {
            Brands = await _brandService.FindAll();
            Categories = await _categoryService.FindAll();
        }

    }

    public class DeviceAddedOrEditedEvent : PubSubEvent<DeviceAddedOrEditedEventArgs> { }

    public class DeviceAddedOrEditedEventArgs
    {
        public Device Device { get; set; }
    }
}
