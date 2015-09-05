namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Deals", "Complete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deals", "Complete", c => c.Boolean(nullable: false));
        }
    }
}
