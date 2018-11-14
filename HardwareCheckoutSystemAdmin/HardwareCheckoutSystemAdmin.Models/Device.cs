using System;
using System.ComponentModel.DataAnnotations;

namespace HardwareCheckoutSystemAdmin.Models
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
        public Device(DeviceViewItem deviceviewitem)
        {
            Id = new Guid();
            BrandId = deviceviewitem.BrandId;
            CategoryId = deviceviewitem.CategoryId;
            Description = deviceviewitem.Description;
            SerialNumber = deviceviewitem.SerialNumber;
            Model = deviceviewitem.Model;
            //Status = deviceviewitem.Status;
            Permission = deviceviewitem.Permission;
            //MaxPeriod = deviceviewitem.MaxPeriod;
            //Image = deviceviewitem.Image;
        }

        public Device()
        {

        }






    }
}
