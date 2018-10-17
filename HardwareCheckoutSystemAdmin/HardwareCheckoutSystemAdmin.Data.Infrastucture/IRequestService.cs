﻿using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Infrastructure
{
    public interface IRequestService
    {
        Task Insert(Request request);
        Task Update(Request request);
        Task Delete(Request request);
        Task<Request> FindRequestById(Guid id);
        Task<List<Request>> FindAll();
    }
}
