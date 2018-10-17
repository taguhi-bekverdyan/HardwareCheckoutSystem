namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HcsV2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.Devices", "SerialNumber", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("public.Devices", "SerialNumber", c => c.String(maxLength: 50));
        }
    }
}
