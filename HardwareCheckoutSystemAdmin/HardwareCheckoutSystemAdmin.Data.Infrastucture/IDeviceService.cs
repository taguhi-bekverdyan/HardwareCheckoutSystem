using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data.Infrastructure
{
    public interface IDeviceService
    {
        Task Insert(Device newDevice);
        Task<List<Device>> FindAll();
        Task<Device> FindDeviceById(Guid id);
        Task<Device> FindDeviceBySerialNumber(string sn);
        Task Update(Device device);
        Task Delete(Device device);
        Task DeleteDeviceById(Guid id);
    }
}
