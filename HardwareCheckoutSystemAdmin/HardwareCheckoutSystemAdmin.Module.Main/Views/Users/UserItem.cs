using HardwareCheckoutSystemAdmin.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Users
{
    public class UserItem:BindableBase,ICloneable
    {
        private string firstName;
        private string lastName;
        private string address;
        private DateTime birthdate;
        private string telNumber;
        private Permission permission;
        private BitmapImage avatarImage;
        private string occupation;
        private int index;

        public string FirstName { get { return firstName; } set { SetProperty(ref firstName, value); } }
        public string LastName { get { return lastName; } set { SetProperty(ref lastName, value); } }
        public string Address { get { return address; } set { SetProperty(ref address, value); } }
        public DateTime Birthdate { get { return birthdate; } set { SetProperty(ref birthdate, value); } }
        public string TelNumber { get { return telNumber; } set { SetProperty(ref telNumber, value); } }
        public Permission Permission { get { return permission; } set { SetProperty(ref permission, value); } }
        public BitmapImage AvatarImage { get { return avatarImage; } set { SetProperty(ref avatarImage, value); } }
        public string Occupation { get { return occupation; } set { SetProperty(ref occupation, value); } }
        public int Index { get { return index; } set { SetProperty(ref index, value); } }
        public UserItem()
        {
                
        }

        private BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        public object Clone()
        {
            UserItem UserItemClone = new UserItem();
            UserItemClone.FirstName = this.FirstName;
            UserItemClone.LastName = this.LastName;
            UserItemClone.Address = this.Address;
            UserItemClone.Birthdate = this.Birthdate;
            UserItemClone.TelNumber = this.TelNumber;
            UserItemClone.Permission = this.Permission;
            UserItemClone.AvatarImage = this.AvatarImage;
            UserItemClone.Occupation = this.Occupation;
            return UserItemClone;
        }

        public UserItem(User user)
        {

         FirstName = user.FirstName;
         LastName = user.LastName;
         Address = user.Address;
         Birthdate = user.Birthdate;
         TelNumber = user.TelNumber;
         Permission = user.Permission;
         AvatarImage = ToImage(user.AvatarImage);
         Occupation = user.Occupation;
    }
    }
}
