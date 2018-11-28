using HardwareCheckoutSystemWebApi.Context.Models;
using HardwareCheckoutSystemWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Services
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
            return Task.Factory.StartNew(() =>
            {
                _context.Requests.Attach(request);
                _context.Requests.Remove(request);
                _context.SaveChanges();
            });
        }

        public async Task DeleteRequestById(Guid id)
        {
            Request request = await FindRequestById(id);
            await Delete(request);
        }

        public Task<List<Request>> FindAll()
        {
            return Task<List<Request>>.Factory.StartNew(() =>
            {
                var requests = _context.Requests
                .Include(r => r.User)
                .Include(r => r.Device)
                .Include(r => r.Responses)               
                .ToList();

                foreach (var item in requests)
                {
                    item.User.Requests = null;
                    
                    foreach (var item1 in item.Responses)
                    {
                        item1.Request = null;
                        item1.User = null;
                        item.LastResponseId = item1.Id;
                    }
                }

                return requests;
            });
        }

        public Task<Request> FindRequestById(Guid id)
        {
            return Task<Request>.Factory.StartNew(() =>
            {
                Request request = _context.Requests
                .Include(r => r.User)
                .Include(r => r.Device)
                .Include(r => r.Responses)
                .FirstOrDefault(r => r.Id == id);

                request.User.Requests = null;

                foreach (var item in request.Responses)
                {
                    item.Request = null;
                    item.User = null;
                    request.LastResponseId = item.Id;
                }

                return request;
            });
        }

        public Task Insert(Request request)
        {
            return Task.Factory.StartNew(() =>
            {
                _context.Requests.Add(request);
                _context.SaveChanges();
            });
        }
        
        public async Task<bool> Update(Request request)
        {
            Request item = await FindRequestById(request.Id);
            if (item == null) { return false; }
            _context.Entry(item).CurrentValues.SetValues(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

