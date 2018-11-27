﻿using HardwareCheckoutSystemWebApi.Context.Models;
using HardwareCheckoutSystemWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Services
{
    public class ResponseService
    {

        private readonly DataContext _context;

        public ResponseService(DataContext context)
        {
            _context = context;
        }

        public Task Delete(Response response)
        {
            return Task.Factory.StartNew(() =>
            {
                _context.Responses.Attach(response);
                _context.Responses.Remove(response);
                _context.SaveChanges();
            });
        }

        public Task<List<Response>> FindAll()
        {
            return Task<List<Response>>.Factory.StartNew(() =>
            {
                return _context.Responses.ToList();
            });
        }

        public Task<Response> FindResponseById(Guid id)
        {
            return Task<Response>.Factory.StartNew(() =>
            {
                return _context.Responses.FirstOrDefault(r => r.Id == id);
            });
        }

        public Task Insert(Response response)
        {
            return Task.Factory.StartNew(() =>
            {
                _context.Responses.Add(response);
                _context.SaveChanges();
            });
        }

        public async Task<bool> Update(Response response)
        {
            Response item = await FindResponseById(response.Id);
            if (item == null) { return false; }
            _context.Entry(item).CurrentValues.SetValues(response);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
