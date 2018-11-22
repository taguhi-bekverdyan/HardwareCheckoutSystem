using RestSharp;
using System;

namespace HardwareCheckoutSystemAdmin.Data.Services.Helpers
{
  public static class Helper
  {
    public static T CheckResponseStatus<T>(IRestResponse<T> response)
    {
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
  }
}
