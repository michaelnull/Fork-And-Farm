namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Deals", "AcceptedOn", c => c.DateTime());
            AlterColumn("dbo.Deals", "Deal_Id", c => c.Int());
            AlterColumn("dbo.Deals", "ProposedBy_Id", c => c.Int());
            AlterColumn("dbo.Deals", "AcceptedBy_Id", c => c.Int());
            AlterColumn("dbo.Deals", "Delivery", c => c.DateTime());
            AlterColumn("dbo.Deals", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.PurchaseOffers", "PurchaseOffer_Id", c => c.Int());
            AlterColumn("dbo.PurchaseOffers", "Delivery", c => c.DateTime());
            AlterColumn("dbo.PurchaseOffers", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.SupplyOffers", "SupplyOffer_Id", c => c.Int());
            AlterColumn("dbo.SupplyOffers", "Delivery", c => c.DateTime());
            AlterColumn("dbo.SupplyOffers", "CreatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SupplyOffers", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SupplyOffers", "Delivery", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SupplyOffers", "SupplyOffer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.PurchaseOffers", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseOffers", "Delivery", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseOffers", "PurchaseOffer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Deals", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Deals", "Delivery", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Deals", "AcceptedBy_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Deals", "ProposedBy_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Deals", "Deal_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Deals", "AcceptedOn", c => c.DateTime(nullable: false));
        }
    }
}
