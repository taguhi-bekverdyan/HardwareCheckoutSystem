using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.HelperClasses;
using HardwareCheckoutSystemAdmin.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HardwareCheckoutSystemAdmin.Module.Main.Views.Brands.AddBrandViewModel;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Brands
{
    public class BrandsViewModel : BindableBase, INavigationAware
    {
        private readonly IShellService _ishellservice;
        private readonly IBrandService _ibrandservice;
        private readonly IEventAggregator _ieventaggregator;
        private ShellView addBrandView;

        public BrandsViewModel(IShellService shellservice, IBrandService brandservice, IEventAggregator eventaggregator)
        {
            _ishellservice = shellservice;
            _ibrandservice = brandservice;
            _ieventaggregator = eventaggregator;
            Brands = new List<Brand>();
            FindAll();
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
            throw new NotImplementedException();
        }

        private List<Brand> _brands;
        public List<Brand> Brands
        {
            get { return _brands; }

            set { SetProperty(ref _brands, value); }
        }

        private Brand _selectedbrand;
        public Brand SelectedBrand
        {
            get { return _selectedbrand; }

            set
            {
                SetProperty(ref _selectedbrand, value);
                EditBrandCommand.RaiseCanExecuteChanged();
                DeleteBrandCommand.RaiseCanExecuteChanged();
            }
        }

        public async void FindAll()
        {
            Brands = await _ibrandservice.FindAll();
        }

        private DelegateCommand _DeleteBrandCommand;
        public DelegateCommand DeleteBrandCommand => _DeleteBrandCommand ?? (_DeleteBrandCommand = new DelegateCommand(DeleteBrandAction));

        public async void DeleteBrandAction()
        {
            await _ibrandservice.Delete(SelectedBrand);
            FindAll();

        }

        private DelegateCommand _AddBrandCommand;
        public DelegateCommand AddBrandCommand => _AddBrandCommand ?? (_AddBrandCommand = new DelegateCommand(AddBrandAction));

        public void AddBrandAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<Brand>(ViewMode.Add,new Brand()) } };
            _ieventaggregator.GetEvent<BrandAddedOrEditedEvent>().Subscribe(BrandAddedOrEditedEventHandler, ThreadOption.UIThread);
            addBrandView = _ishellservice.ShowShell(nameof(AddBrandView), param, 280, 200);
        }

        private DelegateCommand _EditBrandCommand;
        public DelegateCommand EditBrandCommand => _EditBrandCommand ?? (_EditBrandCommand = new DelegateCommand(AddBrandAction));

        public void EditBrandAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request",new Param<Brand>(ViewMode.Edit,SelectedBrand) } };
            _ieventaggregator.GetEvent<BrandAddedOrEditedEvent>().Subscribe(BrandAddedOrEditedEventHandler, ThreadOption.UIThread);
            addBrandView = _ishellservice.ShowShell(nameof(AddBrandView), param, 280, 200);

        }


        private void BrandAddedOrEditedEventHandler(BrandAddedOrEditedEventArgs args)
        {
            addBrandView.Close();
            FindAll();
            _ieventaggregator.GetEvent<BrandAddedOrEditedEvent>().Unsubscribe(BrandAddedOrEditedEventHandler);
        }
    }
}
