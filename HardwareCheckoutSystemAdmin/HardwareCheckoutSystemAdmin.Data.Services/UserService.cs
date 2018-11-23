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
    public class UserService : IUserService
    {
        private readonly IRestService _restService;

        public UserService(IRestService restservice)
        {
            _restService = restservice;
        }

        #region [CREATE]
        public async Task Insert(User user)
        {
            var request = new RestRequest("api/users", Method.POST);
            request.AddBody(user);
            var response =  _restService.Client.Execute(request);
        }

        #endregion

        #region [READ]
        public async Task<List<User>> FindAll()
        {

            //var devices = context.Devices.ToListAsync();
            //return await devices;
            var request = new RestRequest("api/users", Method.GET);
            var response = await _restService.Client.ExecuteTaskAsync<List<User>>(request);
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
        public async Task<User> FindBySn(string sn)
        {
            //using (var context = new DataContext())
            //{
            //    return await context.Devices.FirstOrDefaultAsync(d => d.SerialNumber == sn);
            //}
            var request = new RestRequest("api/users/Sn/{sn}", Method.GET);
            request.AddUrlSegment("{sn}", sn);
            var response = await _restService.Client.ExecuteTaskAsync<User>(request);
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
        public async Task<User> FindById(Guid Id)
        {
            //using (var context = new DataContext())
            //{
            //    return await context.Devices.FirstOrDefaultAsync(d => d.Id == deviceId);
            //}

            var request = new RestRequest("api/users/{guid}", Method.GET);
            request.AddUrlSegment("{guid}", Id);
            var response = await _restService.Client.ExecuteTaskAsync<User>(request);
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
        public async Task Update(User user)
        {
            var request = new RestRequest("api/users/{guid}", Method.PUT);
            request.AddUrlSegment("{guid}", user.Id);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);
            IRestResponse response = _restService.Client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
        #endregion

        #region [DELETE]
        public async Task Delete(User user)
        {
            await DeleteBySerialNumber(user.SerialNumber);
        }

        public async Task DeleteBySerialNumber(string sn)
        {
            
            var request = new RestRequest("api/users/{sn}", Method.DELETE);
            request.AddUrlSegment("{sn}", sn);
            var response =  _restService.Client.Execute(request);
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