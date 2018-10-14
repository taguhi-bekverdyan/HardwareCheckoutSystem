using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemAdmin.Models
{
  public class Device
  {
    #region Properties
    [Key]
    public int SN { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    //
    public string Permission { get; set; }
    public DateTime MaxPeriod { get; set; }
    //
    #endregion


  }
}
