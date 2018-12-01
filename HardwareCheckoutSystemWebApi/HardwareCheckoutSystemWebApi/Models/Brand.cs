using HardwareCheckoutSystemWebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Models
{
    public class Brand
    {
        [Key, ValidGuid, Required]
        public Guid Id { get; set; }
        public string Name { get; set; }

        
        public Brand()
        {
            Id = Guid.NewGuid();
            
        }

       

    }
}
