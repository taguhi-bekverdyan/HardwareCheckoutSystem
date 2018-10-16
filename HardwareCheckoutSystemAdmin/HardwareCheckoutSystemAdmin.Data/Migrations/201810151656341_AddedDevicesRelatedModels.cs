namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDevicesRelatedModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.People", "ParentId", "public.People");
            DropIndex("public.People", new[] { "ParentId" });
            DropPrimaryKey("public.Devices");
            CreateTable(
                "public.Brands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Requests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        RentStartDate = c.DateTime(nullable: false),
                        RentEndDate = c.DateTime(nullable: false),
                        Message = c.String(),
                        LastResponseId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Devices", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("public.Responses", t => t.LastResponseId)
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.UserId)
                .Index(t => t.LastResponseId);
            
            CreateTable(
                "public.Responses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Message = c.String(),
                        NewStatus = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Request_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("public.Requests", t => t.Request_Id)
                .Index(t => t.UserId)
                .Index(t => t.Request_Id);
            
            CreateTable(
                "public.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        TelNumber = c.String(),
                        Permission = c.Int(nullable: false),
                        AvatarImage = c.Binary(),
                        Occupation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("public.Devices", "Id", c => c.Guid(nullable: false));
            AddColumn("public.Devices", "SerialNumber", c => c.String());
            AddColumn("public.Devices", "CategoryId", c => c.Guid(nullable: false));
            AddColumn("public.Devices", "BrandId", c => c.Guid(nullable: false));
            AddColumn("public.Devices", "Status", c => c.Int(nullable: false));
            AddColumn("public.Devices", "PermissionEnum", c => c.Int(nullable: false));
            AddPrimaryKey("public.Devices", "Id");
            CreateIndex("public.Devices", "CategoryId");
            CreateIndex("public.Devices", "BrandId");
            AddForeignKey("public.Devices", "BrandId", "public.Brands", "Id", cascadeDelete: true);
            AddForeignKey("public.Devices", "CategoryId", "public.Categories", "Id", cascadeDelete: true);
            DropColumn("public.Devices", "SN");
            DropColumn("public.Devices", "Category");
            DropColumn("public.Devices", "Brand");
            DropColumn("public.Devices", "Permission");
            DropTable("public.People");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        Address = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("public.Devices", "Permission", c => c.String());
            AddColumn("public.Devices", "Brand", c => c.String());
            AddColumn("public.Devices", "Category", c => c.String());
            AddColumn("public.Devices", "SN", c => c.Int(nullable: false, identity: true));
            DropForeignKey("public.Requests", "UserId", "public.Users");
            DropForeignKey("public.Responses", "Request_Id", "public.Requests");
            DropForeignKey("public.Requests", "LastResponseId", "public.Responses");
            DropForeignKey("public.Responses", "UserId", "public.Users");
            DropForeignKey("public.Requests", "DeviceId", "public.Devices");
            DropForeignKey("public.Devices", "CategoryId", "public.Categories");
            DropForeignKey("public.Devices", "BrandId", "public.Brands");
            DropIndex("public.Responses", new[] { "Request_Id" });
            DropIndex("public.Responses", new[] { "UserId" });
            DropIndex("public.Requests", new[] { "LastResponseId" });
            DropIndex("public.Requests", new[] { "UserId" });
            DropIndex("public.Requests", new[] { "DeviceId" });
            DropIndex("public.Devices", new[] { "BrandId" });
            DropIndex("public.Devices", new[] { "CategoryId" });
            DropPrimaryKey("public.Devices");
            DropColumn("public.Devices", "PermissionEnum");
            DropColumn("public.Devices", "Status");
            DropColumn("public.Devices", "BrandId");
            DropColumn("public.Devices", "CategoryId");
            DropColumn("public.Devices", "SerialNumber");
            DropColumn("public.Devices", "Id");
            DropTable("public.Users");
            DropTable("public.Responses");
            DropTable("public.Requests");
            DropTable("public.Categories");
            DropTable("public.Brands");
            AddPrimaryKey("public.Devices", "SN");
            CreateIndex("public.People", "ParentId");
            AddForeignKey("public.People", "ParentId", "public.People", "Id");
        }
    }
}
