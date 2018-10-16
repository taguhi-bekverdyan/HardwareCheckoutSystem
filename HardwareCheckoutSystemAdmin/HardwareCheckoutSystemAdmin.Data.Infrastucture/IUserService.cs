using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Infrastructure
{
  public interface IUserService
  {
    Task Insert(User user);
    Task<List<User>> FindAll();
    Task<User> FindById(Guid userId);
    Task Update(User user);
    Task Delete(Guid key);
  }
}
