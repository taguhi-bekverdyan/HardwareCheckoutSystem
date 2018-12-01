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
        Task Update(Response response);
        Task Delete(Response response);
        Task<Response> FindResponseById(Guid id);
        Task<List<Response>> FindAll();
    }
}
