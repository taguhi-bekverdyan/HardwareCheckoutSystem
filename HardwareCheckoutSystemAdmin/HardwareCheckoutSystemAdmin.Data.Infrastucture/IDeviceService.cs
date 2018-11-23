using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Services
{
    public interface IDeviceService
    {
        Task Insert(Device device);
        Task<List<Device>> FindAll();
        Task<Device> FindById(Guid deviceId);
        Task<Device> FindBySn(string sn);
        Task Update(Device device);
        Task Delete(Device device);
        Task DeleteBySerialNumber(string serialnumber);
    }
}
