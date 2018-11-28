using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public class UserService : IUserService
    {
        private const string EndPoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;

        public UserService()
        {
            _client = new RestClient(EndPoint);
        }

        public async Task Delete(User user)
        {
            await DeleteUserById(user.Id);
        }

        public async Task DeleteUserById(Guid id)
        {
            RestRequest request = new RestRequest("users/{guid}", Method.DELETE);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task<List<User>> FindAll()
        {
            var request = new RestRequest("users", Method.GET);
            IRestResponse response = await _client.ExecuteTaskAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<List<User>>(response.Content);
            }
            throw new Exception(response.ErrorMessage);

        }
        
        public async Task<User> FindUserById(Guid id)
        {
            var request = new RestRequest("users/{guid}", Method.GET);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse<User> response = await _client.ExecuteTaskAsync<User>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task Insert(User newUser)
        {
            var request = new RestRequest("users", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newUser);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
        
        public async Task Update(User user)
        {
            var request = new RestRequest("users", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}
