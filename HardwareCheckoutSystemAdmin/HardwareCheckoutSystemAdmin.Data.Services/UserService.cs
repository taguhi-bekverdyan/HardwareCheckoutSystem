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
    public class UserService : IUserService
    {
        public async Task Delete(User user)
        {
            using (DataContext context = new DataContext())
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> FindAll()
        {
            using (DataContext context = new DataContext())
            {
                return await context.Users.ToListAsync();
            }
        }

        public async Task<User> FindUserById(Guid id)
        {
            using (DataContext context = new DataContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
        }

        public async Task Insert(User newUser)
        {
            using (DataContext context = new DataContext())
            {
                context.Users.Add(newUser);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(User updated)
        {
            using (DataContext context = new DataContext())
            {
                User temp = await context.Users.FirstOrDefaultAsync(d => d.Id == updated.Id);
                if (temp != null)
                {
                    context.Entry(temp).CurrentValues.SetValues(updated);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
