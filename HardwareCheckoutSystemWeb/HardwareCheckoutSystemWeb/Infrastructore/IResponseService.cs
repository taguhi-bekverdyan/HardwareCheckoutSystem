using HardwareCheckoutSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWeb.Infrastructore
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
