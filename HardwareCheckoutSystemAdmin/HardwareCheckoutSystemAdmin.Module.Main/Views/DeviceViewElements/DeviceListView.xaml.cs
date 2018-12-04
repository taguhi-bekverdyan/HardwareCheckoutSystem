using HardwareCheckoutSystemAdmin.Common.Controls;
using HardwareCheckoutSystemAdmin.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    private string path;
    private List<JsonMenuItem> menuItemList;

    public DeviceListView()
    {
      InitializeComponent();
      path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), Properties.Resources.columnsHeaderPath);
    }

    private void DataGrid_Loaded(object sender, RoutedEventArgs e)
    {
      var headerContextMenu = Resources["DataGridColumnHeaderContextMenu"] as HeaderContextMenu;
      headerContextMenu.Initialize(grid);

      menuItemList = JsonSerializer<List<JsonMenuItem>>.JsonReadFile(path);
      foreach (MenuItem item in headerContextMenu.ItemsSource)
      {
        item.IsChecked = menuItemList.Find(i => i.Header == item.Header.ToString()).IsChecked;
      }
    }

    private void DataGrid_Unloaded(object sender, RoutedEventArgs e)
    {
      var headerContextMenu = Resources["DataGridColumnHeaderContextMenu"] as HeaderContextMenu;

      menuItemList = new List<JsonMenuItem>();
      foreach (MenuItem item in headerContextMenu.ItemsSource)
      {
        menuItemList.Add(new JsonMenuItem { Header = item.Header.ToString(), IsChecked = item.IsChecked });
      }

      JsonSerializer<List<JsonMenuItem>>.JsonWriteToFile(path, menuItemList);
    }
  }
}
