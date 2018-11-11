using HardwareCheckoutSystemAdmin.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.Devices
{
  public class UpdateDevicesEvent : PubSubEvent<UpdateDevicesEventArgs> { }

  public class UpdateDevicesEventArgs
  {
    public string Message { get; set; }
    public Device Device { get; set; }

    public UpdateDevicesEventArgs(Device device, string message = "")
    {
      Message = message;
      Device = device;
    }
  }
}
