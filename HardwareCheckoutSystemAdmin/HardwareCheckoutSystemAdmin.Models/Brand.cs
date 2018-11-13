﻿using HardwareCheckoutSystemAdmin.Models.HelperAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class Brand:IEquatable<Brand>
    {
        [Key, ValidGuid, Required]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Device> Devices { get; set; }

        public bool Equals(Brand other)
        {
            return Id == other.Id;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}