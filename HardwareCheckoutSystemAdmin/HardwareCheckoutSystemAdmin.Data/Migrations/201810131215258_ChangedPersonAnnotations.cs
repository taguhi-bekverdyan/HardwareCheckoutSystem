namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPersonAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.People", "FirstName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("public.People", "FirstName", c => c.String());
        }
    }
}
