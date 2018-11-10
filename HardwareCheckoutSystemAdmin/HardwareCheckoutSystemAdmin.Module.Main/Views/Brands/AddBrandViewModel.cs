using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.HelperClasses;
using Prism.Commands;
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
        private ViewMode mode;
        private Brand brand;

        public AddBrandViewModel(IShellService shellservice, IBrandService brandservice)
        {
            _ishellservice = shellservice;
            _ibrandservice = brandservice;

        }

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
            throw new NotImplementedException();
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
                var newbrand = new Brand();
                newbrand.Name = Name;
                _ibrandservice.Insert(newbrand);
            }
        }

        private DelegateCommand _CancelAddingBrandCommand;
        public DelegateCommand CancelAddingBrandCommand => _CancelAddingBrandCommand ?? (_CancelAddingBrandCommand = new DelegateCommand(CancelAddingBrandAction));

        public void CancelAddingBrandAction()
        {
            //TODO
        }
    }
}
