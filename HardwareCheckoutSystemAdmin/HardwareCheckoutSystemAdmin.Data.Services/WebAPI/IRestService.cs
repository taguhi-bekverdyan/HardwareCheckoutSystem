using RestSharp;

namespace HardwareCheckoutSystemAdmin.Data.WebAPI
{
  public interface IRestService
  {
    RestClient Client { get; set; }
  }
}
