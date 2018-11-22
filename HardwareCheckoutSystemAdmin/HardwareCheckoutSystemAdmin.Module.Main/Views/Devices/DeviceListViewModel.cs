using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using HardwareCheckoutSystemAdmin.Common.Views;
using System.Windows;
using System;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Brands;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categorys;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class DeviceListViewModel : BindableBase
  {
    #region Fields
    private readonly IDeviceService _devices;
    private readonly IShellService _shellService;
    private readonly IEventAggregator _eventAggregator;
    private ShellView _addDeviceView;
    //private ShellView _openBrandsView;
    //private ShellView _openCategoriesView;
    #endregion

    #region Constructor
    public DeviceListViewModel(IDeviceService devices, IShellService shellService, IEventAggregator eventAggregator)
    {
      _devices = devices;
      _shellService = shellService;
      _eventAggregator = eventAggregator;

      Devices = new ObservableCollection<Device>();
      LoadDevices();
    }
    #endregion

    #region DependencyProperties

    private Device _selectedDevice;
    public Device SelectedDevice
    {
      get => _selectedDevice;
      set => SetProperty(ref _selectedDevice, value);
    }

    private ObservableCollection<Device> _deviceItems;
    public ObservableCollection<Device> Devices
    {
      get => _deviceItems;
      set
      {
        SetProperty(ref _deviceItems, value);
      }
    }

    #endregion

    #region Commands

    public DelegateCommand AddDeviceCommand => new DelegateCommand(AddDeviceAction);
    public DelegateCommand EditDeviceCommand => new DelegateCommand(EditDeviceAction, CanEditOrDeleteDevice)
      .ObservesProperty(() => SelectedDevice);
    public DelegateCommand DeleteDeviceCommand => new DelegateCommand(DeleteDeviceAction, CanEditOrDeleteDevice)
      .ObservesProperty(() => SelectedDevice);
    public DelegateCommand OpenBrandsCommand => new DelegateCommand(OpenBrandsAction);
    public DelegateCommand OpenCategoriesCommand => new DelegateCommand(OpenCategoriesAction);

    #endregion

    #region Methods

    private bool CanEditOrDeleteDevice()
    {
      if (SelectedDevice != null) return true;
      return false;
    }

    /// <summary>
    /// Load all devices by rest request
    /// </summary>
    private async void LoadDevices()
    {
      try
      {
        var devices = await _devices.FindAll();
        Devices.AddRange(devices);
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message);
      }
    }

    /// <summary>
    /// Update device items to refresh datagrid view
    /// </summary>
    private void UpdateDevices()
    {
      // To refresh the datagrid need to use the DeviceItems setter method
      Devices = new ObservableCollection<Device>();
      LoadDevices();
    }
    #endregion

    #region Add Device

    private void AddDeviceAction()
    {
      var parameters = new NavigationParameters();
      parameters.Add("Mode", SaveMode.Add);
      _addDeviceView = _shellService.ShowShell(nameof(AddDeviceView), parameters);
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Subscribe(AddDeviceEventHandler);
    }

    private void AddDeviceEventHandler(UpdateDevicesEventArgs args)
    {
      _addDeviceView.Close();

      if (args != null)
      {
        UpdateDevices();
      }
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Unsubscribe(AddDeviceEventHandler);
      SelectedDevice = null;
    }

    #endregion

    #region Edit Device

    private void EditDeviceAction()
    {
      var parameters = new NavigationParameters();
      parameters.Add("SelectedDevice", SelectedDevice);
      parameters.Add("Mode", SaveMode.Edit);
      _addDeviceView = _shellService.ShowShell(nameof(AddDeviceView), parameters);
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Subscribe(EditDeviceEventHandler);
    }

    private void EditDeviceEventHandler(UpdateDevicesEventArgs args)
    {
      _addDeviceView.Close();
      UpdateDevices();
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Unsubscribe(EditDeviceEventHandler);
      SelectedDevice = null;
    }

    #endregion

    #region Delete Device

    private async void DeleteDeviceAction()
    {
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Subscribe(DeleteDeviceEventHandler);
      var dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo);

      if (dialogResult == MessageBoxResult.Yes)
      {
        try
        {
          await _devices.Delete(SelectedDevice.Id);
        }
        catch (Exception e)
        {
          MessageBox.Show(e.Message);
        }
        
        _eventAggregator.GetEvent<UpdateDevicesEvent>().Publish(new UpdateDevicesEventArgs(SelectedDevice));
      }
      else _eventAggregator.GetEvent<UpdateDevicesEvent>().Publish(null);
    }

    private void DeleteDeviceEventHandler(UpdateDevicesEventArgs args)
    {
      if (args != null)
      {
        UpdateDevices();
      }
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Unsubscribe(DeleteDeviceEventHandler);
      SelectedDevice = null;
    }

    #endregion

    #region OpenBrands

    private void OpenBrandsAction()
    {
      //_openBrandsView = _shellService.ShowShell(nameof(BrandPageView));
      _shellService.ShowShell(nameof(BrandPageView));
    }

    #endregion

    #region OpenCategories

    private void OpenCategoriesAction()
    {
      //_openCategoriesView = _shellService.ShowShell(nameof(CategoryPageView));
      _shellService.ShowShell(nameof(CategoryPageView));
    }

    #endregion
  }
}
