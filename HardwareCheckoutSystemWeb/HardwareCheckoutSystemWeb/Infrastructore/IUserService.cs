using HardwareCheckoutSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWeb.Infrastructore
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
