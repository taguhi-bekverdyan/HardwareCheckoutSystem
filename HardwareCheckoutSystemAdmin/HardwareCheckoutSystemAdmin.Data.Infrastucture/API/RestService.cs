using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Services
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
