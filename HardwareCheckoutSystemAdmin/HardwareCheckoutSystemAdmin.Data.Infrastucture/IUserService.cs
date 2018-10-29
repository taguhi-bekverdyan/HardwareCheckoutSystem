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
        Task Insert(User newUser);
        Task<User> FindUserById(Guid id);
        Task Update(User updated);
        Task Delete(User user);
        Task<List<User>> FindAll();
        Task DeleteUserById(Guid id);
    }
}
