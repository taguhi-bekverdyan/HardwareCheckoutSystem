namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParent : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.People", "ParentId", c => c.Int(nullable: true));
            CreateIndex("public.People", "ParentId");
            AddForeignKey("public.People", "ParentId", "public.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("public.People", "ParentId", "public.People");
            DropIndex("public.People", new[] { "ParentId" });
            DropColumn("public.People", "ParentId");
        }
    }
}
