using HCSWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Services
{
  public class UserService
  {
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
      _context = context;
    }

    public async Task Delete(Guid key)
    {
        var userToDelete = (from d in _context.Users
                            where d.Id == key
                            select d).FirstOrDefault();
        _context.Users.Remove(userToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> FindAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> FindById(Guid userId)
    {
        return await _context.Users.FirstOrDefaultAsync(d => d.Id == userId);
    }

    public async Task Insert(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        var userToUpdate = (from d in _context.Users
                            where d.Id == user.Id
                            select d).FirstOrDefault();
        userToUpdate = user;
        await _context.SaveChangesAsync();
    }
  }
}
