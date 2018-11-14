﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class UserViewItem
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Permission Permission { get; set; }
        public string Address { get; set; }
        public string TelNumber { get; set; }
        //public byte[] AvatarImage { get; set; }
        public string SerialNumber { get; set; }

        public UserViewItem( User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            BirthDate = user.BirthDate;
            Permission = user.Permission;
            Address = user.Address;
            TelNumber = user.TelNumber;
            SerialNumber = user.SerialNumber;

        }

        public UserViewItem ()
        {

        }
    }
}
