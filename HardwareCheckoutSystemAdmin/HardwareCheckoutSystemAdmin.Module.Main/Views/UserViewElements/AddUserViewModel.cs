using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.UserViewElements
{
    class AddUserViewModel : BindableBase, INavigationAware
    {
        #region Services
        private readonly IUserService _userService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Properties
        private UserViewItem _user;
        private Mode _mode;

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
            }
        }

        private string _lasttName;
        public string LastName
        {
            get { return _lasttName; }
            set
            {
                SetProperty(ref _lasttName, value);
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                SetProperty(ref _address, value);
            }
        }

        private DateTime _birthdate = DateTime.Now;
        public DateTime Birthdate
        {
            get { return _birthdate; }
            set
            {
                SetProperty(ref _birthdate, value);
            }
        }

        private string _telNumber;
        public string TelNumber
        {
            get { return _telNumber; }
            set
            {
                SetProperty(ref _telNumber, value);
            }
        }

        private Permission _permission;
        public Permission Permission
        {
            get { return _permission; }
            set
            {
                SetProperty(ref _permission, value);
            }
        }

        private string _occupation;
        public string Occupation
        {
            get { return _occupation; }
            set
            {
                SetProperty(ref _occupation, value);
            }
        }

        private byte[] _avatarimage;
        public byte[] AvatarImage
        {
            get { return _avatarimage; }
            set
            {
                SetProperty(ref _avatarimage, value);
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                SetProperty(ref _imagePath, value);
                AddUser.RaiseCanExecuteChanged();
            }
        }



        #endregion

        #region Ctor
        public AddUserViewModel(IUserService userService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _userService = userService;
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            TelNumber = string.Empty;
            Occupation = string.Empty;


            AddUser = new DelegateCommand(AddUserAction);
            Cancel = new DelegateCommand(CancelAction);
            ChooseImage = new DelegateCommand(ChooseImageAction);

        }
        #endregion

        #region Navigation
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
            UserParameter param = (UserParameter)navigationContext.Parameters["request"];
            _mode = param.Mode;
            if (_mode == Mode.Add)
            {
                _user = new UserViewItem();

            }
            else
            {
                _user = param.User;
                GetData();
            }
        }
        #endregion

        #region Helpers
        private void GetData()
        {
            LastName = _user.LastName;
            FirstName = _user.FirstName;
            Address = _user.Address;
            Birthdate = _user.Birthdate;
            Permission = _user.Permission;
            TelNumber = _user.TelNumber;
            Occupation = _user.Occupation;
            AvatarImage = _user.AvatarImage;
            ImagePath = string.Empty;
            // AvatarImage = GetBytesFromImage(ImagePath);
        }
        private void SetData()
        {
            _user.LastName = LastName;
            _user.FirstName = FirstName;
            _user.Address = Address;
            _user.Birthdate = Birthdate;
            _user.Permission = Permission;
            _user.TelNumber = TelNumber;
            _user.Occupation = Occupation;
            _user.AvatarImage = GetBytesFromImage(ImagePath);
        }

        private void CallbackAction()
        {
            _eventAggregator.GetEvent<UserAddOrEditEvent>()
                .Publish(new Object());
        }

        private byte[] GetBytesFromImage(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                //return File.ReadAllBytes(path);
                Bitmap image = new Bitmap(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            return null;
        }
        #endregion

        #region Commands

        public DelegateCommand AddUser { get; private set; }
        private async void AddUserAction()
        {
            if (_mode == Mode.Add)
            {
                SetData();
                User user = (User)_user;
                await _userService.Insert(user);
            }
            else
            {
                SetData();
                User user = (User)_user;
                await _userService.Update(user);
            }
            CallbackAction();
        }

        public DelegateCommand Cancel { get; private set; }
        private void CancelAction()
        {
            CallbackAction();
        }

        public DelegateCommand ChooseImage { get; private set; }
        private void ChooseImageAction()
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.Filter = "AvatarImage files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            fileChooser.FilterIndex = 1;
            fileChooser.Multiselect = true;

            if (fileChooser.ShowDialog() == DialogResult.OK)
            {
                //System.Windows.MessageBox.Show(fileChooser.FileName);
                ImagePath = fileChooser.FileName;
            }

        }

        #endregion

    }

    public class UserAddOrEditEvent:PubSubEvent<object>
    {

    }

}
