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

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class DeviceListViewModel : BindableBase
  {
    #region Fields
    private readonly IDeviceService _devices;
    private readonly IShellService _shellService;
    private readonly IEventAggregator _eventAggregator;
    private ShellView _addDeviceView;
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

    #region Properties

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
    public DelegateCommand AddDeviceCommand => new DelegateCommand(AddDeviceAction);
    public DelegateCommand EditDeviceCommand => new DelegateCommand(EditDeviceAction, CanEditOrDeleteDevice)
      .ObservesProperty(() => SelectedDevice);
    public DelegateCommand DeleteDeviceCommand => new DelegateCommand(DeleteDeviceAction, CanEditOrDeleteDevice)
      .ObservesProperty(() => SelectedDevice);

    #endregion

    #region Methods

    /// <summary>
    /// Load all devices from database
    /// </summary>
    private async void LoadDevices()
    {
      var devices = await _devices.FindAll();
      Devices.AddRange(devices);
    }

    /// <summary>
    /// Update device items to refresh datagrid view
    /// </summary>
    private void UpdateDevices()
    {
      // To refresh the datagrid need to use the DeviceItems setter method
      //DeviceItems.Clear(); this method does not call the setter
      Devices = new ObservableCollection<Device>();
      LoadDevices();
    }
    #endregion

    #region Add Device
    private void AddDeviceAction()
    {
      _addDeviceView = _shellService.ShowShell(nameof(AddDeviceView));
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
    private async void EditDeviceAction()
    {

    }
    #endregion

    #region Delete Device
    private async void DeleteDeviceAction()
    {
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Subscribe(DeleteDeviceEventHandler);
      var dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo);

      if (dialogResult == MessageBoxResult.Yes)
      {
        await _devices.Delete(SelectedDevice.Id);
        _eventAggregator.GetEvent<UpdateDevicesEvent>().Publish(new UpdateDevicesEventArgs(SelectedDevice));
      }
      _eventAggregator.GetEvent<UpdateDevicesEvent>().Unsubscribe(DeleteDeviceEventHandler);
      SelectedDevice = null;
    }

    private bool CanEditOrDeleteDevice()
    {
      if (SelectedDevice != null) return true;
      return false;
    }

    private void DeleteDeviceEventHandler(UpdateDevicesEventArgs args)
    {
      if (args != null)
      {
        UpdateDevices();
      }
    }
    #endregion    
  }
}
