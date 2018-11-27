using HardwareCheckoutSystemWebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Models
{
    public class User
    {
        [Key, ValidGuid]
        [Required]
        public Guid Id { get; set; }
        [StringLength(25)]
        public string FirstName { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        [StringLength(25)]
        public string TelNumber { get; set; }
        public Permission Permission { get; set; }
        public byte[] AvatarImage { get; set; }
        [StringLength(25)]
        public string Occupation { get; set; }

        public ICollection<Request> Requests { get; set; }


        public User()
        {
            Id = Guid.NewGuid();
            Requests = new List<Request>();
        }

    }
}
