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
    public class ResponseService : IResponseService
    {
        public async Task Delete(Response response)
        {
            using (DataContext context = new DataContext())
            {
                context.Responses.Remove(response);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Response>> FindAll()
        {
            using (DataContext context = new DataContext())
            {
                return await context.Responses.ToListAsync();
            }
        }

        public async Task<Response> FindResponseById(Guid id)
        {
            using (DataContext context = new DataContext())
            {
                return await context.Responses.FirstOrDefaultAsync(r => r.Id == id);
            }
        }

        public async Task Insert(Response response)
        {
            using (DataContext context = new DataContext())
            {
                context.Responses.Add(response);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Response response)
        {
            using (DataContext context = new DataContext())
            {
                Response temp = await context.Responses.FirstOrDefaultAsync(d => d.Id == response.Id);
                if (temp != null)
                {
                    context.Entry(temp).CurrentValues.SetValues(response);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
