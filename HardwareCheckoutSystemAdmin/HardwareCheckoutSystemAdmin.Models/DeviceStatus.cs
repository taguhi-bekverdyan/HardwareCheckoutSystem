using System.ComponentModel;

namespace HardwareCheckoutSystemAdmin.Models
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