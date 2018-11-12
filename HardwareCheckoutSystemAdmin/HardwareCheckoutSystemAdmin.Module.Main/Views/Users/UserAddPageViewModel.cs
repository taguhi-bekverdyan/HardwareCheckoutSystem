using HardwareCheckoutSystemAdmin.Common.Prism;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Users
{
    public class UserAddPageViewModel : BindableBase, IRegionManagerAware
    {
        public IRegionManager RegionManager { get; set; }
       
        private UserItem _user_item;
        public UserItem User_item
        {
            get { return _user_item; }
            set { SetProperty(ref _user_item, value); }
        }
        public DelegateCommand SearchCommand => new DelegateCommand(Search);
        public UserAddPageViewModel()
        {
            User_item = new UserItem();
        }
        private void Search()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image File|*.bmp;*.gif;*.jpeg;*.jpg;*.png";
            open.Title = "Select a picture";
            if (open.ShowDialog() == true)
            {
                string path = open.FileName;
                MessageBox.Show(path);
                User_item.AvatarImage = path;
                MessageBox.Show(User_item.AvatarImage);
            }

        }
    }
}
