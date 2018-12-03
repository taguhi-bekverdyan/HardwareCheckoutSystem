using HardwareCheckoutSystemAdmin.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        public int? MaxPeriod { get; set; }
        public DeviceStatus Status { get; set; }
        public Permission Permission { get; set; }
        public byte[] Image { get; set; }

        public BitmapSource BitmapImage { get; set; }

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
            GetImage(d);
            Permission = d.Permission;

        }

        private void GetImage(Device d)
        {
            if (d.Image == null || d.Image.Length == 0)
            {
                return;
            }

            BitmapImage = (BitmapSource)new ImageSourceConverter().ConvertFrom(d.Image);

        }

        public DeviceViewItem()
        {
            _id = Guid.NewGuid();
            SerialNumber = string.Empty;
            Model = string.Empty;
            Description = string.Empty;
            Status = DeviceStatus.StatusOne;

        }

        public Guid GetId()
        {
            return _id;
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
