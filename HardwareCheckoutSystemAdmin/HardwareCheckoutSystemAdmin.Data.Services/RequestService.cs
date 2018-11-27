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
    public class RequestService : IRequestService
    {
        private const string EndPoint = @"http://localhost:63596/api";
        private readonly RestClient _client;

        public RequestService()
        {
            _client = new RestClient(EndPoint);
        }

        public async Task Delete(Request request)
        {
            await DeleteRequestById(request.Id);
        }

        public async Task DeleteRequestById(Guid id)
        {
            RestRequest request = new RestRequest("requests/{guid}", Method.DELETE);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task<List<Request>> FindAll()
        {
            var request = new RestRequest("requests", Method.GET);
            IRestResponse<List<Request>> response = await _client.ExecuteTaskAsync<List<Request>>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task<Request> FindRequestById(Guid id)
        {
            var request = new RestRequest("requests/{guid}", Method.GET);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse<Request> response = await _client.ExecuteTaskAsync<Request>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task Insert(Request newRequest)
        {
            var request = new RestRequest("requests", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newRequest);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
        
        public async Task Update(Request newRequest)
        {
            var request = new RestRequest("requests", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newRequest);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}
