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
                return _context.Users
                .ToList();
            });
        }

        public Task<User> FindUserById(Guid id)
        {
            return Task<User>.Factory.StartNew(() =>
            {
                return _context.Users
                
                .FirstOrDefault(u => u.Id == id);
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

