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

        private DelegateCommand _openCategoryListView;
        public DelegateCommand OpenCategoryListView => _openCategoryListView ?? (_openCategoryListView = new DelegateCommand(OpenCategoryListViewAction));

        public void OpenCategoryListViewAction()
        {
            _shellService.ShowShell(nameof(CategoryListView));
        }

        private DelegateCommand _openBrandListView;
        public DelegateCommand OpenBrandListView => _openBrandListView ?? (_openBrandListView = new DelegateCommand(OpenBrandListViewAction));

        public void OpenBrandListViewAction()
        {
            _shellService.ShowShell(nameof(BrandListView));
        }

        private DelegateCommand _openUserListView;
        public DelegateCommand OpenUserListView => _openUserListView ?? (_openUserListView = new DelegateCommand(OpenUserListViewAction));

        public void OpenUserListViewAction()
        {
            _shellService.ShowShell(nameof(UserListView));
        }

        private DelegateCommand _openRequestListView;
        public DelegateCommand OpenRequestListView => _openRequestListView ?? (_openRequestListView = new DelegateCommand(OpenRequestListViewAction));

        public void OpenRequestListViewAction()
        {
            _shellService.ShowShell(nameof(RequestListView));
        }

        private DelegateCommand _openResponseListView;
        public DelegateCommand OpenResponseListView => _openResponseListView ?? (_openResponseListView = new DelegateCommand(OpenResponseListViewAction));

        public void OpenResponseListViewAction()
        {
            _shellService.ShowShell(nameof(ResponseListView));
        }


    }
}
