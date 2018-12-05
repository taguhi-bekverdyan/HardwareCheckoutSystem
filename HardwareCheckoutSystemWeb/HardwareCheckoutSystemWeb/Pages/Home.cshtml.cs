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
                Devices = await OnGetData();
            }
            catch (Exception)
            {
                Devices = new List<Device>();
            }
        }

        
        private async Task<List<Device>> OnGetData()
        {
            List<Device> temp = await _deviceService.FindAll();
            return (from i in temp where i.Status != DeviceStatus.StatusThree select i)
                .ToList<Device>();
        }

        [HttpGet]
        public JsonResult OnGetUserId()
        {
            return new JsonResult(_user.Id);
        }

        [HttpGet]
        public async Task<JsonResult> OnGetDevices()
        {
            return new JsonResult(await OnGetData());
        }
        
        
        [HttpPost]
        public async Task<IActionResult> OnPost([FromBody]Request request)
        {
            try
            {
                await _requestService.Insert(request);
                Device device = await _deviceService.FindDeviceById(request.DeviceId);
                device.Status = DeviceStatus.StatusThree;
                await _deviceService.Update(device);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        

    }
}