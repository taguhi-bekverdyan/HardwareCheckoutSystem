using System;
using System.ComponentModel;

namespace HardwareCheckoutSystemAdmin.Models
{
  public enum RequestStatus
  {
    Other,
    Pending,
    Accepted,
    Approved,
    Rejected,
    [Description("Need more information")]
    NeedMoreInfo
  }
}