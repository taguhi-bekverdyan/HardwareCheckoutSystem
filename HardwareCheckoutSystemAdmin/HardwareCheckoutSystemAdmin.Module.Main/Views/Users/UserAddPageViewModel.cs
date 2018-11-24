﻿using HardwareCheckoutSystemAdmin.Common.Prism;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Users
{
    public class UserAddPageViewModel : BindableBase, IRegionManagerAware, INavigationAware
    {
        public IRegionManager RegionManager { get; set; }
        private UserItem _user_item;
        private IShellService _service;
        private Permission selectedUserPermisssion;
        public DelegateCommand SearchCommand => new DelegateCommand(Search);
        public DelegateCommand PermisssionChenged => new DelegateCommand(PermisssionChengedFunc);
        private void PermisssionChengedFunc()
        {
            User_item.Permission = SelectedUserPermisssion;
        }
        public Permission SelectedUserPermisssion
        {
            get { return selectedUserPermisssion; }
            set { SetProperty(ref selectedUserPermisssion, value); }
        }

        public UserItem User_item
        {
            get { return _user_item; }
            set { SetProperty(ref _user_item, value); }
        }
        
        public UserAddPageViewModel(IShellService service)
        {
            _service = service;
            User_item = new UserItem();
        }
       
        private void Search()
        {
           
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image File|*.bmp;*.gif;*.jpeg;*.jpg;*.png";
            open.Title = "Select a picture";
            if (open.ShowDialog() == true)
            {
                User_item.AvatarImage = new BitmapImage(new Uri(open.FileName));
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            User_item = (UserItem)navigationContext.Parameters["Param"];
            SelectedUserPermisssion = User_item.Permission;
            MessageBox.Show(User_item.Index.ToString());
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
