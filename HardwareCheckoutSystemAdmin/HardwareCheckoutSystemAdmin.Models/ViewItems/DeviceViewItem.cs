using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class DeviceViewItem
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public string Model { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public DeviceStatus Status { get; set; }
        public Permission Permission { get; set; }
        public DateTime MaxPeriod { get; set; }

        public DeviceViewItem()
        {

        }

        public DeviceViewItem(Device device)
        {
            this.Id = Id;
            this.Brand = device.Brand;
            this.Category = device.Category;
            this.Description = device.Description;
            this.SerialNumber = device.SerialNumber;
            this.Model = device.Model;
            this.Status = device.Status;
            this.Permission = device.Permission;
            this.MaxPeriod = device.MaxPeriod;
            this.Image = device.Image;
        }


    }
}

