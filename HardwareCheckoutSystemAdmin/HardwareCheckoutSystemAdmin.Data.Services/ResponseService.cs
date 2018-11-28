using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public class ResponseService : IResponseService
    {
        private const string EndPoint = @"http://localhost:63596/api";
        private readonly RestClient _client;

        public ResponseService()
        {
            _client = new RestClient(EndPoint);
        }

        public async Task Delete(Response response)
        {
            await DeleteResponseById(response.Id);
        }

        public async Task DeleteResponseById(Guid id)
        {
            RestRequest request = new RestRequest("response/{guid}", Method.DELETE);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task<List<Response>> FindAll()
        {
            var request = new RestRequest("response", Method.GET);
            IRestResponse<List<Response>> response = await _client.ExecuteTaskAsync<List<Response>>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task<Response> FindResponseById(Guid id)
        {
            var request = new RestRequest("response/{guid}", Method.GET);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse<Response> response = await _client.ExecuteTaskAsync<Response>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task Insert(Response newResponse)
        {
            var request = new RestRequest("responses", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newResponse);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
        
        public async Task Update(Response newResponse)
        {
            var request = new RestRequest("responses", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newResponse);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}
