namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class four : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Deals", name: "Profile_Id", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Deals", name: "Profile_Id2", newName: "Profile_Id");
            RenameColumn(table: "dbo.Deals", name: "AcceptedBy_Id1", newName: "Profile_Id3");
            RenameColumn(table: "dbo.Deals", name: "__mig_tmp__0", newName: "Profile_Id2");
            RenameIndex(table: "dbo.Deals", name: "IX_Profile_Id2", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Deals", name: "IX_Profile_Id", newName: "IX_Profile_Id2");
            RenameIndex(table: "dbo.Deals", name: "IX_AcceptedBy_Id1", newName: "IX_Profile_Id3");
            RenameIndex(table: "dbo.Deals", name: "__mig_tmp__0", newName: "IX_Profile_Id");
            CreateTable(
                "dbo.DealListVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ProposedBy = c.String(),
                        Quantity = c.Double(nullable: false),
                        Unit = c.String(),
                        Product = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        ExtPrice = c.Double(nullable: false),
                        Delivery = c.DateTime(nullable: false),
                        PaymentTerms = c.String(maxLength: 10),
                        Memo = c.String(maxLength: 50),
                        IsComplete = c.Boolean(nullable: false),
                        AcceptedBy = c.String(),
                        AcceptedOn = c.DateTime(nullable: false),
                        AcceptanceComments = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Deals", "OfferedTo_Id", c => c.Int());
            CreateIndex("dbo.Deals", "OfferedTo_Id");
            AddForeignKey("dbo.Deals", "OfferedTo_Id", "dbo.Profiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "OfferedTo_Id", "dbo.Profiles");
            DropIndex("dbo.Deals", new[] { "OfferedTo_Id" });
            DropColumn("dbo.Deals", "OfferedTo_Id");
            DropTable("dbo.DealListVMs");
            RenameIndex(table: "dbo.Deals", name: "IX_Profile_Id", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Deals", name: "IX_Profile_Id3", newName: "IX_AcceptedBy_Id1");
            RenameIndex(table: "dbo.Deals", name: "IX_Profile_Id2", newName: "IX_Profile_Id");
            RenameIndex(table: "dbo.Deals", name: "__mig_tmp__0", newName: "IX_Profile_Id2");
            RenameColumn(table: "dbo.Deals", name: "Profile_Id2", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Deals", name: "Profile_Id3", newName: "AcceptedBy_Id1");
            RenameColumn(table: "dbo.Deals", name: "Profile_Id", newName: "Profile_Id2");
            RenameColumn(table: "dbo.Deals", name: "__mig_tmp__0", newName: "Profile_Id");
        }
    }
}
