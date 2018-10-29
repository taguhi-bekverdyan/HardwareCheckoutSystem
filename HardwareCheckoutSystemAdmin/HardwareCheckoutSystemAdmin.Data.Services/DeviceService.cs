using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public class DeviceService : IDeviceService
    {
        
        public async Task Delete(Device device)
        {
            using (DataContext context = new DataContext())
            {
                context.Devices.Attach(device);
                context.Devices.Remove(device);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteDeviceById(Guid id)
        {
            using (DataContext context = new DataContext())
            {
                Device device = await FindDeviceById(id);
                await Delete(device);
            }
        }

        public async Task<List<Device>> FindAll()
        {
            using (DataContext context = new DataContext())
            {
                return await context.Devices
                    .Include((d) => d.Brand)
                    .Include((d) => d.Category)
                    .ToListAsync();
            }
        }

        public async Task<Device> FindDeviceById(Guid id)
        {          
            using (DataContext context = new DataContext())
            {
                return await context.Devices
                    .Include((d) => d.Brand)
                    .Include((d) => d.Category)
                    .FirstOrDefaultAsync(d => d.Id == id);
            }           
        }

        public async Task<Device> FindDeviceBySerialNumber(string sn)
        {
            using (DataContext context = new DataContext())
            {
                return await context.Devices
                    .Include((d) => d.Brand)
                    .Include((d) => d.Category)
                    .FirstOrDefaultAsync(d => d.SerialNumber == sn);
            }
        }

        public async Task Insert(Device newDevice)
        {
            using (DataContext context = new DataContext())
            {
                context.Devices.Add(newDevice);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Device device)
        {
            using (DataContext context = new DataContext()) {
                Device temp = await context.Devices.FirstOrDefaultAsync(d=>d.Id == device.Id);
                if (temp != null)
                {                   
                    context.Entry(temp).CurrentValues.SetValues(device);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
