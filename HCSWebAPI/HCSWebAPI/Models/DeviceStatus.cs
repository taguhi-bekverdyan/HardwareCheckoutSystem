using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Models
{
  public enum DeviceStatus
  {
    Other,
    [Description("In Stock")]
    InStock,
    [Description("In Use")]
    InUse,
    Shipping,
    [Description("In Repair")]
    InRepair,
    Malfunctioning
  }
}
