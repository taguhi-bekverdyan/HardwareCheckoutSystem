using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<HardwareCheckoutSystemAdmin.Data.DataContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(HardwareCheckoutSystemAdmin.Data.DataContext context)
    {
      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //  to avoid creating duplicate seed data.

      //TODO: Always drop DB before migration

      //List<Category> categories = new List<Category> {
      //  new Category { Id = Guid.NewGuid(), Name = "Desktop Computers"},
      //  new Category { Id = Guid.NewGuid(), Name = "Servers" },
      //  new Category { Id = Guid.NewGuid(), Name = "Monitors" },
      //  new Category { Id = Guid.NewGuid(), Name = "Accessories" },
      //  new Category { Id = Guid.NewGuid(), Name = "Network Equipments" },
      //  new Category { Id = Guid.NewGuid(), Name = "Printers and Scanners" },
      //  new Category { Id = Guid.NewGuid(), Name = "Laptops" } };
      //context.Categories.AddRange(categories);

      //context.Users.Add(new User { Id = Guid.NewGuid(), FirstName = "Admin" });

      //List<Brand> brands = new List<Brand> {
      //  new Brand { Id = Guid.NewGuid(), Name = "Dell"},
      //  new Brand { Id = Guid.NewGuid(), Name = "Lenovo" },
      //  new Brand { Id = Guid.NewGuid(), Name = "Acer" },
      //  new Brand { Id = Guid.NewGuid(), Name = "Apple" },
      //  new Brand { Id = Guid.NewGuid(), Name = "Toshiba" },
      //  new Brand { Id = Guid.NewGuid(), Name = "Canon" },
      //  new Brand { Id = Guid.NewGuid(), Name = "Logitec" },
      //  new Brand { Id = Guid.NewGuid(), Name = "HP" },
      //  new Brand { Id = Guid.NewGuid(), Name = "TP-Link" },
      //  new Brand { Id = Guid.NewGuid(), Name = "D-Link" }
      //};
      //context.Brands.AddRange(brands);

      //var brand1 = brands.FirstOrDefault(b => b.Name == "Desktop Computers");
      //var category1 = categories.FirstOrDefault(b => b.Name == "Dell");

      //  context.Devices.AddOrUpdate(new List<Device>() {
      //     new Device{
      //       Id = Guid.NewGuid(),
      //       Brand = brand1,
      //       Category = category1,
      //       SerialNumber = "QB0456464",
      //       Description = "New laptop description",
      //       Status = DeviceStatus.InStock,
      //       Permission = Permission.Level2
      //     }
      //}.ToArray());


    }
  }
}
