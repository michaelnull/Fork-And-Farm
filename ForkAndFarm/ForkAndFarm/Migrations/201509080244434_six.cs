namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class six : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForkAndFarmCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ListName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForkAndFarmItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(maxLength: 25),
                        ForkAndFarmCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForkAndFarmCategories", t => t.ForkAndFarmCategory_Id)
                .Index(t => t.ForkAndFarmCategory_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForkAndFarmItems", "ForkAndFarmCategory_Id", "dbo.ForkAndFarmCategories");
            DropIndex("dbo.ForkAndFarmItems", new[] { "ForkAndFarmCategory_Id" });
            DropTable("dbo.ForkAndFarmItems");
            DropTable("dbo.ForkAndFarmCategories");
        }
    }
}
