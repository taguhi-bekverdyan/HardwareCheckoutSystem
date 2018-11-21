using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Models
{
    public class Category
    {
        [Key, ValidGuid]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Device> Devices { get; set; }

        public Category()
        {

        }

        public Category(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
