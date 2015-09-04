namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Offers", newName: "Deals");
            DropForeignKey("dbo.Offers", "PurchaseOffer_Id", "dbo.Offers");
            DropForeignKey("dbo.Offers", "SupplyOffer_Id", "dbo.Offers");
            DropForeignKey("dbo.Offers", "Offer_Id", "dbo.Offers");
            DropIndex("dbo.Deals", new[] { "PurchaseOffer_Id" });
            DropIndex("dbo.Deals", new[] { "SupplyOffer_Id" });
            DropIndex("dbo.Deals", new[] { "Offer_Id" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id4" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id5" });
            RenameColumn(table: "dbo.PurchaseOffers", name: "ForkAndFarmUser_Id4", newName: "ForkAndFarmUser_Id");
            RenameColumn(table: "dbo.SupplyOffers", name: "ForkAndFarmUser_Id5", newName: "ForkAndFarmUser_Id");
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
                .Index(t => t.ForkAndFarmUser_Id);
            
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
                .Index(t => t.ForkAndFarmUser_Id);
            
            AddColumn("dbo.Deals", "OfferId", c => c.Int(nullable: false));
            AlterColumn("dbo.Deals", "Complete", c => c.Boolean(nullable: false));
            DropColumn("dbo.Deals", "PurchaseOrder");
            DropColumn("dbo.Deals", "Invoice");
            DropColumn("dbo.Deals", "Discriminator");
            DropColumn("dbo.Deals", "PurchaseOffer_Id");
            DropColumn("dbo.Deals", "SupplyOffer_Id");
            DropColumn("dbo.Deals", "Offer_Id");
            DropColumn("dbo.Deals", "ForkAndFarmUser_Id4");
            DropColumn("dbo.Deals", "ForkAndFarmUser_Id5");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deals", "ForkAndFarmUser_Id5", c => c.String(maxLength: 128));
            AddColumn("dbo.Deals", "ForkAndFarmUser_Id4", c => c.String(maxLength: 128));
            AddColumn("dbo.Deals", "Offer_Id", c => c.Int());
            AddColumn("dbo.Deals", "SupplyOffer_Id", c => c.Int());
            AddColumn("dbo.Deals", "PurchaseOffer_Id", c => c.Int());
            AddColumn("dbo.Deals", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Deals", "Invoice", c => c.String());
            AddColumn("dbo.Deals", "PurchaseOrder", c => c.String());
            DropIndex("dbo.SupplyOffers", new[] { "ForkAndFarmUser_Id" });
            DropIndex("dbo.PurchaseOffers", new[] { "ForkAndFarmUser_Id" });
            AlterColumn("dbo.Deals", "Complete", c => c.Boolean());
            DropColumn("dbo.Deals", "OfferId");
            DropTable("dbo.SupplyOffers");
            DropTable("dbo.PurchaseOffers");
            RenameColumn(table: "dbo.SupplyOffers", name: "ForkAndFarmUser_Id", newName: "ForkAndFarmUser_Id5");
            RenameColumn(table: "dbo.PurchaseOffers", name: "ForkAndFarmUser_Id", newName: "ForkAndFarmUser_Id4");
            CreateIndex("dbo.Deals", "ForkAndFarmUser_Id5");
            CreateIndex("dbo.Deals", "ForkAndFarmUser_Id4");
            CreateIndex("dbo.Deals", "Offer_Id");
            CreateIndex("dbo.Deals", "SupplyOffer_Id");
            CreateIndex("dbo.Deals", "PurchaseOffer_Id");
            AddForeignKey("dbo.Offers", "Offer_Id", "dbo.Offers", "Id");
            AddForeignKey("dbo.Offers", "SupplyOffer_Id", "dbo.Offers", "Id");
            AddForeignKey("dbo.Offers", "PurchaseOffer_Id", "dbo.Offers", "Id");
            RenameTable(name: "dbo.Deals", newName: "Offers");
        }
    }
}
