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

        [BindProperty]
        public List<Request> ReceivedDevices { get; set; } = new List<Request>();
        public List<Request> History { get; set; } = new List<Request>();

        private User _user;

        public MyRequestsModel(IUserService userService)
        {
            _userService = userService;

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
            History = user.Requests.ToList();
        }
    }
}