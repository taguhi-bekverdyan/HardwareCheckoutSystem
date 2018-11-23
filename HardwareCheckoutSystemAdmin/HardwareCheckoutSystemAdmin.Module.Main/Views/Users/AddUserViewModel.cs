using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Data.Services;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Users
{
    public class AddUserViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _ieventaggregator;
        private readonly IUserService _iuserservice;
        private readonly IShellService _ishellservice;
        private ViewMode mode;

        public AddUserViewModel(IEventAggregator eventaggregator, IUserService userservice, IShellService shellservice)
        {
            _ieventaggregator = eventaggregator;
            _iuserservice = userservice;
            _ishellservice = shellservice;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        
        private string _serialnumber;
        public string SerialNumber
        {
            get { return _serialnumber; }

            set { SetProperty(ref _serialnumber, value); }
        }

        private byte[] _avatarimage;
        public byte[] AvatarImage
        {
            get { return _avatarimage; }

            set { SetProperty(ref _avatarimage, value); }
        }

        private string _firstname;
        public string FirstName
        {
            get { return _firstname; }

            set { SetProperty(ref _firstname, value); }
        }

        private string _lastname;
        public string LastName
        {
            get { return _lastname; }

            set { SetProperty(ref _lastname, value); }
        }

        private string _telnumber;
        public string TelNumber
        {
            get { return _telnumber; }

            set { SetProperty(ref _telnumber, value); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }

            set { SetProperty(ref _address, value); }
        }
        private Permission _permission;
        public Permission Permission
        {
            get { return _permission; }

            set { SetProperty(ref _permission, value); }
        }

        private DateTime _datebirth;
        public DateTime DateBirth
        {
            get { return _datebirth; }

            set { SetProperty(ref _datebirth, value); }
        }


        private DelegateCommand _CancelAddingUserCommand;
        public DelegateCommand CancelAddingUserCommand => _CancelAddingUserCommand ?? (_CancelAddingUserCommand = new DelegateCommand(CancelAddingUserAction));

        public void CancelAddingUserAction()
        {
            _ieventaggregator.GetEvent<UserAddedOrEditedEvent>().Publish(new UserAddedOrEditedEventArgs { User = null });
        }

        private DelegateCommand _ChooseImageCommand;
        public DelegateCommand ChooseImageCommand => _ChooseImageCommand ?? (_ChooseImageCommand = new DelegateCommand(ChooseImageAction));

        public void ChooseImageAction()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            var result = dialog.ShowDialog();
            AvatarImage = File.ReadAllBytes(dialog.FileName);
        }

        private DelegateCommand _AddNewUserCommand;
        public DelegateCommand AddNewUserCommand => _AddNewUserCommand ?? (_AddNewUserCommand = new DelegateCommand(AddNewUserAction));

        public void AddNewUserAction()
        {
            User user = new User();
            user.Id = new Guid();
            user.Permission = Permission;
            user.AvatarImage = AvatarImage;
            user.LastName = LastName;
            user.SerialNumber = SerialNumber;
            user.Permission = Permission;
            if (mode == ViewMode.Edit)
            {
                _iuserservice.Update(user);
            }
            else
            {
                user.Id = new Guid();
                _iuserservice.Insert(user);
            }
            _ieventaggregator.GetEvent<UserAddedOrEditedEvent>().Publish(new UserAddedOrEditedEventArgs { User = user });
        }


        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var param = (Param<UserViewItem>)navigationContext.Parameters["request"];
            var user = param._ViewItem;
            mode = param._ViewMode;
            if (param._ViewMode.Equals(ViewMode.Edit))
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                Permission = user.Permission;
                SerialNumber = user.LastName;
            }
            else
            {
                FirstName = null;
                LastName = null;
                SerialNumber = null;
                Permission = Permission.Other;
            }
        }

        public class UserAddedOrEditedEvent : PubSubEvent<UserAddedOrEditedEventArgs> { }

        public class UserAddedOrEditedEventArgs
        {
            public User User { get; set; }
        }
    }
}
