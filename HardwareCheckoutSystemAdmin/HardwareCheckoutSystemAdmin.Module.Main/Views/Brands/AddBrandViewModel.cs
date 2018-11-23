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
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Brands
{
    public class AddBrandViewModel : BindableBase, INavigationAware
    {
        private readonly IShellService _ishellservice;
        private readonly IBrandService _ibrandservice;
        private readonly IEventAggregator _ieventaggregator;
        private ViewMode mode;
        private Brand brand;

        public AddBrandViewModel(IShellService shellservice, IBrandService brandservice , IEventAggregator eventaggregator)
        {
            _ishellservice = shellservice;
            _ibrandservice = brandservice;
            _ieventaggregator = eventaggregator;

        }

        #region TYPESANDNAVIGATION
        private string _name;
        public string Name
        {
            get { return _name; }

            set { SetProperty(ref _name, value); }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var param = (Param<Brand>)navigationContext.Parameters["request"];
            brand = param._ViewItem;
            mode = param._ViewMode;
            if (param._ViewMode.Equals(ViewMode.Edit))
            {
                Name = brand.Name;
            }
            else
            {
                Name = null;
            }
        }

        #endregion

        #region BUTTONS
        private DelegateCommand _AddBrandCommand;
        public DelegateCommand AddBrandCommand => _AddBrandCommand ?? (_AddBrandCommand = new DelegateCommand(AddBrandAction));

        public void AddBrandAction()
        {

            if (mode == ViewMode.Edit)
            {
                brand.Name = Name;
                _ibrandservice.Update(brand);
            }
            else
            {
                brand = new Brand(Name);
                _ibrandservice.Insert(brand);
            }

            _ieventaggregator.GetEvent<BrandAddedOrEditedEvent>().Publish(new BrandAddedOrEditedEventArgs { Brand = brand });
        }

        private DelegateCommand _CancelAddingBrandCommand;
        public DelegateCommand CancelAddingBrandCommand => _CancelAddingBrandCommand ?? (_CancelAddingBrandCommand = new DelegateCommand(CancelAddingBrandAction));

        public void CancelAddingBrandAction()
        {
            _ieventaggregator.GetEvent<BrandAddedOrEditedEvent>().Publish(new BrandAddedOrEditedEventArgs { Brand = null });
        }
        #endregion


        public class BrandAddedOrEditedEvent : PubSubEvent<BrandAddedOrEditedEventArgs> { }

        public class BrandAddedOrEditedEventArgs
        {
            public Brand Brand { get; set; }
        }
    }
}
