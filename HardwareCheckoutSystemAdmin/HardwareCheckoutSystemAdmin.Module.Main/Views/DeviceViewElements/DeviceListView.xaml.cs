using HardwareCheckoutSystemAdmin.Common.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.DeviceViewElements
{
    /// <summary>
    /// Interaction logic for DeviceListView.xaml
    /// </summary>
    public partial class DeviceListView : UserControl
    {
        public DeviceListView()
        {
            InitializeComponent();
        }

    private void DataGrid_Loaded(object sender, RoutedEventArgs e)
    {
      headerContextMenu.Initialize(grid);

      //LoadColumnVisibilities

    }

    private void UserControl_Unloaded(object sender, RoutedEventArgs e)
    {
      //SaveColumnVisibilities
    }
  }
}
