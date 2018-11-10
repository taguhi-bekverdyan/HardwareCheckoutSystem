using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Categories
{
    public class AddCategoryViewModel : BindableBase, INavigationAware
    {
        private readonly IShellService _ishellservice;
        private readonly ICategoryService _icategoryservice;

        public AddCategoryViewModel(IShellService shellservice, ICategoryService categoryservice)
        {
            _ishellservice = shellservice;
            _icategoryservice = categoryservice;

        }

        //private Category _newcategory;
        //public Category NewCategory
        //{
        //    get { return _newcategory; }

        //    set { SetProperty(ref _newcategory, value); }
        //}

        private string _name;
        public string Name
        {
            get { return _name; }

            set { SetProperty(ref _name, value); }
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
            Category CategoryToEdit = (Category)navigationContext.Parameters["request_for_edit"];
            Name = CategoryToEdit.Name;
        }

        private DelegateCommand _AddCategoryCommand;
        public DelegateCommand AddCategoryCommand => _AddCategoryCommand ?? (_AddCategoryCommand = new DelegateCommand(AddCategoryAction));

        public void AddCategoryAction()
        {
            var NewCategory = new Category(Name);
            _icategoryservice.Insert(NewCategory);
            _ishellservice.ShowShell(nameof(AddCategoryView));
        }

        private DelegateCommand _CancelAddingCategoryCommand;
        public DelegateCommand CancelAddingCategoryCommand => _CancelAddingCategoryCommand ?? (_CancelAddingCategoryCommand = new DelegateCommand(CancelAddingCategoryAction));

        public void CancelAddingCategoryAction()
        {
            //TODO
        }
    }
}
