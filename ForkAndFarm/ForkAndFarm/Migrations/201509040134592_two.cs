namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Deals", newName: "Offers");
            DropForeignKey("dbo.PurchaseOffers", "ForkAndFarmUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SupplyOffers", "ForkAndFarmUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PurchaseOffers", new[] { "ForkAndFarmUser_Id" });
            DropIndex("dbo.SupplyOffers", new[] { "ForkAndFarmUser_Id" });
            AddColumn("dbo.Offers", "PurchaseOrder", c => c.String());
            AddColumn("dbo.Offers", "Invoice", c => c.String());
            AddColumn("dbo.Offers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Offers", "PurchaseOffer_Id", c => c.Int());
            AddColumn("dbo.Offers", "SupplyOffer_Id", c => c.Int());
            AddColumn("dbo.Offers", "Offer_Id", c => c.Int());
            AddColumn("dbo.Offers", "ForkAndFarmUser_Id4", c => c.String(maxLength: 128));
            AddColumn("dbo.Offers", "ForkAndFarmUser_Id5", c => c.String(maxLength: 128));
            AlterColumn("dbo.Offers", "Complete", c => c.Boolean());
            CreateIndex("dbo.Offers", "PurchaseOffer_Id");
            CreateIndex("dbo.Offers", "SupplyOffer_Id");
            CreateIndex("dbo.Offers", "Offer_Id");
            CreateIndex("dbo.Offers", "ForkAndFarmUser_Id4");
            CreateIndex("dbo.Offers", "ForkAndFarmUser_Id5");
            AddForeignKey("dbo.Offers", "PurchaseOffer_Id", "dbo.Offers", "Id");
            AddForeignKey("dbo.Offers", "SupplyOffer_Id", "dbo.Offers", "Id");
            AddForeignKey("dbo.Offers", "Offer_Id", "dbo.Offers", "Id");
            AddForeignKey("dbo.Offers", "ForkAndFarmUser_Id4", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Offers", "ForkAndFarmUser_Id5", "dbo.AspNetUsers", "Id");
            DropTable("dbo.PurchaseOffers");
            DropTable("dbo.SupplyOffers");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Offers", "ForkAndFarmUser_Id5", "dbo.AspNetUsers");
            DropForeignKey("dbo.Offers", "ForkAndFarmUser_Id4", "dbo.AspNetUsers");
            DropForeignKey("dbo.Offers", "Offer_Id", "dbo.Offers");
            DropForeignKey("dbo.Offers", "SupplyOffer_Id", "dbo.Offers");
            DropForeignKey("dbo.Offers", "PurchaseOffer_Id", "dbo.Offers");
            DropIndex("dbo.Offers", new[] { "ForkAndFarmUser_Id5" });
            DropIndex("dbo.Offers", new[] { "ForkAndFarmUser_Id4" });
            DropIndex("dbo.Offers", new[] { "Offer_Id" });
            DropIndex("dbo.Offers", new[] { "SupplyOffer_Id" });
            DropIndex("dbo.Offers", new[] { "PurchaseOffer_Id" });
            AlterColumn("dbo.Offers", "Complete", c => c.Boolean(nullable: false));
            DropColumn("dbo.Offers", "ForkAndFarmUser_Id5");
            DropColumn("dbo.Offers", "ForkAndFarmUser_Id4");
            DropColumn("dbo.Offers", "Offer_Id");
            DropColumn("dbo.Offers", "SupplyOffer_Id");
            DropColumn("dbo.Offers", "PurchaseOffer_Id");
            DropColumn("dbo.Offers", "Discriminator");
            DropColumn("dbo.Offers", "Invoice");
            DropColumn("dbo.Offers", "PurchaseOrder");
            CreateIndex("dbo.SupplyOffers", "ForkAndFarmUser_Id");
            CreateIndex("dbo.PurchaseOffers", "ForkAndFarmUser_Id");
            AddForeignKey("dbo.SupplyOffers", "ForkAndFarmUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PurchaseOffers", "ForkAndFarmUser_Id", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.Offers", newName: "Deals");
        }
    }
}
