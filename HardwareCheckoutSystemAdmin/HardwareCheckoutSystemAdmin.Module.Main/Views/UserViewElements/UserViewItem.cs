using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.UserViewElements
{
    class UserViewItem
    {
        private Guid _id;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string TelNumber { get; set; }
        public Permission Permission { get; set; }
        public byte[] AvatarImage { get; set; }
        public string Occupation { get; set; }

        public BitmapSource BitmapImage { get; set; }


        public UserViewItem()
        {
            _id = Guid.NewGuid();
        }

        public UserViewItem(User user)
        {
            _id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Address = user.Address;
            Birthdate = user.Birthdate;
            TelNumber = user.TelNumber;
            Permission = user.Permission;
            GetImage(user);
            Occupation = user.Occupation;
        }

        private void GetImage(User user)
        {
            if (user.AvatarImage == null || user.AvatarImage.Length == 0)
            {
                return;
            }

            BitmapImage = (BitmapSource)new ImageSourceConverter().ConvertFrom(user.AvatarImage);
        }

        public static explicit operator User(UserViewItem item)
        {
            return new User() {
                Id = item._id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Address = item.Address,
                Birthdate = item.Birthdate,
                TelNumber = item.TelNumber,
                Permission = item.Permission,
                AvatarImage = item.AvatarImage,
                Occupation = item.Occupation
            };            
        }

    }
}
