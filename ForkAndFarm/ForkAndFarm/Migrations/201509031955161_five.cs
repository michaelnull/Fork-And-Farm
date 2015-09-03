namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class five : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "OfferedTo_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Deals", new[] { "OfferedTo_Id" });
            AddColumn("dbo.Deals", "OfferedTo_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Deals", "OfferedTo_Id", c => c.String());
            CreateIndex("dbo.Deals", "OfferedTo_Id1");
            AddForeignKey("dbo.Deals", "OfferedTo_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "OfferedTo_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Deals", new[] { "OfferedTo_Id1" });
            AlterColumn("dbo.Deals", "OfferedTo_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Deals", "OfferedTo_Id1");
            CreateIndex("dbo.Deals", "OfferedTo_Id");
            AddForeignKey("dbo.Deals", "OfferedTo_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
