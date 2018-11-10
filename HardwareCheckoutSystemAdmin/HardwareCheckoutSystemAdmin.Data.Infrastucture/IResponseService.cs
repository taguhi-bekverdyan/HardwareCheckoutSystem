using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Infrastructure
{
    public interface IResponseService
    {
        Task Insert(Response response);
        Task<List<Response>> FindAll();
        Task<Response> FindById(Guid responseId);
        Task Update(Response response);
        Task Delete(Guid key);
    }
}
