using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Models.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace HardwareCheckoutSystemAdmin.Data
{
  public class Response
  {
    [Key, ValidGuid]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public RequestStatus NewStatus { get; set; }   
    public Guid UserId { get; set; }
    public User User { get; set; }
  }
}