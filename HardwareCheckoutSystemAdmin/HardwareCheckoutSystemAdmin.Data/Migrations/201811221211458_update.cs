namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.Devices", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("public.Devices", "Image", c => c.String());
        }
    }
}
