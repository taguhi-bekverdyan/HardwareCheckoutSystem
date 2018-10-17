using HardwareCheckoutSystemAdmin.Models.HelperAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class Request
    {
        [Key, ValidGuid]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid DeviceId { get; set; }
        public Device Device { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public RequestStatus Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? RentStartDate { get; set; }
        public DateTime? RentEndDate { get; set; }
        public string Message { get; set; }

        [ForeignKey("Response")]
        public Guid? LastResponseId { get; set; }
        public Response Response { get; set; }

        public ICollection<Response> Responses { get; set; }
    }

    public enum RequestStatus
    {
        Other,
        StatusOne,
        StatusTwo,
        StatusThree,
        StatusFour,
        StatusFive
    }
}