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
    public class RequestService : IRequestService
    {
        public async Task Delete(Request request)
        {
            using (DataContext context = new DataContext())
            {
                context.Requests.Remove(request);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Request>> FindAll()
        {
            using (DataContext context = new DataContext())
            {
                return await context.Requests.ToListAsync();
            }
        }

        public async Task<Request> FindRequestById(Guid id)
        {
            using (DataContext context = new DataContext())
            {
                return await context.Requests.FirstOrDefaultAsync(r => r.Id == id);
            }
        }

        public async Task Insert(Request request)
        {
            using (DataContext context = new DataContext())
            {
                context.Requests.Add(request);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Request request)
        {
            using (DataContext context = new DataContext())
            {
                Request temp = await context.Requests.FirstOrDefaultAsync(d => d.Id == request.Id);
                if (temp != null)
                {
                    context.Entry(temp).CurrentValues.SetValues(request);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
