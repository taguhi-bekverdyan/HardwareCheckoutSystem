using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Views;
using Prism.Regions;

namespace HardwareCheckoutSystemAdmin.Common.Prism
{
    public interface IShellService
    {
        ShellView ShowShell(string uri);
        ShellView ShowShell(string uri, int w, int h);
        ShellView ShowShell(string uri, int w, int h, NavigationParameters navigationParameters);
        ShellView ShowShell(string uri, NavigationParameters navigationParameters);
    }

}
