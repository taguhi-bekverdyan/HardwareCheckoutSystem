using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Range(1,99)]
        public int Age { get; set; }
        public string Address { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Person Parent { get; set; }

        public ICollection<Person> Children { get; set; }
    }
}
