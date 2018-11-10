using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class Request
    {
        [Key, ValidGuid]
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public User Device { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime RentEndDate { get; set; }
        public string Message { get; set; }
        public Guid? LastResponseId { get; set; }
        public Response LastResponse { get; set; }

        public List<Response> Responses { get; set; }
    }
}
