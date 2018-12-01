using HardwareCheckoutSystemWebApi.Context.Models;
using HardwareCheckoutSystemWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Services
{
    public class CategoryService
    {

        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public Task Delete(Category category)
        {
            return Task.Factory.StartNew(()=> {
                _context.Categories.Attach(category);
                _context.Categories.Remove(category);
                _context.SaveChanges();
            });
        }

        public async Task DeleteCategoryById(Guid id)
        {

            Category item = await FindCategoryById(id);
            if (item != null)
            {
                await Delete(item);
            }

        }

        public Task<List<Category>> FindAll()
        {
            return Task<List<Category>>.Factory.StartNew(()=> {
                return _context.Categories.ToList();
            });
        }

        public Task<Category> FindCategoryById(Guid id)
        {
            return Task.Factory.StartNew(() => {
                Category item = _context.Categories.FirstOrDefault(c => c.Id == id);
                return item;
            });
        }

        public Task<Category> FindCategoryByName(string name)
        {
            return Task.Factory.StartNew(() => {
                Category item = _context.Categories.FirstOrDefault(c => c.Name == name);
                return item;
            });
        }

        public Task Insert(Category category)
        {
            return Task.Factory.StartNew(()=> {
                _context.Categories.Add(category);
                _context.SaveChanges();
            });
        }

        public async Task<bool> Update(Category category)
        {
            Category item = await FindCategoryById(category.Id);
            if (item == null) { return false; }
            _context.Entry(item).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
