using RestSharp;

namespace HardwareCheckoutSystemAdmin.Data.WebAPI
{
  public class RestService : IRestService
  {
    public RestService(string baseUrl)
    {
      Client = new RestClient(baseUrl);
    }

    public RestClient Client { get; set; }
  }
}
