using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using RestSharp;
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
        private readonly IRestService _restService;

        public DeviceService  (IRestService restservice)
        {
            _restService = restservice;
        }

        #region [CREATE]
        public async Task Insert(Device device)
        {
            var request = new RestRequest("api/devices", Method.POST);
            request.AddBody(device);
            var response = _restService.Client.Execute(request);
        }
        
        #endregion

        #region [READ]
        public async Task<List<Device>> FindAll()
        {

        //var devices = context.Devices.ToListAsync();
        //return await devices;
        var request = new RestRequest("api/devices", Method.GET);
        var response = await _restService.Client.ExecuteTaskAsync<List<Device>>(request);
        if (response.IsSuccessful)
           {
            return response.Data;
            }
          else
        {
            string message = response.ErrorMessage;
            throw new Exception("Server Error: " + message);
        }

    }
        #endregion

        #region [SN]
        public async Task<Device> FindBySn(string sn)
        {
            //using (var context = new DataContext())
            //{
            //    return await context.Devices.FirstOrDefaultAsync(d => d.SerialNumber == sn);
            //}
            var request = new RestRequest("api/devices/Sn/{sn}", Method.GET);
            request.AddUrlSegment("{sn}", sn);
            var response = await _restService.Client.ExecuteTaskAsync<Device>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                string message = response.ErrorMessage;
                throw new Exception("Server Error: " + message);
            }

        }
        #endregion

        #region[FIND]
        public async Task<Device> FindById(Guid deviceId)
        {
            //using (var context = new DataContext())
            //{
            //    return await context.Devices.FirstOrDefaultAsync(d => d.Id == deviceId);
            //}

            var request = new RestRequest("api/devices/{guid}", Method.GET);
            request.AddUrlSegment("{guid}", deviceId);
            var response = await  _restService.Client.ExecuteTaskAsync<Device>(request);
           if (response.IsSuccessful)
          {
            return response.Data;
          }
        else
        {
            string message = response.ErrorMessage;
            throw new Exception("Server Error: " + message);
        }
    }
        #endregion

        #region Update
        public async Task Update(Device device)
        {
            var request = new RestRequest("api/devices/{guid}", Method.PUT);
            request.AddUrlSegment("{guid}", device.Id);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(device);
            IRestResponse response = _restService.Client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
        #endregion

        #region [DELETE]
        public async Task Delete(Device device)
        {
            await DeleteBySerialNumber(device.SerialNumber);
        }

        public async Task DeleteBySerialNumber(string sn)
        {

            var request = new RestRequest("api/devices/{sn}", Method.DELETE);
            request.AddUrlSegment("{sn}", sn);
            var response = _restService.Client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            //using (var context = new DataContext())
            //{
            //    var deviceToDelete = (from d in context.Devices
            //                          where d.SerialNumber == serialnumber
            //                          select d).FirstOrDefault();
            //    context.Devices.Remove(deviceToDelete);
            //    await context.SaveChangesAsync();
            //}
        }
        #endregion

 
    }
}
