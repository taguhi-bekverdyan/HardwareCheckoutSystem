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
using HardwareCheckoutSystemAdmin.Data.Services.Helpers;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
  public class DeviceService : IDeviceService
  {
    private readonly IRestService _restService;
    private RestRequest _restRequest;

    public DeviceService(IRestService restService)
    {
      _restService = restService;
    }

    #region [READ]
    public async Task<List<Device>> FindAll()
    {
      _restRequest = new RestRequest("api/devices", Method.GET);
      var response = await _restService.Client.ExecuteTaskAsync<List<Device>>(_restRequest);

      return Helper.CheckResponseStatus(response);
    }

    public async Task<Device> FindById(Guid deviceId)
    {
      _restRequest = new RestRequest($"api/devices/{deviceId}", Method.GET);
      var response = await _restService.Client.ExecuteTaskAsync<Device>(_restRequest);

      return Helper.CheckResponseStatus(response);
    }

    public async Task<Device> FindBySn(string deviceSn)
    {
      _restRequest = new RestRequest($"api/devices/by-sn/{deviceSn}", Method.GET);
      var response = await _restService.Client.ExecuteTaskAsync<Device>(_restRequest);

      return Helper.CheckResponseStatus(response);
    }
    #endregion

    #region [CREATE]
    public async Task Insert(Device device)
    {
      _restRequest = new RestRequest("api/devices", Method.POST);
      _restRequest.AddJsonBody(device);
      var response = await _restService.Client.ExecuteTaskAsync<Device>(_restRequest);

      Helper.CheckResponseStatus(response);
    }
    #endregion


    #region [UPDATE]
    public async Task Update(Device device)
    {
      _restRequest = new RestRequest("api/devices", Method.PUT);
      _restRequest.AddJsonBody(device);
      var response = await _restService.Client.ExecuteTaskAsync<Device>(_restRequest);

      Helper.CheckResponseStatus(response);
    }
    #endregion

    #region [DELETE]
    public async Task Delete(Guid id)
    {
      _restRequest = new RestRequest($"api/devices/{id}", Method.DELETE);
      var response = await _restService.Client.ExecuteTaskAsync<Device>(_restRequest);

      Helper.CheckResponseStatus(response);
    }
    #endregion    
  }
}
