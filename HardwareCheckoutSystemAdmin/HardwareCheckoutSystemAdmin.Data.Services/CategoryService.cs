using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public class CategoryService : ICategoryService
    {
        public async Task Delete(Category category)
        {
            using (DataContext context = new DataContext())
            {
                if (category != null)
                {
                    context.Categories.Attach(category);
                    context.Categories.Remove(category);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteCategoryById(Guid id)
        {
            using (DataContext context = new DataContext())
            {
                Category category = await FindCategoryById(id);
                await Delete(category);
            }
        }

        public async Task<List<Category>> FindAll()
        {
            using (DataContext context = new DataContext())
            {
                return await context.Categories.ToListAsync();
            }
        }

        public async Task<Category> FindCategoryById(Guid id)
        {
            using (DataContext context = new DataContext())
            {
                return await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            }
        }

        public async Task Insert(Category category)
        {
            using (DataContext context = new DataContext())
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Category category)
        {
            using (DataContext context = new DataContext())
            {
                Category temp = await context.Categories.FirstOrDefaultAsync(d => d.Id == category.Id);
                if (temp != null)
                {
                    context.Entry(temp).CurrentValues.SetValues(category);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
