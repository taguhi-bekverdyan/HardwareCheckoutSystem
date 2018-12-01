using HardwareCheckoutSystemWebApi.Context.Models;
using HardwareCheckoutSystemWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Services
{
    public class BrandService
    {
        private readonly DataContext _context;

        public BrandService(DataContext context)
        {
            _context = context;
        }

        public Task Delete(Brand brand)
        {
            return Task.Factory.StartNew(()=> {
                _context.Brands.Attach(brand);
                _context.Brands.Remove(brand);
                _context.SaveChanges();
            });
        }

        public async Task DeleteBrandById(Guid id)
        {
            Brand brand = await FindBrandById(id);
            if (brand != null) { await Delete(brand); }
        }


        public Task<List<Brand>> FindAll()
        {
            return Task.Factory.StartNew(()=> {
                List<Brand> categories = _context.Brands
                .ToList();
                return categories;
            });
        }

        public Task<Brand> FindBrandById(Guid id)
        {
            return Task.Factory.StartNew(()=> {
                return _context.Brands.FirstOrDefault(d=> d.Id == id);
            });
        }

        public Task<Brand> FindBrandByName(string name)
        {
            return Task.Factory.StartNew(()=> {
                return _context.Brands.FirstOrDefault(b => b.Name == name);
            });
        }

        public Task Insert(Brand brand)
        {
            return Task.Factory.StartNew(() => {
                _context.Brands.Add(brand);
                _context.SaveChanges();
            });
        }

        public async Task<bool> Update(Brand brand)
        {
            Brand item = await FindBrandById(brand.Id);
            if (item == null) { return false; }
            _context.Entry(item).CurrentValues.SetValues(brand);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
