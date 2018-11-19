using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.Categories;
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
using static HardwareCheckoutSystemAdmin.Module.Main.Views.Categories.AddCategoryViewModel;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Categories
{
    public class CategoriesViewModel : BindableBase, INavigationAware
    {
        private readonly IShellService _ishellservice;
        private readonly ICategoryService _icategoryservice;
        private readonly IEventAggregator _ieventaggregator;
        private ShellView addCategoryView;

        public CategoriesViewModel(IShellService shellservice, ICategoryService categoryservice, IEventAggregator eventaggregator)
        {
            _ishellservice = shellservice;
            _icategoryservice = categoryservice;
            _ieventaggregator = eventaggregator;
            Categories = new List<Category>();
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

        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }

            set { SetProperty(ref _categories, value); }
        }

        private Category _selectedcategory;
        public Category SelectedCategory
        {
            get { return _selectedcategory; }

            set { SetProperty(ref _selectedcategory, value); }
        }

        public async void FindAll()
        {
            Categories = await _icategoryservice.FindAll();
        }

        private DelegateCommand _DeleteCategoryCommand;
        public DelegateCommand DeleteCategoryCommand => _DeleteCategoryCommand ?? (_DeleteCategoryCommand = new DelegateCommand(DeleteCategoryAction));

        public async void DeleteCategoryAction()
        {
            await _icategoryservice.Delete(SelectedCategory);
            FindAll();

        }

        private DelegateCommand _AddCategoryCommand;
        public DelegateCommand AddCategoryCommand => _AddCategoryCommand ?? (_AddCategoryCommand = new DelegateCommand(AddCategoryAction));

        public void AddCategoryAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<Category>(ViewMode.Add, new Category()) } };
            _ieventaggregator.GetEvent<CategoryAddedOrEditedEvent>().Subscribe(CategoryAddedOrEditedEventHandler, ThreadOption.UIThread);
            addCategoryView = _ishellservice.ShowShell(nameof(AddCategoryView),param,280,200);

        }

        private DelegateCommand _EditCategoryCommand;
        public DelegateCommand EditCategoryCommand => _EditCategoryCommand ?? (_EditCategoryCommand = new DelegateCommand(EditCategoryAction));

        public void EditCategoryAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<Category>(ViewMode.Add, SelectedCategory) } };
            _ieventaggregator.GetEvent<CategoryAddedOrEditedEvent>().Subscribe(CategoryAddedOrEditedEventHandler, ThreadOption.UIThread);
           addCategoryView= _ishellservice.ShowShell(nameof(AddCategoryView),param,280,200);
        }

        private void CategoryAddedOrEditedEventHandler(CategoryAddedOrEditedEventArgs args)
        {
            addCategoryView.Close();
            FindAll();
            _ieventaggregator.GetEvent<CategoryAddedOrEditedEvent>().Unsubscribe(CategoryAddedOrEditedEventHandler);
        }

    }
}
