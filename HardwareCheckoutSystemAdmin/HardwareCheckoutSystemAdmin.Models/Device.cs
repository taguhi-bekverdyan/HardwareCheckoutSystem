using HardwareCheckoutSystemAdmin.Models.HelperAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime? MaxPeriod { get; set; }
        public byte[] Image { get; set; }
        public DeviceStatus Status { get; set; }
        public Permission Permission { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }

    public enum DeviceStatus
    {
        Other,
        StatusOne,
        StatusTwo,
        StatusThree,
        StatusFour,
        StatusFive
    }

}
