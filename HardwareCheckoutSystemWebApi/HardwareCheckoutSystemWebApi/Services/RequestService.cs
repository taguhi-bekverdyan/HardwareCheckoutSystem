using HardwareCheckoutSystemWebApi.Context.Models;
using HardwareCheckoutSystemWebApi.Models;
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

        public Task<List<Request>> FindAll()
        {
            return Task<List<Request>>.Factory.StartNew(() =>
            {
                return _context.Requests.ToList();
            });
        }

        public Task<Request> FindRequestById(Guid id)
        {
            return Task<Request>.Factory.StartNew(() =>
            {
                return _context.Requests.FirstOrDefault(r => r.Id == id);
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

