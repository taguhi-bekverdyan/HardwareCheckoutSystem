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

        #region [CREATE]
        public async Task Insert(Category category)
        {
            using (var context = new DataContext())
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }
        }
        #endregion

        #region [READ]
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
        #endregion

        #region [UPDATE]
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
        #endregion

        #region [DELETE]
        public async Task Delete(Category category)
        {
            using (var context = new DataContext())
            {
                var categoryToDelete = (from d in context.Categories
                                        where d.Id == category.Id
                                        select d).FirstOrDefault();
                context.Categories.Remove(categoryToDelete);
                await context.SaveChangesAsync();
            }
        }
        #endregion

        #region [DELETEbyNAME]
        public async Task DeleteByName(string name)
        {
            using (var context = new DataContext())
            {
                var categoryToDelete = (from d in context.Categories
                                        where d.Name == name
                                        select d).FirstOrDefault();
                context.Categories.Remove(categoryToDelete);
                await context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
