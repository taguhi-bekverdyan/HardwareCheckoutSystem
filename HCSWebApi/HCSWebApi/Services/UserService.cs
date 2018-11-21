
using HCSWebApi.Models;
using HCSWebApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Services
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

        public async Task DeleteById(Guid id)
        {
            User user = await FindById(id);
            if (user != null) { await Delete(user); }
        }


        public Task<List<User>> FindAll()
        {
            return Task.Factory.StartNew(() =>
            {
                return _context.Users.ToList();
            });
        }

        public Task<User> FindById(Guid id)
        {
            return Task.Factory.StartNew(() =>
            {
                return _context.Users.FirstOrDefault(d => d.Id == id);
            });
        }


        public Task Insert(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            });
        }

        public async Task Update(User user)
        {
            User item = await FindById(user.Id);
            if (item == null)
            {
                return;
            }
            _context.Entry(item).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBySerialNumber(string serialnumber)
        {
            var userToDelete = (from d in _context.Users
                                where d.SerialNumber == serialnumber
                                select d).FirstOrDefault();
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
