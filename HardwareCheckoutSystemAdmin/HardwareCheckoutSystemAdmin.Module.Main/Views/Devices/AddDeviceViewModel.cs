using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class AddDeviceViewModel : BindableBase, IRegionManagerAware, INavigationAware
  {
    #region Fields

    private readonly IDeviceService _devices;
    private readonly IBrandService _brands;
    private readonly ICategoryService _categories;
    private readonly IEventAggregator _eventAggreagator;
    private SaveMode _mode;

    #endregion

    #region Properties

    public IRegionManager RegionManager { get; set; }

    #endregion

    #region Commands

    public DelegateCommand SaveDeviceCommand => new DelegateCommand(SaveDeviceAction, CanSaveDevice);
    public DelegateCommand CancelDeviceCommand => new DelegateCommand(CancelDeviceAction);

    #endregion

    #region DependencyProperties

    private Device _device;
    public Device Device
    {
      get => _device;
      set => SetProperty(ref _device, value, () => { SelectedBrand = _device.Brand; SelectedCategory = _device.Category; });
    }

    private List<Brand> _brandList;
    public List<Brand> Brands
    {
      get => _brandList;
      set => SetProperty(ref _brandList, value);
    }

    private List<Category> _categoryList;
    public List<Category> Categories
    {
      get => _categoryList;
      set => SetProperty(ref _categoryList, value);
    }

    private Brand _selectedBrand;
    public Brand SelectedBrand
    {
      get => _selectedBrand;
      set
      {
        SetProperty(ref _selectedBrand, value, () => { Device.BrandId = _selectedBrand.Id; });
        RaisePropertyChanged("SaveDeviceCommand");
      }
    }

    private Category _selectedCategory;
    public Category SelectedCategory
    {
      get => _selectedCategory;
      set
      {
        SetProperty(ref _selectedCategory, value, () => { Device.CategoryId = _selectedCategory.Id; });
        RaisePropertyChanged("SaveDeviceCommand");
      }
    }

    #endregion

    #region Constructor

    public AddDeviceViewModel(IDeviceService devices, IBrandService brands, ICategoryService category, IEventAggregator eventAggregator)
    {
      _devices = devices;
      _brands = brands;
      _categories = category;
      _eventAggreagator = eventAggregator;

      SetCategories();
      SetBrands();
    }

    #endregion

    #region Methods

    private bool CanSaveDevice()
    {
      if (SelectedBrand != null && SelectedCategory != null) return true;
      return false;
    }

    private async void SetBrands()
    {
      Brands = await _brands.FindAll();
    }

    private async void SetCategories()
    {
      Categories = await _categories.FindAll();
    }

    #endregion

    #region Save action

    private async void SaveDeviceAction()
    {
      if (Device.Id == Guid.Empty) Device.Id = Guid.NewGuid();
      switch (_mode)
      {
        case SaveMode.Add:
          await _devices.Insert(Device);
          break;
        case SaveMode.Edit:
          Device.Brand = null;
          Device.Category = null;
          await _devices.Update(Device);
          break;
        default:
          break;
      }
      _eventAggreagator.GetEvent<UpdateDevicesEvent>().Publish(new UpdateDevicesEventArgs(Device));
    }

    #endregion

    #region Cancel action

    private void CancelDeviceAction()
    {
      _eventAggreagator.GetEvent<UpdateDevicesEvent>().Publish(null);
    }

    #endregion

    #region Navigation

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
      Device = navigationContext.Parameters["SelectedDevice"] as Device ?? new Device();
      _mode = (SaveMode)navigationContext.Parameters["Mode"];
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
      throw new NotImplementedException();
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
      throw new NotImplementedException();
    }

    #endregion    
  }
}
