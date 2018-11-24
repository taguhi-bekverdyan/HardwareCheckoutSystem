using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Users
{
    public class UserPageViewModel: BindableBase, IRegionManagerAware
    {
        public IRegionManager RegionManager { get; set; }
        public DelegateCommand DelateCommand => new DelegateCommand(DeleteUser);
        public DelegateCommand<object> Save { get; set; }
        public ObservableCollection<UserItem> UserItems { get; set; }
        public UserItem SelectedUser { get; set; }
        private IShellService _service;
        public DelegateCommand<UserItem> EditCommand { get;private set; }
        public DelegateCommand<UserItem> AddCommand { get; private set; }
        private readonly IUserService _users;

        public UserPageViewModel(IShellService service, IUserService users)
        {
            Save = new DelegateCommand<object>(DoSave, CanSave);
            GlobalCommands.SaveUser.RegisterCommand(Save);
            _service = service;
            _users = users;
            UserItems = new ObservableCollection<UserItem>();
            SelectedUser = new UserItem();
            EditCommand = new DelegateCommand<UserItem>(Edit);
            AddCommand = new DelegateCommand<UserItem>(Add_User);

            UserItem ui = new UserItem();
            ui.Address = "User_Item.Address";
            ui.Birthdate = DateTime.Now;
            ui.FirstName = "User_Item.FirstName";
            ui.LastName = "User_Item.LastName";
            ui.Occupation = "User_Item.Occupation";
            ui.TelNumber = "User_Item.TelNumber";
            UserItems.Add(ui);
        }
       
        private void Edit(UserItem useritem)
        {
            
            var Parameters = new NavigationParameters();
            UserItem userParams = (UserItem)SelectedUser.Clone();
            userParams.Index = UserItems.IndexOf(SelectedUser);
            Parameters.Add("Param", userParams);
            var uri = new Uri(Parameters.ToString(),UriKind.Relative);
            _service.ShowShell(nameof(UserAddPageView),Parameters);
            
        }

        private void Add_User(UserItem useritem)
        {
            var Parameters = new NavigationParameters();
            UserItem userParams = new UserItem();
            userParams.Index = -1;
            Parameters.Add("Param", userParams);
            var uri = new Uri(Parameters.ToString(), UriKind.Relative);
            _service.ShowShell(nameof(UserAddPageView), Parameters);
        }

        private  void DeleteUser()
        {
            UserItems.Remove(SelectedUser);
        }

        private  void DoSave(object value)
        {
            UserItem User_Item = (UserItem)value;
            UserItem ui = new UserItem();
            ui.Address = User_Item.Address;
            ui.AvatarImage = User_Item.AvatarImage;
            ui.Birthdate = User_Item.Birthdate;
            ui.FirstName =  User_Item.FirstName;
            ui.LastName = User_Item.LastName;
            ui.Occupation = User_Item.Occupation;
            ui.Permission = User_Item.Permission;
            ui.TelNumber = User_Item.TelNumber;
            ui.Index = User_Item.Index;
            if (User_Item.Index == -1)
            {
                UserItems.Add(ui);
                
            }
            else
            {
                MessageBox.Show(ui.Index.ToString());
                UserItems[ui.Index] = ui;
              
            }
        }
        private bool CanSave(object value) { return true; }
        private byte[] ConvertBitmapToByteArr(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
                return data;
            }
        }
    }
}
