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
  public class BrandService : IBrandService
  {
    public async Task Delete(Guid key)
    {
      using (var context = new DataContext())
      {
        var brandToDelete = (from d in context.Brands
                              where d.Id == key
                              select d).FirstOrDefault();
        context.Brands.Remove(brandToDelete);
        await context.SaveChangesAsync();
      }
    }

    public async Task<List<Brand>> FindAll()
    {
      using (var context = new DataContext())
      {
        return await context.Brands.ToListAsync();
      }
    }

    public async Task<Brand> FindById(Guid brandId)
    {
      using (var context = new DataContext())
      {
        return await context.Brands.FirstOrDefaultAsync(d => d.Id == brandId);
      }
    }

    public async Task Insert(Brand brand)
    {
      using (var context = new DataContext())
      {
        context.Brands.Add(brand);
        await context.SaveChangesAsync();
      }
    }

    public async Task Update(Brand brand)
    {
      using (var context = new DataContext())
      {
        var brandToUpdate = (from d in context.Brands
                              where d.Id == brand.Id
                              select d).FirstOrDefault();
        brandToUpdate = brand;
        await context.SaveChangesAsync();
      }
    }
  }
}
