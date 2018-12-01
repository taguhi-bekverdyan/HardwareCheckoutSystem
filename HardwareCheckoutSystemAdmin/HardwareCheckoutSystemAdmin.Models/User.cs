using HardwareCheckoutSystemAdmin.Models.HelperAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class User
    {

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }

        public string TelNumber { get; set; }
        public Permission Permission { get; set; }
        public byte[] AvatarImage { get; set; }

        public string Occupation { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
