using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;

namespace HardwareCheckoutSystemAdmin.Common.Prism
{
    public interface IShellService
    {
        void ShowShell(string uri);
        void ShowShell(string uri, int w, int h);
        void ShowShell(string uri, int w, int h, NavigationParameters navigationParameters);
        void ShowShell(string uri, NavigationParameters navigationParameters);
    }

}
