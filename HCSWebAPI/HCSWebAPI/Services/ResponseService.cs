using HCSWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Services
{
  public class ResponseService
  {
    private readonly DataContext _context;

    public ResponseService(DataContext context)
    {
      _context = context;
    }

    public async Task Delete(Guid key)
    {
        var responseToDelete = (from d in _context.Responses
                                where d.Id == key
                                select d).FirstOrDefault();
        _context.Responses.Remove(responseToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Response>> FindAll()
    {
        return await _context.Responses.ToListAsync();
    }

    public async Task<Response> FindById(Guid responseId)
    {
        return await _context.Responses.FirstOrDefaultAsync(d => d.Id == responseId);
    }

    public async Task Insert(Response response)
    {
        _context.Responses.Add(response);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Response response)
    {
        var responseToUpdate = (from d in _context.Responses
                                where d.Id == response.Id
                                select d).FirstOrDefault();
        responseToUpdate = response;
        await _context.SaveChangesAsync();
    }
  }
}
