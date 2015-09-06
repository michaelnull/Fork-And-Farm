namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class five : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id2", "dbo.AspNetUsers");
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id1" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id2" });
            DropColumn("dbo.Deals", "ForkAndFarmUser_Id");
            RenameColumn(table: "dbo.Deals", name: "ForkAndFarmUser_Id1", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Deals", name: "ForkAndFarmUser_Id3", newName: "ForkAndFarmUser_Id1");
            RenameColumn(table: "dbo.Deals", name: "__mig_tmp__0", newName: "ForkAndFarmUser_Id");
            RenameIndex(table: "dbo.Deals", name: "IX_ForkAndFarmUser_Id3", newName: "IX_ForkAndFarmUser_Id1");
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.Int(nullable: false));
            DropColumn("dbo.Deals", "ForkAndFarmUser_Id2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deals", "ForkAndFarmUser_Id2", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "UserRole");
            RenameIndex(table: "dbo.Deals", name: "IX_ForkAndFarmUser_Id1", newName: "IX_ForkAndFarmUser_Id3");
            RenameColumn(table: "dbo.Deals", name: "ForkAndFarmUser_Id", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Deals", name: "ForkAndFarmUser_Id1", newName: "ForkAndFarmUser_Id3");
            RenameColumn(table: "dbo.Deals", name: "__mig_tmp__0", newName: "ForkAndFarmUser_Id1");
            AddColumn("dbo.Deals", "ForkAndFarmUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Deals", "ForkAndFarmUser_Id2");
            CreateIndex("dbo.Deals", "ForkAndFarmUser_Id1");
            AddForeignKey("dbo.Deals", "ForkAndFarmUser_Id2", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Deals", "ForkAndFarmUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
