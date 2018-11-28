using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Newtonsoft.Json;
using RestSharp;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public class DeviceService : IDeviceService
    {

        private const string EndPoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;

        public DeviceService()
        {
            _client = new RestClient(EndPoint);
        }

        public async Task Delete(Device device)
        {
            await DeleteDeviceById(device.Id);
        }

        public async Task DeleteDeviceById(Guid id)
        {
            RestRequest request = new RestRequest("devices/{guid}", Method.DELETE);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task<List<Device>> FindAll()
        {
            var request = new RestRequest("devices", Method.GET);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (response.IsSuccessful)
            {
                List<Device> devices = JsonConvert.DeserializeObject<List<Device>>(response.Content);
                return devices;
            }
            throw new Exception(response.ErrorMessage);
        }

        public async Task<Device> FindDeviceById(Guid id)
        {
            var request = new RestRequest("devices/{guid}", Method.GET);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse<Device> response = await _client.ExecuteTaskAsync<Device>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task<Device> FindDeviceBySerialNumber(string sn)
        {
            var request = new RestRequest("devices/serialNumber/{sn}", Method.GET);
            request.AddUrlSegment("sn", sn);

            IRestResponse<Device> response = await _client.ExecuteTaskAsync<Device>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task Insert(Device newDevice)
        {
            var request = new RestRequest("devices", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newDevice);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task Update(Device device)
        {
            var request = new RestRequest("devices", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(device);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}
