using HardwareCheckoutSystemAdmin.Common.Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views
{
    class MainViewModel : BindableBase
    {
        private IShellService _shellService;

        public MainViewModel(IShellService shellService)
        {
            _shellService = shellService;
        }

        private DelegateCommand _openDeviceListView;
        public DelegateCommand OpenDeviceListView => _openDeviceListView ?? (_openDeviceListView = new DelegateCommand(OpenDeviceListViewAction));

        public void OpenDeviceListViewAction()
        {
            _shellService.ShowShell(nameof(DeviceListView));
        }


    }
}
