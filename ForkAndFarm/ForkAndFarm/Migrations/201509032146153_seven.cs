namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seven : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Deals", new[] { "OfferedTo_Id1" });
            DropIndex("dbo.Deals", new[] { "ProposedBy_Id1" });
            DropIndex("dbo.PurchaseOffers", new[] { "ProposedBy_Id1" });
            DropIndex("dbo.SupplyOffers", new[] { "ProposedBy_Id1" });
            DropColumn("dbo.Deals", "OfferedTo_Id");
            DropColumn("dbo.Deals", "ProposedBy_Id");
            DropColumn("dbo.PurchaseOffers", "ProposedBy_Id");
            DropColumn("dbo.SupplyOffers", "ProposedBy_Id");
            RenameColumn(table: "dbo.Deals", name: "OfferedTo_Id1", newName: "OfferedTo_Id");
            RenameColumn(table: "dbo.Deals", name: "ProposedBy_Id1", newName: "ProposedBy_Id");
            RenameColumn(table: "dbo.PurchaseOffers", name: "ProposedBy_Id1", newName: "ProposedBy_Id");
            RenameColumn(table: "dbo.SupplyOffers", name: "ProposedBy_Id1", newName: "ProposedBy_Id");
            AlterColumn("dbo.Deals", "OfferedTo_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Deals", "ProposedBy_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.PurchaseOffers", "ProposedBy_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SupplyOffers", "ProposedBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Deals", "OfferedTo_Id");
            CreateIndex("dbo.Deals", "ProposedBy_Id");
            CreateIndex("dbo.PurchaseOffers", "ProposedBy_Id");
            CreateIndex("dbo.SupplyOffers", "ProposedBy_Id");
            DropColumn("dbo.Deals", "Deal_Id");
            DropColumn("dbo.Deals", "AcceptedBy_Id");
            DropColumn("dbo.AspNetUsers", "ForkAndFarmUser_Id");
            DropColumn("dbo.PurchaseOffers", "PurchaseOffer_Id");
            DropColumn("dbo.SupplyOffers", "SupplyOffer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SupplyOffers", "SupplyOffer_Id", c => c.Int());
            AddColumn("dbo.PurchaseOffers", "PurchaseOffer_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ForkAndFarmUser_Id", c => c.String());
            AddColumn("dbo.Deals", "AcceptedBy_Id", c => c.String());
            AddColumn("dbo.Deals", "Deal_Id", c => c.Int());
            DropIndex("dbo.SupplyOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.PurchaseOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.Deals", new[] { "ProposedBy_Id" });
            DropIndex("dbo.Deals", new[] { "OfferedTo_Id" });
            AlterColumn("dbo.SupplyOffers", "ProposedBy_Id", c => c.String());
            AlterColumn("dbo.PurchaseOffers", "ProposedBy_Id", c => c.String());
            AlterColumn("dbo.Deals", "ProposedBy_Id", c => c.String());
            AlterColumn("dbo.Deals", "OfferedTo_Id", c => c.String());
            RenameColumn(table: "dbo.SupplyOffers", name: "ProposedBy_Id", newName: "ProposedBy_Id1");
            RenameColumn(table: "dbo.PurchaseOffers", name: "ProposedBy_Id", newName: "ProposedBy_Id1");
            RenameColumn(table: "dbo.Deals", name: "ProposedBy_Id", newName: "ProposedBy_Id1");
            RenameColumn(table: "dbo.Deals", name: "OfferedTo_Id", newName: "OfferedTo_Id1");
            AddColumn("dbo.SupplyOffers", "ProposedBy_Id", c => c.String());
            AddColumn("dbo.PurchaseOffers", "ProposedBy_Id", c => c.String());
            AddColumn("dbo.Deals", "ProposedBy_Id", c => c.String());
            AddColumn("dbo.Deals", "OfferedTo_Id", c => c.String());
            CreateIndex("dbo.SupplyOffers", "ProposedBy_Id1");
            CreateIndex("dbo.PurchaseOffers", "ProposedBy_Id1");
            CreateIndex("dbo.Deals", "ProposedBy_Id1");
            CreateIndex("dbo.Deals", "OfferedTo_Id1");
        }
    }
}
