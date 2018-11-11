using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class AddDeviceViewModel : BindableBase, IRegionManagerAware
  {
    private readonly IDeviceService _devices;
    private readonly IBrandService _brands;
    private readonly ICategoryService _categories;
    private readonly IEventAggregator _eventAggreagator;

    public IRegionManager RegionManager { get; set; }
    public Device Device { get; set; }
    public Brand SelectedBrand { get; set; }
    public Category SelectedCategory { get; set; }
    public List<Brand> Brands { get; set; }
    public List<Category> Categories { get; set; }
    public DelegateCommand SaveDeviceCommand => new DelegateCommand(SaveDeviceAction);
    public DelegateCommand CancelDeviceCommand => new DelegateCommand(CancelDeviceAction);

    public AddDeviceViewModel(IDeviceService devices, IBrandService brands, ICategoryService category, IEventAggregator eventAggregator)
    {
      _devices = devices;
      _brands = brands;
      _categories = category;
      _eventAggreagator = eventAggregator;

      SetCategories();
      SetBrands();
      Device = new Device();
    }

    private async void SetBrands()
    {
      Brands = await _brands.FindAll();
    }

    private async void SetCategories()
    {
      Categories = await _categories.FindAll();
    }

    private async void SaveDeviceAction()
    {
      Device.Id = Guid.NewGuid();
      Device.BrandId = SelectedBrand.Id;
      Device.CategoryId = SelectedCategory.Id;
      await _devices.Insert(Device);
      _eventAggreagator.GetEvent<UpdateDevicesEvent>().Publish(new UpdateDevicesEventArgs(Device));
    }

    private void CancelDeviceAction()
    {
      _eventAggreagator.GetEvent<UpdateDevicesEvent>().Publish(null);
    }
  }  
}
