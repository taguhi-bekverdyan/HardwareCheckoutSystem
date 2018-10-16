using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
  public class UserService : IUserService
  {
    public Task Delete(Guid key)
    {
      throw new NotImplementedException();
    }

    public async Task<List<User>> FindAll()
    {
      using (var context = new DataContext())
      {
        return await context.Users.ToListAsync();
      }
    }

    public Task<User> FindById(Guid userId)
    {
      throw new NotImplementedException();
    }

    public Task Insert(User user)
    {
      throw new NotImplementedException();
    }

    public Task Update(User user)
    {
      throw new NotImplementedException();
    }
  }
}
