using HCSWebAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Models
{
  public class User
  {
    [Key, ValidGuid]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public DateTime Birthdate { get; set; }
    public string TelNumber { get; set; }
    public Permission Permission { get; set; }
    // TODO: Create method for image input
    public byte[] AvatarImage { get; set; }
    public string Occupation { get; set; }
  }
}
