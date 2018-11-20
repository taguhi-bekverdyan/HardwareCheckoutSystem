using HCSWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Services
{
  public class BrandService
  {
    private readonly DataContext _context;

    public BrandService(DataContext context)
    {
      _context = context;
    }

    public async Task<List<Brand>> FindAll()
    {
      return await _context.Brands.ToListAsync();
    }

    public async Task<Brand> FindById(Guid Id)
    {
      return await _context.Brands.FirstOrDefaultAsync(d => d.Id == Id);
    }

    public async Task Insert(Brand brand)
    {
      _context.Brands.Add(brand);
      await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid key)
    {
      var brandToDelete = (from d in _context.Brands
                           where d.Id == key
                           select d).FirstOrDefault();
      _context.Brands.Remove(brandToDelete);
      await _context.SaveChangesAsync();
    }

    public async Task Update(Brand brand)
    {
      var brandToUpdate = (from d in _context.Brands
                           where d.Id == brand.Id
                           select d).FirstOrDefault();
      brandToUpdate = brand;
      await _context.SaveChangesAsync();
    }
  }
}
