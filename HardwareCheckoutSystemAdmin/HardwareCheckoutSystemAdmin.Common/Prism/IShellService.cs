using HardwareCheckoutSystemAdmin.Views;
using Prism.Regions;


namespace HardwareCheckoutSystemAdmin.Common.Prism
{
    public interface IShellService
    {
        ShellView ShowShell(string uri);
        ShellView ShowShell(string uri, NavigationParameters navigationParameters, int w, int h);
        void ShowShell(string uri, int w, int h);
        void ShowShell(string uri, NavigationParameters navigationParameters);
    }


}
