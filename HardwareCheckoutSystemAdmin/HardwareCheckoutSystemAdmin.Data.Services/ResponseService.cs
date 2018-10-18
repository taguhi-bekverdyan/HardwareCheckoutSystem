using HardwareCheckoutSystemAdmin.Data.Infrastructure;
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
    public async Task Delete(Guid key)
    {
      using (var context = new DataContext())
      {
        var responseToDelete = (from d in context.Responses
                              where d.Id == key
                              select d).FirstOrDefault();
        context.Responses.Remove(responseToDelete);
        await context.SaveChangesAsync();
      }
    }

    public async Task<List<Response>> FindAll()
    {
      using (var context = new DataContext())
      {
        return await context.Responses.ToListAsync();
      }
    }

    public async Task<Response> FindById(Guid responseId)
    {
      using (var context = new DataContext())
      {
        return await context.Responses.FirstOrDefaultAsync(d => d.Id == responseId);
      }
    }

    public async Task Insert(Response response)
    {
      using (var context = new DataContext())
      {
        context.Responses.Add(response);
        await context.SaveChangesAsync();
      }
    }

    public async Task Update(Response response)
    {
      using (var context = new DataContext())
      {
        var responseToUpdate = (from d in context.Responses
                              where d.Id == response.Id
                              select d).FirstOrDefault();
        responseToUpdate = response;
        await context.SaveChangesAsync();
      }
    }
  }
}
