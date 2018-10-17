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
        public async Task Delete(Brand brand)
        {
            using (DataContext context = new DataContext())
            {
                context.Brands.Remove(brand);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Brand>> FindAll()
        {
            using (DataContext context = new DataContext())
            {
                return await context.Brands.ToListAsync();
            }
        }

        public async Task<Brand> FindBrandByIdAsync(Guid id)
        {
            using (DataContext context = new DataContext())
            {
                return await context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            }
        }

        public async Task Insert(Brand brand)
        {
            using (DataContext context = new DataContext())
            {
                context.Brands.Add(brand);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Brand brand)
        {
            using (DataContext context = new DataContext())
            {
                Brand temp = await context.Brands.FirstOrDefaultAsync(d => d.Id == brand.Id);
                if (temp != null)
                {
                    context.Entry(temp).CurrentValues.SetValues(brand);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
