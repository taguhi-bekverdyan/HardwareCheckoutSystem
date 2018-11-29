using HardwareCheckoutSystemAdmin.Models.HelperAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class Response
    {


        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public string Message { get; set; }
        public RequestStatus NewStatus { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RequestId { get; set; }
        public Request Request { get; set; }

        public Response()
        {
            Id = Guid.NewGuid();
        }

    }
}