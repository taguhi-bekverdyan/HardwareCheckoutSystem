using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Data.Services.Helpers;
using HardwareCheckoutSystemAdmin.Data.WebAPI;
using HardwareCheckoutSystemAdmin.Models;
using RestSharp;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
  public class CategoryService : ICategoryService
  {
    private readonly IRestService _restService;
    private RestRequest _restRequest;

    public CategoryService(IRestService restService)
    {
      _restService = restService;
    }

    #region [READ]
    public async Task<List<Category>> FindAll()
    {
      _restRequest = new RestRequest("api/categories", Method.GET);
      var response = await _restService.Client.ExecuteTaskAsync<List<Category>>(_restRequest);

      return Helper.CheckResponseStatus(response);
    }

    public async Task<Category> FindById(Guid categoryId)
    {
      _restRequest = new RestRequest($"api/categories/{categoryId}", Method.GET);
      var response = await _restService.Client.ExecuteTaskAsync<Category>(_restRequest);

      return Helper.CheckResponseStatus(response);
    }

    #endregion

    #region [CREATE]
    public async Task Insert(Category category)
    {
      _restRequest = new RestRequest("api/categories", Method.POST);
      _restRequest.AddJsonBody(category);
      var response = await _restService.Client.ExecuteTaskAsync<Category>(_restRequest);

      Helper.CheckResponseStatus(response);
    }
    #endregion


    #region [UPDATE]
    public async Task Update(Category category)
    {
      _restRequest = new RestRequest("api/categories", Method.PUT);
      _restRequest.AddJsonBody(category);
      var response = await _restService.Client.ExecuteTaskAsync<Category>(_restRequest);

      Helper.CheckResponseStatus(response);
    }
    #endregion

    #region [DELETE]
    public async Task Delete(Guid id)
    {
      _restRequest = new RestRequest($"api/categories/{id}", Method.DELETE);
      var response = await _restService.Client.ExecuteTaskAsync<Category>(_restRequest);

      Helper.CheckResponseStatus(response);
    }
    #endregion    
  }
}
