using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Models
{
    public class Brand
    {
        [ValidGuid, Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Device> Devices { get; set; }

        public Brand(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public Brand()
        {

        }
    }
}
