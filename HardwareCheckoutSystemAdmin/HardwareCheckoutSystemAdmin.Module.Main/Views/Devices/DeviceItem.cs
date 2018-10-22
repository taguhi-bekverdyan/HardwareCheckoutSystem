using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class DeviceItem
  {
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public Category Category { get; set; }
    public Brand Brand { get; set; }
    public string Model { get; set; }
    public string Image { get; set; }
    public DeviceStatus Status { get; set; }
    public Permission Permission { get; set; }
    public DateTime MaxPeriod { get; set; }

    public DeviceItem(Device device)
    {
      Description = device.Description;
      SerialNumber = device.SerialNumber;
      Model = device.Model;
      Image = device.Image;
      Status = device.Status;
      MaxPeriod = device.MaxPeriod;
      Permission = device.Permission;
      Brand = device.Brand;  // null
      Category = device.Category; // null
    }
  }
}
