namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDevices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Devices",
                c => new
                    {
                        SN = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Brand = c.String(),
                        Model = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Permission = c.String(),
                        MaxPeriod = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SN);
            
        }
        
        public override void Down()
        {
            DropTable("public.Devices");
        }
    }
}
