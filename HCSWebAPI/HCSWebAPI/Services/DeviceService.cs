using HCSWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCSWebAPI.Services
{
  public class DeviceService
  {
    private readonly DataContext _context;

    public DeviceService(DataContext context)
    {
      this._context = context;
    }

    #region [CREATE]
    public async Task Insert(Device device)
    {
      _context.Devices.Add(device);
      await _context.SaveChangesAsync();
    }
    #endregion

    #region [READ]
    public async Task<List<Device>> FindAll()
    {
      return await _context.Devices.Include(d => d.Brand).Include(d => d.Category).ToListAsync();
    }

    public async Task<Device> FindById(Guid deviceId)
    {
      return await _context.Devices.Include(d => d.Brand).Include(d => d.Category).FirstOrDefaultAsync(d => d.Id == deviceId);
    }

    public async Task<Device> FindBySn(string sn)
    {
      return await _context.Devices.Include(d => d.Brand).Include(d => d.Category).FirstOrDefaultAsync(d => d.SerialNumber == sn);
    }
    #endregion

    #region [UPDATE]
    public async Task Update(Device device)
    {
      var deviceToUpdate = (from d in _context.Devices
                            where d.Id == device.Id
                            select d).FirstOrDefault();
      deviceToUpdate = device;
      await _context.SaveChangesAsync();
    }
    #endregion

    #region [DELETE]
    public async Task Delete(Guid key)
    {
      var deviceToDelete = (from d in _context.Devices
                            where d.Id == key
                            select d).FirstOrDefault();
      _context.Devices.Remove(deviceToDelete);
      await _context.SaveChangesAsync();
    }
    #endregion
  }
}
