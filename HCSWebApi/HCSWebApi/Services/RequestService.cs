
using Microsoft.EntityFrameworkCore;
using HCSWebApi.Models;
using HCSWebApi.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Services
{
    public class RequestService
    {
        private readonly DataContext _context;

        public RequestService(DataContext context)
        {
            _context = context;
        }

        public Task Delete(Request request)
        {
            return Task.Factory.StartNew(() => {
                _context.Requests.Attach(request);
                _context.Requests.Remove(request);
                _context.SaveChanges();
            });
        }

        public async Task DeleteById(Guid id)
        {
            Request request = await FindById(id);
            if (request != null)
            {
                await Delete(request);
            }
        }


        public Task<List<Request>> FindAll()
        {
            return Task.Factory.StartNew(() => {
                return _context.Requests.ToList();
            });
        }

        public Task<Request> FindById(Guid id)
        {
            return Task.Factory.StartNew(() => {
                return _context.Requests.FirstOrDefault(d => d.Id == id);
            });
        }

        public Task Insert(Request request)
        {
            return Task.Factory.StartNew(() => {
                _context.Requests.Add(request);
                _context.SaveChanges();
            });
        }

        public async Task Update(Request request)
        {
            Request item = await FindById(request.Id);
            if (item == null)
            { return;
            }
            _context.Entry(item).CurrentValues.SetValues(request);
            await _context.SaveChangesAsync();
        }
    }
}
