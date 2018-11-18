using HCSWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Services
{
  public class CategoryService
  {
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
      _context = context;
    }

    public async Task Delete(Guid key)
    {
        var categoryToDelete = (from d in _context.Categories
                                where d.Id == key
                                select d).FirstOrDefault();
        _context.Categories.Remove(categoryToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Category>> FindAll()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> FindById(Guid categoryId)
    {
        return await _context.Categories.FirstOrDefaultAsync(d => d.Id == categoryId);
    }

    public async Task Insert(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Category category)
    {
        var categoryToUpdate = (from d in _context.Categories
                                where d.Id == category.Id
                                select d).FirstOrDefault();
        categoryToUpdate = category;
        await _context.SaveChangesAsync();
    }
  }
}
