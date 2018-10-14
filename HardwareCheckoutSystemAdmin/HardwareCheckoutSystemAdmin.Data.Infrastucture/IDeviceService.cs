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
    Task Insert(Device person);
    Task<List<Device>> FindAll();
    Task<Device> FindOne(int personId);
  }
}
