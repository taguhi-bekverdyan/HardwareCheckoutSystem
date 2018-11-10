using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using HardwareCheckoutSystemAdmin.Models;
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
            //context.Categories.AddRange(new List<Category>()
            //    {
            //  new Category { Id=Guid.NewGuid(), Name="Desktop Computers"},
            //  new Category { Id = Guid.NewGuid(), Name = "Servers" },
            //  new Category { Id = Guid.NewGuid(), Name = "Monitors" },
            //  new Category { Id = Guid.NewGuid(), Name = "Accessories" },
            //  new Category { Id = Guid.NewGuid(), Name = "Network Equipments" },
            //  new Category { Id = Guid.NewGuid(), Name = "Printers and Scanners" },
            //  new Category { Id = Guid.NewGuid(), Name = "Laptops" }
            //   });

            context.Brands.AddRange(new List<Brand>()
                 {  new Brand ("Dell"),
                     new Brand ("TP-Link"),
                     new Brand ("D-Link")
            });
        }
    }
}
