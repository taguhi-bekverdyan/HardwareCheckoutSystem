
using HCSWebApi.Models;
using HCSWebApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Services
{
    public class ResponseService
    {
        private readonly DataContext _context;

        public ResponseService(DataContext context)
        {
            _context = context;
        }

        public Task Delete(Response response)
        {
            return Task.Factory.StartNew(() => 
            {
                _context.Responses.Attach(response);
                _context.Responses.Remove(response);
                _context.SaveChanges();
            });
        }

        public async Task DeleteById(Guid id)
        {
            Response response = await FindById(id);
            if (response != null) { await Delete(response); }
        }


        public Task<List<Response>> FindAll()
        {
            return Task.Factory.StartNew(() => 
            {
                return _context.Responses.ToList();
            });
        }

        public Task<Response> FindById(Guid id)
        {
            return Task.Factory.StartNew(() => 
            {
                return _context.Responses.FirstOrDefault(d => d.Id == id);
            });
        }

        public Task Insert(Response response)
        {
            return Task.Factory.StartNew(() => {
                _context.Responses.Add(response);
                _context.SaveChanges();
            });
        }

        public async Task Update(Response response)
        {
            Response item = await FindById(response.Id);
            if (item == null)
            {
                return;
            }
            _context.Entry(item).CurrentValues.SetValues(response);
            await _context.SaveChangesAsync();
        }
    }
}
