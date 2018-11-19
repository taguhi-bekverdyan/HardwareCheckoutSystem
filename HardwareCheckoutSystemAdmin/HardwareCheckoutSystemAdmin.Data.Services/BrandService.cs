﻿using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public class BrandService : IBrandService
    {
        private const string EndPoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;

        public BrandService()
        {
            _client = new RestClient(EndPoint);
        }

        public async Task Delete(Brand brand)
        {
            await DeleteBrandById(brand.Id);
        }

        public Task DeleteBrandById(Guid id)
        {
            return Task.Factory.StartNew(() => {
                try
                {
                    RestRequest request = new RestRequest("brands/{guid}", Method.DELETE);
                    request.AddUrlSegment("guid",id.ToString());

                    IRestResponse response = _client.Execute(request);
                }
                catch (Exception)
                {

                }
            });
        }


        public Task<List<Brand>> FindAll()
        {
            return Task<List<Brand>>.Factory.StartNew(()=> {
                RestRequest request = new RestRequest("brands", Method.GET);
                IRestResponse<List<Brand>> response = _client.Execute<List<Brand>>(request);
                return response.Data;
            });               
        }

        public Task<Brand> FindBrandById(Guid id)
        {
            return Task<Brand>.Factory.StartNew(() =>
            {
                try
                {
                    RestRequest request = new RestRequest("brands/{guid}", Method.GET);
                    request.AddUrlSegment("guid", id.ToString());

                    IRestResponse<Brand> response = _client.Execute<Brand>(request);
                    return response.Data;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        public Task<Brand> FindBrandByName(string name)
        {
            return Task<Brand>.Factory.StartNew(()=> {
                try
                {
                    RestRequest request = new RestRequest("brands/byName/{name}", Method.GET);
                    request.AddUrlSegment("name",name);

                    IRestResponse<Brand> response = _client.Execute<Brand>(request);
                    return response.Data;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        public Task Insert(Brand brand)
        {
            return Task.Factory.StartNew(()=> {
                try
                {
                    RestRequest request = new RestRequest("brands", Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(new { name = brand.Name});

                    IRestResponse response = _client.Execute(request);
                }
                catch (Exception)
                {

                }
            });
        }

        public Task Update(Brand brand)
        {
            return Task.Factory.StartNew(()=> {
                try
                {
                    RestRequest request = new RestRequest("brands/{guid}",Method.PUT);
                    request.AddUrlSegment("guid",brand.Id);
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(new { name = brand.Name});

                    IRestResponse response = _client.Execute(request);
                }
                catch (Exception)
                {

                }
            });
        }
    }
}
