using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HardwareCheckoutSystemWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace HardwareCheckoutSystemWeb.Pages
{
    public class HomeModel : PageModel
    {

        public List<Device> Devices { get; set; }

        private const string EndPoint = @"https://localhost:44350/api/";
        private RestClient _client;

        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Hello world and Ghevond";

            _client = new RestClient(EndPoint);
            RestRequest request = new RestRequest("devices", Method.GET);
            IRestResponse<List<Device>> response = _client.Execute<List<Device>>(request);

            if (response.IsSuccessful)
            {
                Devices = response.Data;
            }
            else
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}