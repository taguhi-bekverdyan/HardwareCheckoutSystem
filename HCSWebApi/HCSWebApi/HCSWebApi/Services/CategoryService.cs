
using HCSWebApi.Models;
using HCSWebApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Services
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
            return Task.Factory.StartNew(() => {
                _context.Categories.Attach(category);
                _context.Categories.Remove(category);
                _context.SaveChanges();
            });
        }

        public async Task DeleteById(Guid id)
        {
            Category category = await FindById(id);
            if (category != null)
            {
                await Delete(category);
            }
        }


        public Task<List<Category>> FindAll()
        {
            return Task.Factory.StartNew(() =>
            {
                return _context.Categories.ToList();
            });
        }

        public Task<Category> FindById(Guid id)
        {
            return Task.Factory.StartNew(() => 
            {
                return _context.Categories.FirstOrDefault(d => d.Id == id);
            });
        }

        public Task<Category> FindByName(string name)
        {
            return Task.Factory.StartNew(() => {
                return _context.Categories.FirstOrDefault(b => b.Name == name);
            });
        }

        public Task Insert(Category category)
        {
            return Task.Factory.StartNew(() => {
                _context.Categories.Add(category);
                _context.SaveChanges();
            });
        }

        public async Task Update(Category category)
        {
            Category item = await FindById(category.Id);
            if (item == null)
            {
                return;
            }
            _context.Entry(item).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();
        }
    }
}

