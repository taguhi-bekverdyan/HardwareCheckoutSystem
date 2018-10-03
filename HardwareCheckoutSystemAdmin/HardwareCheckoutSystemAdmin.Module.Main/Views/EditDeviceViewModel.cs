using Prism.Mvvm;
using Prism.Regions;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views
{
    public class EditDeviceViewModel : BindableBase, INavigationAware
    {
        public EditDeviceViewModel()
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var person = navigationContext.Parameters["request"];

            //data loading
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var person = navigationContext.Parameters["request"];
            if (person != null)
                return true;
            else
            {
                return false;
            }
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
