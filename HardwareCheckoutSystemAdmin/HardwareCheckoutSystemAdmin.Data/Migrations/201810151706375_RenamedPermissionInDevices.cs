namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedPermissionInDevices : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Devices", "Permission", c => c.Int(nullable: false));
            DropColumn("public.Devices", "PermissionEnum");
        }
        
        public override void Down()
        {
            AddColumn("public.Devices", "PermissionEnum", c => c.Int(nullable: false));
            DropColumn("public.Devices", "Permission");
        }
    }
}
