
using HCSWebApi.Models;
using HCSWebApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Services
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
            return Task.Factory.StartNew(() => {
                _context.Brands.Attach(brand);
                _context.Brands.Remove(brand);
                _context.SaveChanges();
            });
        }

        public async Task DeleteById(Guid id)
        {
            Brand brand = await FindById(id);
            if (brand != null) { await Delete(brand); }
        }


        public Task<List<Brand>> FindAll()
        {
            return Task.Factory.StartNew(() => {
                return _context.Brands.ToList();
            });
        }

        public Task<Brand> FindById(Guid id)
        {
            return Task.Factory.StartNew(() => {
                return _context.Brands.FirstOrDefault(d => d.Id == id);
            });
        }

        public Task<Brand> FindByName(string name)
        {
            return Task.Factory.StartNew(() => {
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

        public async Task Update(Brand brand)
        {
            Brand item = await FindById(brand.Id);
            if (item == null) { return; }
            _context.Entry(item).CurrentValues.SetValues(brand);
            await _context.SaveChangesAsync();
        }
    }
}
