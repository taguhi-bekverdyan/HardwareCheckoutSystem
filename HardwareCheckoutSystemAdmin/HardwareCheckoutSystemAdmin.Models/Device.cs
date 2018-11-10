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
        public Device()
        {
            //this.Id = new Guid();
            //this.BrandId = deviceviewitem.BrandId;
            //this.CategoryId = deviceviewitem.CategoryId;
            //this.Description = deviceviewitem.Description;
            //this.SerialNumber = deviceviewitem.SerialNumber;
            //this.Model = deviceviewitem.Model;
            //this.Status = deviceviewitem.Status;
            //this.Permission = deviceviewitem.Permission;
            //this.MaxPeriod = deviceviewitem.MaxPeriod;
            //this.Image = deviceviewitem.Image;

        }






    }
}
