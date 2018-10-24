using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements
{
    class Parameter
    {

        public Mode Mode { get; set; }
        public DeviceViewItem Device { get; set; }

        public Parameter(Mode mode,DeviceViewItem device)
        {
            Mode = mode;
            Device = device;
        }

    }

    enum Mode
    {
        Add,
        Edit
    }

}
