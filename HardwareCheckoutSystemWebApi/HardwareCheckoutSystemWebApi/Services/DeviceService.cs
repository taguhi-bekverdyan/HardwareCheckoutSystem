using HardwareCheckoutSystemWebApi.Context.Models;
using HardwareCheckoutSystemWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Services
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
            return Task.Factory.StartNew(()=> {
                _context.Devices.Attach(device);
                _context.Devices.Remove(device);
                _context.SaveChanges();
            });
        }

        public async Task DeleteDeviceById(Guid id)
        {
            Device device = _context.Devices.FirstOrDefault(d => d.Id == id);
            if (device == null) { return; }
            await Delete(device);
        }

        public Task<List<Device>> FindAll()
        {
            return Task<List<Device>>.Factory.StartNew(() =>
            {
                List<Device> devices = _context.Devices
                .Include(d=>d.Brand)
                .Include(d => d.Category)
                .ToList();

                return devices;
            });
        }

        public Task<Device> FindDeviceById(Guid id)
        {
            return Task<Device>.Factory.StartNew(() => {
                return _context.Devices
                .Include(d => d.Brand)
                .Include(d => d.Category)
                .FirstOrDefault(d => d.Id == id);
            });
        }

        public Task<Device> FindDeviceBySerialNumber(string sn)
        {
            return Task<Device>.Factory.StartNew(() => {
                return _context.Devices

                .FirstOrDefault(d => d.SerialNumber == sn);
            });
        }

        public Task Insert(Device newDevice)
        {
            return Task.Factory.StartNew(() => {
                _context.Devices.Add(newDevice);
                _context.SaveChanges();
            });
        }

        public async Task<bool> Update(Device device)
        {
            Device item = await FindDeviceById(device.Id);
            if (item == null) { return false; }
            _context.Entry(item).CurrentValues.SetValues(device);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
