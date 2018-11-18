﻿using HCSWebAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Models
{
  public class Category
  {
    [Key, ValidGuid]
    public Guid Id { get; set; }
    public string Name { get; set; }
  }
}
