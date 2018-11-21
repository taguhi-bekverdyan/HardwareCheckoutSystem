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

        readonly RestClient restClient = new RestClient("https://localhost:44381/");

        #region [READ]
        public async Task<List<Device>> FindAll()
        {
            _restRequest.Resource = "api/devices";
            var response = await _restService.Client.ExecuteTaskAsync<List<Device>>(_restRequest);
            return CheckResponseStatus(response);
        }
        #endregion

        #region [SN]
        public async Task<Device> FindBySn(string sn)
        {
            using (var context = new DataContext())
            {
                return await context.Devices.FirstOrDefaultAsync(d => d.SerialNumber == sn);
            }
        }
        #endregion

        #region[FIND]
        public async Task<Device> FindById(Guid deviceId)
        {
            using (var context = new DataContext())
            {
                return await context.Devices.FirstOrDefaultAsync(d => d.Id == deviceId);
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

        public async Task DeleteBySerialNumber(string serialnumber)
        {
            using (var context = new DataContext())
            {
                var deviceToDelete = (from d in context.Devices
                                      where d.SerialNumber == serialnumber
                                      select d).FirstOrDefault();
                context.Devices.Remove(deviceToDelete);
                await context.SaveChangesAsync();
            }
        }
        #endregion

        private List<Device> CheckResponseStatus(IRestResponse<List<Device>> response)
        {
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
    }
}
