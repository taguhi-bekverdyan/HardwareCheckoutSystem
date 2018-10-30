using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements;
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

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.UserViewElements
{
    class UserListViewModel:BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;
        private ShellView _addUserView;

        private List<UserViewItem> _users = new List<UserViewItem>();
        public List<UserViewItem> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        private UserViewItem _selectedUser;
        public UserViewItem SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                SetProperty(ref _selectedUser, value);
                DeleteUser.RaiseCanExecuteChanged();
                UpdateUser.RaiseCanExecuteChanged();
            }
        }


        public UserListViewModel(IUserService service,IShellService shellService,IEventAggregator eventAggregator)
        {
            _userService = service;
            _shellService = shellService;
            _eventAggregator = eventAggregator;

            DeleteUser = new DelegateCommand(DeleteUserAction, CanUpdateOrDelete);
            AddUser = new DelegateCommand(AddUserAction);
            UpdateUser = new DelegateCommand(UpdateUserAction, CanUpdateOrDelete);
        }


        public DelegateCommand AddUser { get; private set; }

        public void AddUserAction()
        {
            NavigationParameters param = new NavigationParameters { { "request", new UserParameter(Mode.Add, null) } };
            _addUserView = _shellService.ShowShell(nameof(AddUserView),450,450,param);
            _eventAggregator.GetEvent<UserAddOrEditEvent>()
                .Subscribe(UserAddedEventHandler, ThreadOption.UIThread);
        }

        private async void UserAddedEventHandler(object obj)
        {
            await UpdateData();
            _addUserView.Close();
            _addUserView = null;
            _eventAggregator.GetEvent<UserAddOrEditEvent>().Unsubscribe(UserAddedEventHandler);
        }

        public DelegateCommand UpdateUser { get; private set; }

        public void UpdateUserAction()
        {
            NavigationParameters param = new NavigationParameters { { "request", new UserParameter(Mode.Edit, SelectedUser) } };
            _addUserView = _shellService.ShowShell(nameof(AddUserView), 450, 450, param);
            _eventAggregator.GetEvent<UserAddOrEditEvent>()
                .Subscribe(UserAddedEventHandler, ThreadOption.UIThread);
        }

        public DelegateCommand DeleteUser { get; private set; }

        public async void DeleteUserAction()
        {
            User user = (User)SelectedUser;
            await _userService.DeleteUserById(user.Id);
            SelectedUser = null;
            await UpdateData();
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedUser == null)
            {
                return false;
            }
            return true;
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await UpdateData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateData()
        {
            Users = new List<UserViewItem>();
            List<User> temp = await _userService.FindAll();
            foreach (var item in temp)
            {
                Users.Add(new UserViewItem(item));
            }
        }

    }
}
