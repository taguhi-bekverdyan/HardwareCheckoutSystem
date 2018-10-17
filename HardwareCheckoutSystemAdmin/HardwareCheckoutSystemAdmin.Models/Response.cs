using HardwareCheckoutSystemAdmin.Models.HelperAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareCheckoutSystemAdmin.Models
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
    }
}