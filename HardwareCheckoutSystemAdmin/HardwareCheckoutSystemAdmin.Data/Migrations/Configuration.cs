using HardwareCheckoutSystemAdmin.Models;

namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
  using System;
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

      context.Persons.Add(new Person { Address = "Yerevan", Age = 12, FirstName = "Narek", LastName = "Smith", Id = 3, ParentId = null });
      context.Persons.Add(new Person { Address = "Yerevan", Age = 12, FirstName = "John", LastName = "Smith", Id = 2, ParentId = 3 });
      context.Persons.Add(new Person { Address = "Yerevan", Age = 12, FirstName = "Artur", LastName = "Smith", Id = 4, ParentId = 3 });
      context.Persons.Add(new Person { Address = "Yerevan", Age = 12, FirstName = "Gago", LastName = "Smith", Id = 1, ParentId = 2 });
      context.Persons.Add(new Person { Address = "Yerevan", Age = 13, FirstName = "Lucho", LastName = "Smith", Id = 5, ParentId = 1 });

      context.Devices.Add(new Device { SN = 001, Brand = "Dell", Model = "i7", Description = "Laptop" });
    }
  }
}
