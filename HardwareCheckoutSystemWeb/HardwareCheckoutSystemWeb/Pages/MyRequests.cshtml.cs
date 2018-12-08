using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HardwareCheckoutSystemWeb.Infrastructore;
using HardwareCheckoutSystemWeb.Models;
using HardwareCheckoutSystemWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HardwareCheckoutSystemWeb.Pages
{
    public class MyRequestsModel : PageModel
    {

        private readonly IUserService _userService;
        private readonly IRequestService _requestService;
        private readonly IDeviceService _deviceService;

        [BindProperty]
        public List<Request> ReceivedDevices { get; set; } = new List<Request>();
        public List<Request> History { get; set; } = new List<Request>();
        public List<Request> InProgress { get; set; } = new List<Request>();

        private User _user;

        public MyRequestsModel(IUserService userService,IRequestService requestService,IDeviceService deviceService)
        {
            _userService = userService;
            _requestService = requestService;
            _deviceService = deviceService;

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
            var user = await _userService.FindUserById(_user.Id);
            ReceivedDevices = (from r in user.Requests where r.Status == RequestStatus.StatusTwo select r)
                .ToList();
            List<Request> requests = await _requestService.FindAll();
            History = (from r in requests where r.UserId == _user.Id && r.Status != RequestStatus.StatusOne select r)
                .ToList();
            InProgress = await _requestService.FindRequestsInPending();
        }

        
        public async Task<IActionResult> OnPost([FromBody]string guid)
        {
            try
            {
                Request req = await _requestService.FindRequestById(new Guid(guid));
                Device dev = await _deviceService.FindDeviceById(req.DeviceId);
                dev.Status = DeviceStatus.StatusOne;
                req.Status = RequestStatus.StatusThree;
                await _requestService.Update(req);
                await _deviceService.Update(dev);
                return Page();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

    }
}