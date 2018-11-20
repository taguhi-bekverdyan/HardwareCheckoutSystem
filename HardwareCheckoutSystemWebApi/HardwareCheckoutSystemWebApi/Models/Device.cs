﻿using HardwareCheckoutSystemWebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Models
{
    public class Device
    {
        [Key, ValidGuid]
        [Required]
        public Guid Id { get; set; }
        [StringLength(50), Required]
        public string SerialNumber { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime? MaxPeriod { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public DeviceStatus Status { get; set; }
        [Required]
        public Permission Permission { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }

    public enum DeviceStatus
    {
        Other,
        StatusOne,
        StatusTwo,
        StatusThree,
        StatusFour,
        StatusFive
    }
}
