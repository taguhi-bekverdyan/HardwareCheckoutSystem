﻿using HCSWebAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Models
{
  public class Request
  {
    [Key, ValidGuid]
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public Device Device { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public string Message { get; set; }
    public Guid? LastResponseId { get; set; }
    public Response LastResponse { get; set; }

    public List<Response> Responses { get; set; }
  }
}
