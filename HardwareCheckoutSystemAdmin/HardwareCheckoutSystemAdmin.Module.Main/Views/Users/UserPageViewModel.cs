using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Users
{
    public class UserPageViewModel: BindableBase, IRegionManagerAware
    {
        public IRegionManager RegionManager { get; set; }
        public DelegateCommand AddCommand => new DelegateCommand(AddCategoryPageAction);
        public DelegateCommand<object> Save { get; set; }
        public ObservableCollection<UserItem> UserItems { get; set; }
        private IShellService _service;
        public UserPageViewModel(IShellService service)
        {
            Save = new DelegateCommand<object>(DoSave, CanSave);
            GlobalCommands.SaveUser.RegisterCommand(Save);
            _service = service;
            UserItems = new ObservableCollection<UserItem>();
        }
        private void DoSave(object value)
        {
            UserItem User_Item = (UserItem)value;
            //MessageBox.Show(User_Item.FirstName);
            UserItem ui = new UserItem();
            ui.Address = User_Item.Address;
            ui.AvatarImage = User_Item.AvatarImage;
            ui.Birthdate = User_Item.Birthdate;
            ui.FirstName = User_Item.FirstName;
            ui.LastName = User_Item.LastName;
            ui.Occupation = User_Item.Occupation;
            ui.Permission = User_Item.Permission;
            ui.TelNumber = User_Item.TelNumber;
            UserItems.Add(ui);
            MessageBox.Show(ui.Permission.ToString());
        }
        private bool CanSave(object value) { return true; }
        private void AddCategoryPageAction()
        {
         _service.ShowShell(nameof(UserAddPageView));   
        }
    }
}
