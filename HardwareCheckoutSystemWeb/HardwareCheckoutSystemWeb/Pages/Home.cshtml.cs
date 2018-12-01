using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HardwareCheckoutSystemWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;

namespace HardwareCheckoutSystemWeb.Pages
{
    public class HomeModel : PageModel
    {

        public List<Device> Devices { get; private set; }

        private const string EndPoint = @"https://localhost:44350/api/";
        private RestClient _client;


        public void OnGet()
        {

            _client = new RestClient(EndPoint);
            RestRequest request = new RestRequest("devices", Method.GET);
            IRestResponse response = _client.Execute(request);

            

            if (response.IsSuccessful)
            {
                Devices = JsonConvert.DeserializeObject<List<Device>>(response.Content);
            }
            else
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}