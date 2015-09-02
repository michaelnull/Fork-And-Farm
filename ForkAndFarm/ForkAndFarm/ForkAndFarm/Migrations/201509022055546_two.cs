namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
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
                        Profile_Id = c.Int(),
                        Profile_Id1 = c.Int(),
                        Profile_Id2 = c.Int(),
                        AcceptedBy_Id1 = c.Int(),
                        ProposedBy_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id1)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id2)
                .ForeignKey("dbo.Profiles", t => t.AcceptedBy_Id1)
                .ForeignKey("dbo.Profiles", t => t.ProposedBy_Id1)
                .Index(t => t.Profile_Id)
                .Index(t => t.Profile_Id1)
                .Index(t => t.Profile_Id2)
                .Index(t => t.AcceptedBy_Id1)
                .Index(t => t.ProposedBy_Id1);
            
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
                        ProposedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProposedBy_Id)
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
                        ProposedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProposedBy_Id)
                .Index(t => t.ProposedBy_Id);
            
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Id", c => c.String());
            AddColumn("dbo.Profiles", "Profile_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "ProposedBy_Id1", "dbo.Profiles");
            DropForeignKey("dbo.Deals", "AcceptedBy_Id1", "dbo.Profiles");
            DropForeignKey("dbo.SupplyOffers", "ProposedBy_Id", "dbo.Profiles");
            DropForeignKey("dbo.PurchaseOffers", "ProposedBy_Id", "dbo.Profiles");
            DropForeignKey("dbo.Deals", "Profile_Id2", "dbo.Profiles");
            DropForeignKey("dbo.Deals", "Profile_Id1", "dbo.Profiles");
            DropForeignKey("dbo.Deals", "Profile_Id", "dbo.Profiles");
            DropIndex("dbo.SupplyOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.PurchaseOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.Deals", new[] { "ProposedBy_Id1" });
            DropIndex("dbo.Deals", new[] { "AcceptedBy_Id1" });
            DropIndex("dbo.Deals", new[] { "Profile_Id2" });
            DropIndex("dbo.Deals", new[] { "Profile_Id1" });
            DropIndex("dbo.Deals", new[] { "Profile_Id" });
            DropColumn("dbo.Profiles", "Profile_Id");
            DropColumn("dbo.AspNetUsers", "ApplicationUser_Id");
            DropTable("dbo.SupplyOffers");
            DropTable("dbo.PurchaseOffers");
            DropTable("dbo.Deals");
        }
    }
}
