using HardwareCheckoutSystemAdmin.Common.Prism;
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
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Brands
{
  public class BrandPageViewModel : BindableBase, IRegionManagerAware
  {
    public IRegionManager RegionManager { get; set; }
    private readonly IBrandService _brands;
    public List<string> BrandItems { get; set; }
    public ObservableCollection<string> BrandNames { get; set; }
    public string BrandName { get; set; }
    public string SelectedBrandItem { get; set; }
    public DelegateCommand AddCommand => new DelegateCommand(AddBrandPageAction);
    public DelegateCommand DelateCommand => new DelegateCommand(DelatBrandPageAction);
    public BrandPageViewModel(IBrandService brands)
    {
      _brands = brands;
      BrandItems = new List<string>();
      BrandNames = new ObservableCollection<string>();
      LoadBrands();
    }

    private async void LoadBrands()
    {
      try
      {
        var brands = await _brands.FindAll();
        BrandNames.AddRange(brands.Select(b => b.Name));
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message);
      }
    }
    private async void AddBrandPageAction()
    {
      try
      {
        Brand b = new Brand { Id = Guid.NewGuid(), Name = BrandName };
      BrandNames.Add(BrandName);
      await _brands.Insert(b);
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message);
      }      
    }
    private async void DelatBrandPageAction()
    {
      try
      {
        var brands = await _brands.FindAll();
        foreach (var b in brands)
        {
          if (b.Name == SelectedBrandItem)
          {

            await _brands.Delete(b.Id);
          }
        }
        BrandNames.Remove(SelectedBrandItem);
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message);
      }
    }
  }
}
