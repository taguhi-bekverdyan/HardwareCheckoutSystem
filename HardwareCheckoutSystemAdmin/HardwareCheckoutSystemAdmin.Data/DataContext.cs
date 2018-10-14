using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data
{
  public class DataContext : DbContext
  {
    public DataContext() : base("AppDatabaseConnectionString")
    {
    }

    #region DbSets
    public DbSet<Device> Devices { get; set; }
    public DbSet<Person> Persons { get; set; }
    #endregion

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("public");
      base.OnModelCreating(modelBuilder);
    }

  }
}
