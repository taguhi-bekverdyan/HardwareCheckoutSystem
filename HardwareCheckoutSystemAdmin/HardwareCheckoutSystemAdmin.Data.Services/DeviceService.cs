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
            await Task.Run(()=> {
                using (DataContext context = new DataContext())
                {
                    context.Devices.Attach(device);
                    context.Devices.Remove(device);
                    context.SaveChanges();
                }
            });
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
            //using (DataContext context = new DataContext())
            //{
            //    return await context.Devices
            //        .Include((d) => d.Brand)
            //        .Include((d) => d.Category)
            //        .ToListAsync();
            //}
            return await Task<List<Device>>.Run(()=> {
                using (DataContext context = new DataContext())
                {
                    return context.Devices
                        .Include((d) => d.Brand)
                        .Include((d) => d.Category)
                        .ToListAsync();
                }
            });
        }

        public async Task<Device> FindDeviceById(Guid id)
        {
            return await Task<Device>.Run(()=> {
                using (DataContext context = new DataContext())
                {
                    return context.Devices
                        .Include((d) => d.Brand)
                        .Include((d) => d.Category)
                        .FirstOrDefault(d => d.Id == id);
                }
            });                     
        }

        public async Task<Device> FindDeviceBySerialNumber(string sn)
        {
            return await Task<Device>.Run(()=>{
                using (DataContext context = new DataContext())
                {
                    return context.Devices
                        .Include((d) => d.Brand)
                        .Include((d) => d.Category)
                        .FirstOrDefault(d => d.SerialNumber == sn);
                }
            });
        }

        public async Task Insert(Device newDevice)
        {
            await Task.Run(()=> {
                using (DataContext context = new DataContext())
                {
                    context.Devices.Add(newDevice);
                    context.SaveChanges();
                }
            });

        }

        public async Task Update(Device device)
        {
            await Task.Run(()=> {
                using (DataContext context = new DataContext())
                {
                    Device temp = context.Devices.FirstOrDefault(d => d.Id == device.Id);
                    if (temp != null)
                    {
                        context.Entry(temp).CurrentValues.SetValues(device);
                        context.SaveChanges();
                    }
                }
            });

        }
    }
}
