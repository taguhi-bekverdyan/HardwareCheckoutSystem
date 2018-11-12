using HardwareCheckoutSystemAdmin.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace HardwareCheckoutSystemAdmin.Models
{
  public class Brand
  {
    [Key, ValidGuid]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Device> Devices { get; set; }
  }
}