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
    public class BrandService : IBrandService
    {
        private readonly IRestService _restService;

        public BrandService(IRestService restservice)
        {
            _restService = restservice;
        }

        #region [CREATE]
        public async Task Insert(Brand brand)
        {
            var request = new RestRequest("api/brands", Method.POST);
            request.AddBody(brand);
            var response = _restService.Client.Execute(request);
        }

        #endregion

        #region [READ]
        public async Task<List<Brand>> FindAll()
        {
            var request = new RestRequest("api/brands", Method.GET);
            var response = await _restService.Client.ExecuteTaskAsync<List<Brand>>(request);
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
        public async Task<Brand> FindByName(string name)
        {
            var request = new RestRequest("api/brands/byName/{name}", Method.GET);
            request.AddParameter("{name}", name);
            var response = await _restService.Client.ExecuteTaskAsync<Brand>(request);
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
        public async Task<Brand> FindById(Guid Id)
        {
            var request = new RestRequest("api/brands/{guid}", Method.GET);
            request.AddParameter("{guid}", Id);
            var response = await _restService.Client.ExecuteTaskAsync<Brand>(request);
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
        public async Task Update(Brand brand)
        {
            var request = new RestRequest("api/brands/{guid}", Method.PUT);
            request.AddUrlSegment("{guid}", brand.Id);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(brand);
            IRestResponse response = _restService.Client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
        #endregion

        #region [DELETE]
        public async Task Delete(Brand brand)
        {
            await DeleteByName(brand.Name);
        }

        public async Task DeleteByName(string name)
        {
            var request = new RestRequest("api/brands/{name}", Method.DELETE);
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


