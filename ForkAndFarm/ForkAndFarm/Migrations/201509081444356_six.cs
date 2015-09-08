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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ForkAndFarmCategories");
        }
    }
}
