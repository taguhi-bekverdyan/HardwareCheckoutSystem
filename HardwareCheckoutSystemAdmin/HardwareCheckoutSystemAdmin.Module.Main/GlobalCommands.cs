using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Module.Main
{
    public class GlobalCommands
    {
        public static CompositeCommand SaveUser = new CompositeCommand();
        public static CompositeCommand Save_Edit = new CompositeCommand();
    }
}