namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class four : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Deals", "ProposedBy_Id", c => c.String());
            AlterColumn("dbo.Deals", "AcceptedBy_Id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Deals", "AcceptedBy_Id", c => c.Int());
            AlterColumn("dbo.Deals", "ProposedBy_Id", c => c.Int());
        }
    }
}
