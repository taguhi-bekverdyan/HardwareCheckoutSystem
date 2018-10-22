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
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Request> Requests { get; set; } 
    public DbSet<Response> Responses { get; set; }
    #endregion

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("public");
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Device>().HasRequired<Brand>(b => b.Brand)
        .WithMany(d => d.Devices).HasForeignKey<Guid>(s => s.BrandId);
    }

  }
}
