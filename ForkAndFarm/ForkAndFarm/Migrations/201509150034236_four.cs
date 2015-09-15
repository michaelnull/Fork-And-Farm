namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class four : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deals", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CountNewResponses", c => c.Int(nullable: false));
            AlterColumn("dbo.Advertisements", "Unit", c => c.String(maxLength: 20));
            AlterColumn("dbo.Deals", "Unit", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Deals", "Unit", c => c.String(maxLength: 10));
            AlterColumn("dbo.Advertisements", "Unit", c => c.String(maxLength: 10));
            DropColumn("dbo.AspNetUsers", "CountNewResponses");
            DropColumn("dbo.Deals", "IsNew");
        }
    }
}
