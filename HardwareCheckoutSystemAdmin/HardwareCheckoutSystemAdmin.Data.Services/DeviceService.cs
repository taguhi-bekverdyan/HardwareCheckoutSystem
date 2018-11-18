using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Data.WebAPI;
using HardwareCheckoutSystemAdmin.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
  public class DeviceService : IDeviceService
  {
    private readonly IRestService _restService;
    private readonly RestRequest _restRequest;

    public DeviceService(IRestService restService)
    {
      _restService = restService;
      _restRequest = new RestRequest();
    }

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
      _restRequest.Resource = "api/devices";

      //List<Device> list = new List<Device>();
      //var asyncHandle = _restService.Client.ExecuteAsync<List<Device>>(_restRequest, response =>
      //{
      //  list = response.Data;
      //});

      //Thread.Sleep(1000);
      //return list;

      return _restService.Client.Execute<List<Device>>(_restRequest).Data;
    }

    public async Task<Device> FindById(Guid deviceId)
    {
      using (var context = new DataContext())
      {
        return await context.Devices.Include(d => d.Brand).Include(d => d.Category).FirstOrDefaultAsync(d => d.Id == deviceId);
      }
    }

    public async Task<Device> FindBySn(string sn)
    {
      using (var context = new DataContext())
      {
        return await context.Devices.Include(d => d.Brand).Include(d => d.Category).FirstOrDefaultAsync(d => d.SerialNumber == sn);
      }
    }
    #endregion

    #region [UPDATE]
    public async Task Update(Device device)
    {
      using (var context = new DataContext())
      {
        var deviceToUpdate = (from d in context.Devices
                              where d.Id == device.Id
                              select d).FirstOrDefault();
        deviceToUpdate = device;
        await context.SaveChangesAsync();
      }
    }
    #endregion

    #region [DELETE]
    public async Task Delete(Guid key)
    {
      using (var context = new DataContext())
      {
        var deviceToDelete = (from d in context.Devices
                              where d.Id == key
                              select d).FirstOrDefault();
        context.Devices.Remove(deviceToDelete);
        await context.SaveChangesAsync();
      }
    }
    #endregion
  }
}
