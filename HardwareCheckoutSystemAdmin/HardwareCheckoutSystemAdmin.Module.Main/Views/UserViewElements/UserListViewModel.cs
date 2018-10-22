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
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.UserViewElements
{
    class UserListViewModel:BindableBase, INavigationAware
    {
        private IUserService _userService;


        private List<User> _users = new List<User>();
        public List<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                SetProperty(ref _selectedUser, value);
                DeleteUser.RaiseCanExecuteChanged();
            }
        }


        public UserListViewModel(IUserService service)
        {
            _userService = service;

            DeleteUser = new DelegateCommand(DeleteUserAction, CanUpdateOrDelete);
            AddUser = new DelegateCommand(AddUserAction);
            UpdateUser = new DelegateCommand(UpdateUserAction, CanUpdateOrDelete);
        }


        public DelegateCommand AddUser { get; private set; }

        public void AddUserAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand UpdateUser { get; private set; }

        public void UpdateUserAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand DeleteUser { get; private set; }

        public void DeleteUserAction()
        {
            MessageBox.Show("TODO"); 
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedUser == null)
            {
                return false;
            }
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private async void UpdateData()
        {
            Users = await _userService.FindAll();
        }

    }
}
