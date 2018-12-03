using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HardwareCheckoutSystemAdmin.Common.Controls
{
  public class HeaderContextMenu : ContextMenu
  {
    public DataGrid DataGrid { get; set; }


    public void Initialize(DataGrid grid)
    {
      DataGrid = grid;
      var columnsList = new List<MenuItem>();
      foreach (var item in grid.Columns)
      {
        columnsList.Add(new MenuItem() { Header = item.Header });
      }
      ItemsSource = columnsList;

      foreach (MenuItem item in ItemsSource)
      {
        item.IsCheckable = true;
        item.IsChecked = true;
        item.Checked += Item_Checked;
        item.Unchecked += Item_Unchecked;
      }
    }

    private void Item_Unchecked(object sender, RoutedEventArgs e)
    {
      var menuItem = sender as MenuItem;
      var currentColumn = (from col in DataGrid.Columns where col.Header == menuItem.Header select col).FirstOrDefault();
      if (!menuItem.IsChecked)
      {
        currentColumn.Visibility = System.Windows.Visibility.Collapsed;
      }
    }

    //context menu checked events
    private void Item_Checked(object sender, System.Windows.RoutedEventArgs e)
    {
      var menuItem = sender as MenuItem;
      var currentColumn = (from col in DataGrid.Columns where col.Header == menuItem.Header select col).FirstOrDefault();
      if (menuItem.IsChecked)
      {
        currentColumn.Visibility = System.Windows.Visibility.Visible;
      }
    }
  }
}
