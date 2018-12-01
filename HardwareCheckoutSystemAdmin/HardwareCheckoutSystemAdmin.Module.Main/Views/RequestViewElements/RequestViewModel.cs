using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.RequestViewElements
{
    class RequestViewModel
    {

        private Guid _id;
        private User _user;
        private Device _device;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Category DeviceCategory { get; set; }
        public Brand DeviceBrand { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceDescription { get; set; }
        public DeviceStatus DeviceStatus { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? RentStartDate { get; set; }
        public DateTime? RentEndDate { get; set; }
        public string Message { get; set; }


        public RequestViewModel(Request request)
        {
            _id = request.Id;
            _user = request.User;
            _device = request.Device;

            FirstName = _user.FirstName;
            LastName = _user.LastName;
            DeviceCategory = _device.Category;
            DeviceBrand = _device.Brand;
            DeviceModel = _device.Model;
            DeviceDescription = _device.Description;
            DeviceStatus = _device.Status;
            Status = request.Status;
            RequestDate = request.RequestDate;
            RentStartDate = request.RentStartDate;
            RentEndDate = request.RentEndDate;
            Message = request.Message;
        }


        public Guid GetId()
        {
            return _id;
        }

        public static explicit operator Request(RequestViewModel model)
        {
            Request request = new Request();

            request.Id = model._id;
            request.User = model._user;
            request.Device = model._device;
            request.Status = model.Status;
            request.RequestDate = model.RequestDate;
            request.RentStartDate = model.RentStartDate;
            request.RentEndDate = model.RentEndDate;
            request.Message = model.Message;

            return request;
        }

    }
}
