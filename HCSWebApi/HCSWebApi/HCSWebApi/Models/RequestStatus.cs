using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Models
{
    public enum RequestStatus
    {
        Other,
        Pending,
        Accepted,
        Approved,
        Rejected,
        [Description("Need more information")]
        NeedMoreInfo
    }
}
