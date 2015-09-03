namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ForkAndFarmUser_Id = c.String(),
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
                "dbo.Deals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AcceptedOn = c.DateTime(nullable: false),
                        AcceptanceComments = c.String(maxLength: 50),
                        Complete = c.Boolean(nullable: false),
                        Deal_Id = c.Int(nullable: false),
                        ProposedBy_Id = c.Int(nullable: false),
                        AcceptedBy_Id = c.Int(nullable: false),
                        Product = c.String(maxLength: 20),
                        Unit = c.String(maxLength: 10),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        ExtPrice = c.Double(nullable: false),
                        Delivery = c.DateTime(nullable: false),
                        PaymentTerms = c.String(maxLength: 10),
                        CreatedOn = c.DateTime(nullable: false),
                        Memo = c.String(maxLength: 50),
                        OfferedTo_Id = c.String(maxLength: 128),
                        ProposedBy_Id1 = c.String(maxLength: 128),
                        ForkAndFarmUser_Id = c.String(maxLength: 128),
                        ForkAndFarmUser_Id1 = c.String(maxLength: 128),
                        ForkAndFarmUser_Id2 = c.String(maxLength: 128),
                        ForkAndFarmUser_Id3 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OfferedTo_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProposedBy_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id2)
                .ForeignKey("dbo.AspNetUsers", t => t.ForkAndFarmUser_Id3)
                .Index(t => t.OfferedTo_Id)
                .Index(t => t.ProposedBy_Id1)
                .Index(t => t.ForkAndFarmUser_Id)
                .Index(t => t.ForkAndFarmUser_Id1)
                .Index(t => t.ForkAndFarmUser_Id2)
                .Index(t => t.ForkAndFarmUser_Id3);
            
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
            
            CreateTable(
                "dbo.PurchaseOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrder = c.String(),
                        PurchaseOffer_Id = c.Int(nullable: false),
                        Product = c.String(maxLength: 20),
                        Unit = c.String(maxLength: 10),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        ExtPrice = c.Double(nullable: false),
                        Delivery = c.DateTime(nullable: false),
                        PaymentTerms = c.String(maxLength: 10),
                        CreatedOn = c.DateTime(nullable: false),
                        Memo = c.String(maxLength: 50),
                        ProposedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProposedBy_Id)
                .Index(t => t.ProposedBy_Id);
            
            CreateTable(
                "dbo.SupplyOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Invoice = c.String(),
                        SupplyOffer_Id = c.Int(nullable: false),
                        Product = c.String(maxLength: 20),
                        Unit = c.String(maxLength: 10),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        ExtPrice = c.Double(nullable: false),
                        Delivery = c.DateTime(nullable: false),
                        PaymentTerms = c.String(maxLength: 10),
                        CreatedOn = c.DateTime(nullable: false),
                        Memo = c.String(maxLength: 50),
                        ProposedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProposedBy_Id)
                .Index(t => t.ProposedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplyOffers", "ProposedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseOffers", "ProposedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id3", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ProposedBy_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "OfferedTo_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.SupplyOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.PurchaseOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id3" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id2" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id1" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id" });
            DropIndex("dbo.Deals", new[] { "ProposedBy_Id1" });
            DropIndex("dbo.Deals", new[] { "OfferedTo_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.SupplyOffers");
            DropTable("dbo.PurchaseOffers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Deals");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
