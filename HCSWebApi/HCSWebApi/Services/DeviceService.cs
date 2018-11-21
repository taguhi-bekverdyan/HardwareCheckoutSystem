
using HCSWebApi.Models;
using HCSWebApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebApi.Services
{
    public class DeviceService
    {
        private readonly DataContext _context;

        public DeviceService(DataContext context)
        {
            _context = context;
        }

        public Task Delete(Device device)
        {
            return Task.Factory.StartNew(() => 
            {
                _context.Devices.Attach(device);
                _context.Devices.Remove(device);
                _context.SaveChanges();
            });
        }

        public async Task DeleteById(Guid id)
        {
            var device  = await FindById(id);
            if (device != null)
            {
                await Delete(device);
            }
        }


        public Task<List<Device>> FindAll()
        {
            return Task.Factory.StartNew(() => 
            {
                return _context.Devices.ToList();
            });
        }

        public Task<Device> FindById(Guid id)
        {
            return Task.Factory.StartNew(() =>
            {
                return _context.Devices.FirstOrDefault(d => d.Id == id);
            });
        }
        

        public Task Insert(Device device)
        {
            return Task.Factory.StartNew(() =>
            {
                _context.Devices.Add(device);
                _context.SaveChanges();
            });
        }

        public async Task Update(Device device)
        {
            Device item = await FindById(device.Id);
            if (item == null)
            {
                return;
            }
            _context.Entry(item).CurrentValues.SetValues(device);
            await _context.SaveChangesAsync();
        }


        #region [SN]
        public async Task<Device> FindBySn(string sn)
        {
            return await _context.Devices.FirstOrDefaultAsync(d => d.SerialNumber == sn);
        }
        #endregion
    }
}




