using HardwareCheckoutSystemAdmin.Models.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace HardwareCheckoutSystemAdmin.Models
{
  public class Category
  {
    [Key, ValidGuid]
    public Guid Id { get; set; }
    public string Name { get; set; }
  }
}