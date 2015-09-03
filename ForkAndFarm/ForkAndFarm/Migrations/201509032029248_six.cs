namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class six : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseOffers", "ProposedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SupplyOffers", "ProposedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PurchaseOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.SupplyOffers", new[] { "ProposedBy_Id" });
            AddColumn("dbo.PurchaseOffers", "ProposedBy_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.SupplyOffers", "ProposedBy_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.PurchaseOffers", "ProposedBy_Id", c => c.String());
            AlterColumn("dbo.SupplyOffers", "ProposedBy_Id", c => c.String());
            CreateIndex("dbo.PurchaseOffers", "ProposedBy_Id1");
            CreateIndex("dbo.SupplyOffers", "ProposedBy_Id1");
            AddForeignKey("dbo.PurchaseOffers", "ProposedBy_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.SupplyOffers", "ProposedBy_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplyOffers", "ProposedBy_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseOffers", "ProposedBy_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.SupplyOffers", new[] { "ProposedBy_Id1" });
            DropIndex("dbo.PurchaseOffers", new[] { "ProposedBy_Id1" });
            AlterColumn("dbo.SupplyOffers", "ProposedBy_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.PurchaseOffers", "ProposedBy_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.SupplyOffers", "ProposedBy_Id1");
            DropColumn("dbo.PurchaseOffers", "ProposedBy_Id1");
            CreateIndex("dbo.SupplyOffers", "ProposedBy_Id");
            CreateIndex("dbo.PurchaseOffers", "ProposedBy_Id");
            AddForeignKey("dbo.SupplyOffers", "ProposedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PurchaseOffers", "ProposedBy_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
