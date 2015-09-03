namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DealListVMs");
        }
    }
}
