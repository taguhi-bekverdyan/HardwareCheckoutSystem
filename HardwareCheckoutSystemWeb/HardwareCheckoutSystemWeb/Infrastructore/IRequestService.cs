using HardwareCheckoutSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWeb.Infrastructore
{
    public interface IRequestService
    {
        Task Insert(Request request);
        Task Update(Request request);
        Task Delete(Request request);
        Task<Request> FindRequestById(Guid id);
        Task<List<Request>> FindAll();
        Task<List<Request>> FindRequestsInPending();
    }
}
