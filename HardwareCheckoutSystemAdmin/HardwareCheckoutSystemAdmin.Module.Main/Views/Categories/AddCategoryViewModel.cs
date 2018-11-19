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

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Categories
{
    public class AddCategoryViewModel : BindableBase, INavigationAware
    {
        private readonly IShellService _ishellservice;
        private readonly ICategoryService _icategoryservice;
        private readonly IEventAggregator _ieventaggregator;
        private Category category;
        private ViewMode mode;
       

        public AddCategoryViewModel(IShellService shellservice, ICategoryService categoryservice, IEventAggregator eventaggregator)
        {
            _ishellservice = shellservice;
            _icategoryservice = categoryservice;
            _ieventaggregator = eventaggregator;
        
        }

        

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
            var param = (Param<Category>)navigationContext.Parameters["request"];
            category = param._ViewItem;
            mode = param._ViewMode;
            if (param._ViewMode.Equals(ViewMode.Edit))
            {
                Name = category.Name;
            }
            else
            {
                Name = null;
            }
        }

        private DelegateCommand _AddCategoryCommand;
        public DelegateCommand AddCategoryCommand => _AddCategoryCommand ?? (_AddCategoryCommand = new DelegateCommand(AddCategoryAction));

        public void AddCategoryAction()
        {
           
            if (mode == ViewMode.Add)
            {
                category = new Category(Name);
                _icategoryservice.Insert(category);
            }
            else
            {
                category.Name = Name;
                _icategoryservice.Update(category);
            }
           
            _ieventaggregator.GetEvent<CategoryAddedOrEditedEvent>().Publish(new CategoryAddedOrEditedEventArgs { Category = category });

        }

        private DelegateCommand _CancelAddingCategoryCommand;
        public DelegateCommand CancelAddingCategoryCommand => _CancelAddingCategoryCommand ?? (_CancelAddingCategoryCommand = new DelegateCommand(CancelAddingCategoryAction));

        public void CancelAddingCategoryAction()
        {
            //addCategoryView.Close();
        }

        public class CategoryAddedOrEditedEvent : PubSubEvent<CategoryAddedOrEditedEventArgs> { }

        public class CategoryAddedOrEditedEventArgs
        {
            public Category Category { get; set; }
        }
    }
}
