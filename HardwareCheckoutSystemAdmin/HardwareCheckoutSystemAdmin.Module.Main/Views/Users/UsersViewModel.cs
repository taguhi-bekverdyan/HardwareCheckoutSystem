using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Services;
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
using System.Windows;
using static HardwareCheckoutSystemAdmin.Module.Main.Views.Users.AddUserViewModel;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Users
{
    public class UsersViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _iuserservice;
        private readonly IShellService _ishellservice;
        private readonly IEventAggregator _ieventaggregator;
        private ShellView addUserView;

        public UsersViewModel(IEventAggregator eventaggregator, IUserService userservice, IShellService shellservice)
        {
            _iuserservice = userservice;
            _ieventaggregator = eventaggregator;
            _ishellservice = shellservice;
        }

        #region TYPES

        private bool _isbusy;
        public bool IsBusy
        {
            get { return _isbusy; }

            set { SetProperty(ref _isbusy, value); }
        }

        private List<User> _users;
        public List<User> Users
        {
            get { return _users; }

            set { SetProperty(ref _users, value); }
        }

        private User _selecteditem;
        public User SelectedItem
        {
            get { return _selecteditem; }

            set
            {
                SetProperty(ref _selecteditem, value);
                DeleteUserCommand.RaiseCanExecuteChanged();
                EditUserCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region BUTTONS
        private DelegateCommand _EditUserCommand;
        public DelegateCommand EditUserCommand => _EditUserCommand ?? (_EditUserCommand = new DelegateCommand(EditUserAction));

        public void EditUserAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<User>(ViewMode.Edit, SelectedItem) } };
            addUserView = _ishellservice.ShowShell(nameof(AddUserView), param,450,450);
            _ieventaggregator.GetEvent<UserAddedOrEditedEvent>().Subscribe(UserAddedOrEditedEventHandler, ThreadOption.UIThread);

        }

        private DelegateCommand _AddUserCommand;
        public DelegateCommand AddUserCommand => _AddUserCommand ?? (_AddUserCommand = new DelegateCommand(AddUserAction));

        public void AddUserAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", new Param<User>(ViewMode.Add, new User()) } };
            _ieventaggregator.GetEvent<UserAddedOrEditedEvent>().Subscribe(UserAddedOrEditedEventHandler, ThreadOption.UIThread);
            addUserView = _ishellservice.ShowShell(nameof(AddUserView), param,450,450);
        }

        private DelegateCommand _DeleteUserCommand;
        public DelegateCommand DeleteUserCommand => _DeleteUserCommand ?? (_DeleteUserCommand = new DelegateCommand(DeleteUserAction));

        public async void DeleteUserAction()
        {
            await _iuserservice.DeleteBySerialNumber(SelectedItem.SerialNumber);
            await UpdateUsersData();
        }

        #endregion

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await UpdateUsersData();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        private async Task UpdateUsersData()
        {
            try
            {
                Users = await _iuserservice.FindAll();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void UserAddedOrEditedEventHandler(UserAddedOrEditedEventArgs args)
        {
            addUserView.Close();
            await UpdateUsersData();
            _ieventaggregator.GetEvent<UserAddedOrEditedEvent>().Unsubscribe(UserAddedOrEditedEventHandler);

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
