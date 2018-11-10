using HardwareCheckoutSystemAdmin.Models;
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
        Task<List<Request>> FindAll();
        Task<Request> FindById(Guid rerquestId);
        Task Update(Request request);
        Task Delete(Guid key);
    }
}

