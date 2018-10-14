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




    #endregion

    #region [DELETE]



    #endregion


  }
}
