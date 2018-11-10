using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class Response
    {
        [Key, ValidGuid]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public RequestStatus NewStatus { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }


    }
}
