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
        Task Update(Category category);
        Task Delete(Category category);
        Task<Category> FindCategoryByIdAsync(Guid id);
        Task<Category> FindCategoryByName(string name);
        Task<List<Category>> FindAll();
        Task DeleteCategoryById(Guid id);
    }
}
