using HCSWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Services
{
  public class RequestService
  {
    private readonly DataContext _context;

    public RequestService(DataContext context)
    {
      _context = context;
    }

    public async Task Delete(Guid key)
    {
        var requestToDelete = (from d in _context.Requests
                               where d.Id == key
                               select d).FirstOrDefault();
        _context.Requests.Remove(requestToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Request>> FindAll()
    {
        return await _context.Requests.ToListAsync();
    }

    public async Task<Request> FindById(Guid requestId)
    {
        return await _context.Requests.FirstOrDefaultAsync(d => d.Id == requestId);
    }

    public async Task Insert(Request request)
    {
        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Request request)
    {
        var requestToUpdate = (from d in _context.Requests
                               where d.Id == request.Id
                               select d).FirstOrDefault();
        requestToUpdate = request;
        await _context.SaveChangesAsync();
    }
  }
}
