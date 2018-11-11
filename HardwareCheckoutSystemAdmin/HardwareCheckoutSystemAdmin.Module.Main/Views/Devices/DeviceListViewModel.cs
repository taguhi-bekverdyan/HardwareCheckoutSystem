using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Common;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Common.Views;
using System.Windows;
using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class DeviceListViewModel : BindableBase, IRegionManagerAware
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
    public IRegionManager RegionManager { get; set; }
    public Device SelectedDevice { get; set; }
    private ObservableCollection<Device> _deviceItems;
    public ObservableCollection<Device> Devices
    {
      get => _deviceItems;
      set => SetProperty(ref _deviceItems, value);
    }
    public DelegateCommand AddDeviceCommand => new DelegateCommand(AddDeviceAction);
    public DelegateCommand DeleteDeviceCommand => new DelegateCommand(DeleteDeviceAction);
    #endregion

    #region Methods
    private async void LoadDevices()
    {
      var devices = await _devices.FindAll();
      Devices.AddRange(devices);
    }

    private void UpdateDevices()
    {
      // To refresh the datagrid need to use the DeviceItems setter method
      //DeviceItems.Clear(); this method does not call the setter
      Devices = new ObservableCollection<Device>();
      LoadDevices();
    }

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
    }

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
