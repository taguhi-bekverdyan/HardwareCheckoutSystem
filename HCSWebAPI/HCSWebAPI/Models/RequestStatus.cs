using System.ComponentModel;

namespace HCSWebAPI.Models
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