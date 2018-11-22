using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Data.Services.Helpers;
using HardwareCheckoutSystemAdmin.Data.WebAPI;
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
    private RestRequest _restRequest;

    public BrandService(IRestService restService)
    {
      _restService = restService;
    }

    public async Task<List<Brand>> FindAll()
    {
      _restRequest = new RestRequest("api/brands", Method.GET);
      var response = await _restService.Client.ExecuteTaskAsync<List<Brand>>(_restRequest);

      return Helper.CheckResponseStatus(response);
    }

    public async Task<Brand> FindById(Guid brandId)
    {
      _restRequest = new RestRequest($"api/brands/{brandId}", Method.GET);
      var response = await _restService.Client.ExecuteTaskAsync<Brand>(_restRequest);

      return Helper.CheckResponseStatus(response);
    }

    public async Task Insert(Brand brand)
    {
      _restRequest = new RestRequest("api/brands", Method.POST);
      _restRequest.AddJsonBody(brand);
      var response = await _restService.Client.ExecuteTaskAsync<Brand>(_restRequest);

      Helper.CheckResponseStatus(response);
    }

    public async Task Delete(Guid id)
    {
      _restRequest = new RestRequest($"api/brands/{id}", Method.DELETE);
      var response = await _restService.Client.ExecuteTaskAsync<Brand>(_restRequest);

      Helper.CheckResponseStatus(response);
    }

    public async Task Update(Brand brand)
    {
      _restRequest = new RestRequest("api/brands", Method.PUT);
      _restRequest.AddJsonBody(brand);
      var response = await _restService.Client.ExecuteTaskAsync<Brand>(_restRequest);

      Helper.CheckResponseStatus(response);
    }
  }
}
