using HardwareCheckoutSystemWebApi.Context.Models;
using HardwareCheckoutSystemWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Services
{
    public class UserService
    {

        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public Task Delete(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                _context.Users.Attach(user);
                _context.Users.Remove(user);
                _context.SaveChanges();
            });
        }

        public async Task DeleteUserById(Guid id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id.Equals(id));
            if (user == null) { return; }
            await Delete(user);
        }

        public Task<List<User>> FindAll()
        {
            return Task<List<User>>.Factory.StartNew(() =>
            {
                List<User> users = _context.Users
                .Include(u => u.Requests)                
                .ToList();

                foreach (var item in users)
                {
                    foreach (var item1 in item.Requests)
                    {
                        item1.User = null;
                    }
                }

                return users;
            });
        }

        public Task<User> FindUserById(Guid id)
        {
            return Task<User>.Factory.StartNew(() =>
            {
                User user = _context.Users
                .Include(u => u.Requests)
                .FirstOrDefault(u => u.Id == id);

                foreach (var item in user.Requests)
                {
                    item.User = null;
                }

                return user;
            });
        }

        public Task Insert(User newUser)
        {
            return Task.Factory.StartNew(() =>
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();
            });
        }
        
        public async Task<bool> Update(User user)
        {
            User item = await FindUserById(user.Id);
            if (item == null) { return false; }
            _context.Entry(item).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

