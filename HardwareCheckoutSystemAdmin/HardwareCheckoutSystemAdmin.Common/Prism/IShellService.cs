using HardwareCheckoutSystemAdmin.Common.Views;
using Prism.Regions;

namespace HardwareCheckoutSystemAdmin.Common.Prism
{
  public interface IShellService
  {
    ShellView ShowShell(string uri);
    ShellView ShowShell(string uri, NavigationParameters navigationParameters);
  }
}
