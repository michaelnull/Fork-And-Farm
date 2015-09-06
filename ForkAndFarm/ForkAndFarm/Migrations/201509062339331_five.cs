namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class five : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deals", "ProposedByOrganization", c => c.String());
            AddColumn("dbo.Deals", "ProposedByPhone", c => c.String());
            AddColumn("dbo.PurchaseOffers", "ProposedByOrganization", c => c.String());
            AddColumn("dbo.PurchaseOffers", "ProposedByPhone", c => c.String());
            AddColumn("dbo.SupplyOffers", "ProposedByOrganization", c => c.String());
            AddColumn("dbo.SupplyOffers", "ProposedByPhone", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.SupplyOffers", "ProposedByPhone");
            DropColumn("dbo.SupplyOffers", "ProposedByOrganization");
            DropColumn("dbo.PurchaseOffers", "ProposedByPhone");
            DropColumn("dbo.PurchaseOffers", "ProposedByOrganization");
            DropColumn("dbo.Deals", "ProposedByPhone");
            DropColumn("dbo.Deals", "ProposedByOrganization");
        }
    }
}
