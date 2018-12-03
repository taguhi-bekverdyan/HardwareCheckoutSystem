using HardwareCheckoutSystemWeb.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWeb.Models
{
    public class Response
    {
        [Key, ValidGuid]
        [Required]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public string Message { get; set; }
        public RequestStatus NewStatus { get; set; }

        [ForeignKey("User")]
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public Guid RequestId { get; set; }
        public Request Request { get; set; }

        public Response()
        {
            Id = Guid.NewGuid();
        }

    }
}
