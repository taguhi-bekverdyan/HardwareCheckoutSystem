namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedForeignKey : DbMigration
    {
        public override void Up()
        {
            DropIndex("public.People", new[] { "ParentId" });
            AlterColumn("public.People", "ParentId", c => c.Int());
            CreateIndex("public.People", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("public.People", new[] { "ParentId" });
            AlterColumn("public.People", "ParentId", c => c.Int(nullable: false));
            CreateIndex("public.People", "ParentId");
        }
    }
}
