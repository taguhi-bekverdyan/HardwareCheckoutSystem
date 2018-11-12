using HardwareCheckoutSystemAdmin.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HardwareCheckoutSystemAdmin.Models
{
  public class Brand : IEquatable<Brand>
  {
    [Key, ValidGuid]
    public Guid Id { get; set; }
    public string Name { get; set; }

    public bool Equals(Brand other)
    {
      return Id == other.Id;
    }
  }
}