using HardwareCheckoutSystemWeb.Infrastructore;
using HardwareCheckoutSystemWeb.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWeb.Services
{
    public class CategoryService : ICategoryService
    {

        private const string EndPoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;

        public CategoryService()
        {
            _client = new RestClient(EndPoint);
        }

        public async Task Delete(Category category)
        {
            await DeleteCategoryById(category.Id);
        }

        public async Task DeleteCategoryById(Guid id)
        {
            RestRequest request = new RestRequest("categories/{guid}", Method.DELETE);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task<List<Category>> FindAll()
        {
            var request = new RestRequest("categories", Method.GET);
            IRestResponse<List<Category>> response = await _client.ExecuteTaskAsync<List<Category>>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task<Category> FindCategoryByIdAsync(Guid id)
        {
            var request = new RestRequest("categories/{guid}", Method.GET);
            request.AddUrlSegment("guid", id.ToString());

            IRestResponse<Category> response = await _client.ExecuteTaskAsync<Category>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task<Category> FindCategoryByName(string name)
        {
            var request = new RestRequest("categories/byName/{name}", Method.GET);
            request.AddUrlSegment("name", name);

            IRestResponse<Category> response = await _client.ExecuteTaskAsync<Category>(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task Insert(Category category)
        {
            var request = new RestRequest("categories", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(category);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task Update(Category category)
        {
            var request = new RestRequest("categories", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(category);

            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}
