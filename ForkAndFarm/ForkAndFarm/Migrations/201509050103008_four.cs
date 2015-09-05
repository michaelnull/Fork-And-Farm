namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class four : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deals", "PurchaseOffer_Id", c => c.Int());
            AddColumn("dbo.Deals", "SupplyOffer_Id", c => c.Int());
            CreateIndex("dbo.Deals", "PurchaseOffer_Id");
            CreateIndex("dbo.Deals", "SupplyOffer_Id");
            AddForeignKey("dbo.Deals", "PurchaseOffer_Id", "dbo.PurchaseOffers", "Id");
            AddForeignKey("dbo.Deals", "SupplyOffer_Id", "dbo.SupplyOffers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "SupplyOffer_Id", "dbo.SupplyOffers");
            DropForeignKey("dbo.Deals", "PurchaseOffer_Id", "dbo.PurchaseOffers");
            DropIndex("dbo.Deals", new[] { "SupplyOffer_Id" });
            DropIndex("dbo.Deals", new[] { "PurchaseOffer_Id" });
            DropColumn("dbo.Deals", "SupplyOffer_Id");
            DropColumn("dbo.Deals", "PurchaseOffer_Id");
        }
    }
}
