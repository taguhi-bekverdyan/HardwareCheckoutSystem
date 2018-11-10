using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Infrastructure
{
    public interface ICategoryService
    {
        Task Insert(Category category);
        Task<List<Category>> FindAll();
        Task<Category> FindById(Guid catrgoryId);
        Task Update(Category category);
        Task Delete(Category category);
        Task DeleteByName(string name);
    }
}