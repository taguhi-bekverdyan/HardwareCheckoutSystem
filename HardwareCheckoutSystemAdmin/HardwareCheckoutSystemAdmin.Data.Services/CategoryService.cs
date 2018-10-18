using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
  public class CategoryService : ICategoryService
  {
    public async Task Delete(Guid key)
    {
      using (var context = new DataContext())
      {
        var categoryToDelete = (from d in context.Categories
                              where d.Id == key
                              select d).FirstOrDefault();
        context.Categories.Remove(categoryToDelete);
        await context.SaveChangesAsync();
      }
    }

    public async Task<List<Category>> FindAll()
    {
      using (var context = new DataContext())
      {
        return await context.Categories.ToListAsync();
      }
    }

    public async Task<Category> FindById(Guid categoryId)
    {
      using (var context = new DataContext())
      {
        return await context.Categories.FirstOrDefaultAsync(d => d.Id == categoryId);
      }
    }

    public async Task Insert(Category category)
    {
      using (var context = new DataContext())
      {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
      }
    }

    public async Task Update(Category category)
    {
      using (var context = new DataContext())
      {
        var categoryToUpdate = (from d in context.Categories
                              where d.Id == category.Id
                              select d).FirstOrDefault();
        categoryToUpdate = category;
        await context.SaveChangesAsync();
      }
    }
  }
}
