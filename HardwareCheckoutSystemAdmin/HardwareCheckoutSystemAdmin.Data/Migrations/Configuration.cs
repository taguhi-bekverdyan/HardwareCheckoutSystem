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
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            /*
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Apple"});
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Samsung" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Google" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Asus" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Intel" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Amd" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Lenovo" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Canon" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Genius" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "LG" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Xiaomi" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Dell" });
            context.Brands.Add(new Models.Brand() { Id = Guid.NewGuid(), Name = "Msi" });

            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name="PC"});
            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name = "Laptop" });
            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name = "Accessoires" });
            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name = "Monitor" });
            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name = "Server" });
            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name = "CPU" });
            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name = "GPU" });
            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name = "Tablet" });
            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name = "Printer" });
            context.Categories.Add(new Models.Category() { Id = Guid.NewGuid(), Name = "RAM" });
            */
        }
    }
}
