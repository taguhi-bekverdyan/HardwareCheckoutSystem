using HardwareCheckoutSystemWeb.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWeb.Models
{
    public class Device : IEquatable<Device>
    {
        [Key, ValidGuid]
        [Required]
        public Guid Id { get; set; }
        [StringLength(50), Required]
        public string SerialNumber { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        public string Description { get; set; }
        public int? MaxPeriod { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public DeviceStatus Status { get; set; }
        [Required]
        public Permission Permission { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public Device()
        {
            Id = Guid.NewGuid();
        }

        public bool Equals(Device other)
        {
            return Id == other.Id;
        }
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
