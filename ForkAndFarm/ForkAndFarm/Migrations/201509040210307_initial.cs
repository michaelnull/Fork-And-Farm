namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DealListVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ProposedBy = c.String(),
                        Quantity = c.Double(nullable: false),
                        Unit = c.String(),
                        Product = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        ExtPrice = c.Double(nullable: false),
                        Delivery = c.DateTime(nullable: false),
                        PaymentTerms = c.String(maxLength: 10),
                        Memo = c.String(maxLength: 50),
                        IsComplete = c.Boolean(nullable: false),
                        AcceptedBy = c.String(),
                        AcceptedOn = c.DateTime(nullable: false),
                        AcceptanceComments = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Deals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfferedTo = c.String(),
                        AcceptedOn = c.DateTime(),
                        AcceptanceComments = c.String(maxLength: 50),
                        Complete = c.Boolean(nullable: false),
                        OfferId = c.Int(nullable: false),
                        ProposedBy = c.String(),
                        Product = c.String(maxLength: 20),
                        Unit = c.String(maxLength: 10),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        ExtPrice = c.Double(nullable: false),
                        Delivery = c.DateTime(),
                        PaymentTerms = c.String(maxLength: 10),
                        CreatedOn = c.DateTime(),
                        Memo = c.String(maxLength: 50),
                        ForkAndFarmUser_Id = c.String(maxLength: 128),
                        ForkAndFarmUser_Id1 = c.String(maxLength: 128),
                        ForkAndFarmUser_Id2 = c.String(maxLength: 128),
                        ForkAndFarmUser_Id3 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id2)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id3)
                .Index(t => t.ForkAndFarmUser_Id)
                .Index(t => t.ForkAndFarmUser_Id1)
                .Index(t => t.ForkAndFarmUser_Id2)
                .Index(t => t.ForkAndFarmUser_Id3);
            
            CreateTable(
                "dbo.PurchaseOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrder = c.String(),
                        ProposedBy = c.String(),
                        Product = c.String(maxLength: 20),
                        Unit = c.String(maxLength: 10),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        ExtPrice = c.Double(nullable: false),
                        Delivery = c.DateTime(),
                        PaymentTerms = c.String(maxLength: 10),
                        CreatedOn = c.DateTime(),
                        Memo = c.String(maxLength: 50),
                        ForkAndFarmUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id)
                .Index(t => t.ForkAndFarmUser_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SupplyOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Invoice = c.String(),
                        ProposedBy = c.String(),
                        Product = c.String(maxLength: 20),
                        Unit = c.String(maxLength: 10),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        ExtPrice = c.Double(nullable: false),
                        Delivery = c.DateTime(),
                        PaymentTerms = c.String(maxLength: 10),
                        CreatedOn = c.DateTime(),
                        Memo = c.String(maxLength: 50),
                        ForkAndFarmUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id)
                .Index(t => t.ForkAndFarmUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Organization = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplyOffers", "ForkAndFarmUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseOffers", "ForkAndFarmUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id3", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SupplyOffers", new[] { "ForkAndFarmUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PurchaseOffers", new[] { "ForkAndFarmUser_Id" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id3" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id2" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id1" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SupplyOffers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PurchaseOffers");
            DropTable("dbo.Deals");
            DropTable("dbo.DealListVMs");
        }
    }
}
