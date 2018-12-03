using HardwareCheckoutSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWeb.Infrastructore
{
    public interface IBrandService
    {
        Task Insert(Brand brand);
        Task Update(Brand brand);
        Task Delete(Brand brand);
        Task<Brand> FindBrandById(Guid id);
        Task<Brand> FindBrandByName(string name);
        Task<List<Brand>> FindAll();
        Task DeleteBrandById(Guid id);
    }
}
