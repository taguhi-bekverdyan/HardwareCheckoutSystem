using HardwareCheckoutSystemAdmin.Module.Main.Views.UserViewElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements
{
    class DeviceParameter
    {

        public Mode Mode { get; set; }
        public DeviceViewItem Device { get; set; }

        public DeviceParameter(Mode mode,DeviceViewItem device)
        {
            Mode = mode;
            Device = device;
        }

    }

    class UserParameter
    {
        public Mode Mode { get; set; }
        public UserViewItem User { get; set; }

        public UserParameter(Mode mode,UserViewItem user)
        {
            Mode = mode;
            User = user;
        }

    }

    enum Mode
    {
        Add,
        Edit
    }

}
