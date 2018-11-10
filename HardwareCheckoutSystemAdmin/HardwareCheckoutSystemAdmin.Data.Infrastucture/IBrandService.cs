using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Infrastructure
{
    public interface IBrandService
    {
        Task Insert(Brand brand);
        Task<List<Brand>> FindAll();
        Task<Brand> FindById(Guid brandId);
        Task<Brand> FindByName(string name);
        Task Update(Brand brand);
        Task DeleteByKey(Guid key);
        Task Delete(Brand brand);
    }
}
