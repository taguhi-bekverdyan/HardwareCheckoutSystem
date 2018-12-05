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

        [BindProperty]
        public List<Device> Devices { get; private set; } = new List<Device>();

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

        //public JsonResult OnGetUserId()
        //{
        //    return new JsonResult(_user.Id);
        //}
        [BindProperty]
        public Request Request { get; set; } = new Request();

        private string _deviceId = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            Request.UserId = _user.Id;
            Request.DeviceId = Guid.Parse(_deviceId);
            Request.Status = RequestStatus.StatusOne;
            Request.RequestDate = DateTime.Now;
            await _requestService.Insert(Request);
            Request = new Request();
            return Page();
        }

        public void OnPostDeviceId([FromBody]string deviceId)
        {
            _deviceId = deviceId;
        }

    }
}