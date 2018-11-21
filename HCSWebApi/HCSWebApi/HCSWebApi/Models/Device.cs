using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Models
{
    public class Device
    {
        [Key, ValidGuid]
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DeviceStatus Status { get; set; }
        public Permission Permission { get; set; }
        public DateTime MaxPeriod { get; set; }
    }
}
