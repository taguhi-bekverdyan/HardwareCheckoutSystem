using HardwareCheckoutSystemAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Data.Infrastructure
{
  public interface IDeviceService
  {
    Task Insert(Device device);
    Task<List<Device>> FindAll();
    Task<Device> FindById(Guid deviceId);
    Task<Device> FindBySn(string sn);
    Task Update(Device device);
    Task Delete(Guid key);
  }
}
