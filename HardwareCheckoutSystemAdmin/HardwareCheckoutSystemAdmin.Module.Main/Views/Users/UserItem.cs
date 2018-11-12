using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Users
{
    public class UserItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Birthdate { get; set; }
        public string TelNumber { get; set; }
        public Permission Permission { get; set; }
        public string AvatarImage { get; set; }
        public string Occupation { get; set; }
        public UserItem()
        {
                
        }
        public UserItem(User user)
        {
         FirstName = user.FirstName;
         LastName = user.LastName;
         Address = user.Address;
         Birthdate = user.Birthdate.ToString();
         TelNumber = user.TelNumber;
         Permission = user.Permission;
         AvatarImage = user.AvatarImage.ToString();
         Occupation = user.Occupation;
    }
    }
}
