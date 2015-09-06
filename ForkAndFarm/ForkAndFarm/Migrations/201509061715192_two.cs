namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Deals", "Delivery", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseOffers", "Delivery", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SupplyOffers", "Delivery", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SupplyOffers", "Delivery", c => c.DateTime());
            AlterColumn("dbo.PurchaseOffers", "Delivery", c => c.DateTime());
            AlterColumn("dbo.Deals", "Delivery", c => c.DateTime());
        }
    }
}
