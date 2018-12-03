using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HardwareCheckoutSystemWeb.Infrastructore;
using HardwareCheckoutSystemWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HardwareCheckoutSystemWeb.Pages
{
    public class HomeModel : PageModel
    {

        private readonly IDeviceService _deviceService;
        private readonly IRequestService _requestService;

        private User _user;

        
        public List<Device> Devices { get; private set; }
        


        public HomeModel(IDeviceService deviceService, IRequestService requestService)
        {
            _deviceService = deviceService;
            _requestService = requestService;

            string userJson;
            try
            {
                using (StreamReader sr = new StreamReader(@"Helpers/user.json"))
                {
                    userJson = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {

                throw;
            }
            _user = JsonConvert.DeserializeObject<User>(userJson);
        }

        public async Task OnGet()
        {
            try
            {
                await GetData();
            }
            catch (Exception)
            {
                Devices = new List<Device>();
            }
        }

        private async Task GetData()
        {
            List<Device> temp = await _deviceService.FindAll();
            Devices = (from i in temp where i.Status != DeviceStatus.StatusTwo select i)
                .ToList<Device>();
        }

        public JsonResult OnGetUserId()
        {
            return new JsonResult(_user.Id);
        }

    }
}