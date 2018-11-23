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
    public class CategoryService : ICategoryService
    {
        private readonly IRestService _restService;

        public CategoryService(IRestService restservice)
        {
            _restService = restservice;
        }

        #region [CREATE]
        public async Task Insert(Category category)
        {
            var request = new RestRequest("api/categories", Method.POST);
            request.AddBody(category);
            IRestResponse response = _restService.Client.Execute(request);
        }

        #endregion

        #region [READ]
        public async Task<List<Category>> FindAll()
        {
            var request = new RestRequest("api/categories", Method.GET);
            var response = await _restService.Client.ExecuteTaskAsync<List<Category>>(request);
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

        #region [Name]
        public async Task<Category> FindByName(string name)
        {

            var request = new RestRequest("api/categories/byName/{name}", Method.GET);
            request.AddUrlSegment("{name}", name);
            var response = await _restService.Client.ExecuteTaskAsync<Category>(request);
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
        public async Task<Category> FindById(Guid Id)
        {

            var request = new RestRequest("api/categories/{guid}", Method.GET);
            request.AddUrlSegment("{guid}", Id);
            var response = await _restService.Client.ExecuteTaskAsync<Category>(request);
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

        #region [UPDATE]
        public async Task Update(Category category)
        {
            var request = new RestRequest("api/categories/{guid}", Method.PUT);
            request.AddUrlSegment("{guid}", category.Id);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(category);

            IRestResponse response = _restService.Client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
        #endregion

        #region [DELETE]
        public async Task Delete(Category category)
        {
            await DeleteByName(category.Name);
        }

        public async Task DeleteByName(string name)
        {
            var request = new RestRequest("api/categories/{name}", Method.DELETE);
            request.AddUrlSegment("name", name);
            var response = _restService.Client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            #endregion
        }
        }
}
