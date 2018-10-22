namespace HardwareCheckoutSystemAdmin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HcsDBV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Brands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Devices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SerialNumber = c.String(nullable: false, maxLength: 50),
                        Model = c.String(maxLength: 50),
                        Description = c.String(),
                        MaxPeriod = c.DateTime(),
                        Image = c.Binary(),
                        Status = c.Int(nullable: false),
                        Permission = c.Int(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        BrandId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("public.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId);
            
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
                        RentStartDate = c.DateTime(),
                        RentEndDate = c.DateTime(),
                        Message = c.String(),
                        LastResponseId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Devices", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("public.Responses", t => t.LastResponseId)
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
                        FirstName = c.String(maxLength: 25),
                        LastName = c.String(maxLength: 25),
                        Address = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        TelNumber = c.String(maxLength: 25),
                        Permission = c.Int(nullable: false),
                        AvatarImage = c.Binary(),
                        Occupation = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Responses", "Request_Id", "public.Requests");
            DropForeignKey("public.Requests", "LastResponseId", "public.Responses");
            DropForeignKey("public.Responses", "UserId", "public.Users");
            DropForeignKey("public.Requests", "UserId", "public.Users");
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
            DropTable("public.Users");
            DropTable("public.Responses");
            DropTable("public.Requests");
            DropTable("public.Categories");
            DropTable("public.Devices");
            DropTable("public.Brands");
        }
    }
}
