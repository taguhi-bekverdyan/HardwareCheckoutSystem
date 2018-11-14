using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public interface IUserService
    {
        Task Insert(User user);
        Task<List<User>> FindAll();
        Task<User> FindById(Guid userId);
        Task Update(User user);
        Task Delete(Guid key);
        Task DeleteBySerialNumber(string serialnumber);
    }
}
