using HardwareCheckoutSystemAdmin.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements
{
    class DeviceViewItem:BindableBase
    {
        private Guid _id;
        public string SerialNumber { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime? MaxPeriod { get; set; }
        public DeviceStatus Status { get; set; }
        public Permission Permission { get; set; }
        public byte[] Image { get; set; }

        public DeviceViewItem(Device d)
        {
            _id = d.Id;
            SerialNumber = d.SerialNumber;
            Category = d.Category;
            Brand = d.Brand;
            Model = d.Model;
            Description = d.Description;
            MaxPeriod = d.MaxPeriod;
            Status = d.Status;
            Permission = d.Permission;
        }

        public DeviceViewItem()
        {
            _id = Guid.NewGuid();
        }

        public static explicit operator Device(DeviceViewItem item)
        {
            Device device = new Device();
            device.Id = item._id;
            device.SerialNumber = item.SerialNumber;
            device.Model = item.Model;
            device.Description = item.Description;
            device.Permission = item.Permission;
            device.Status = item.Status;
            device.Image = item.Image;
            device.MaxPeriod = item.MaxPeriod;           
            return device;
        }

    }
}
