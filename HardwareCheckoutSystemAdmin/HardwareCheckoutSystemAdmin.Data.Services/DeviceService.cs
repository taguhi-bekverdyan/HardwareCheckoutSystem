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
  public class DeviceService : IDeviceService
  {
    #region [CREATE]
    public async Task Insert(Device device)
    {
      using (var context = new DataContext())
      {
        context.Devices.Add(device);
        await context.SaveChangesAsync();
      }
    }
    #endregion

    #region [READ]
    public async Task<List<Device>> FindAll()
    {
      using (var context = new DataContext())
      {
        return await context.Devices.ToListAsync();
      }
    }

    public async Task<Device> FindOne(int deviceSn)
    {
      using (var context = new DataContext())
      {
        return await context.Devices.FirstOrDefaultAsync(d => d.SN == deviceSn);
      }
    }
    #endregion

    #region [UPDATE]
    public async Task Update(int key)
    {
      using (var context = new DataContext())
      {
        var deviceToUpdate = (from d in context.Devices
                             where d.SN == key
                             select d).FirstOrDefault();
        deviceToUpdate.Brand = "Acer";
        await context.SaveChangesAsync();
      }
    }



    #endregion

    #region [DELETE]
    public async Task Delete(int key)
    {
      using (var context = new DataContext())
      {
        var deviceToDelete = (from d in context.Devices
                              where d.SN == key
                              select d).FirstOrDefault();
        context.Devices.Remove(deviceToDelete);
        await context.SaveChangesAsync();
      }
    }


    #endregion


  }
}
