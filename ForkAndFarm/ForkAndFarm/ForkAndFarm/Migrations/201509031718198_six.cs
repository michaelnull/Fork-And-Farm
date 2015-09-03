namespace ForkAndFarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class six : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Deals", "Profile_Id1", "dbo.Profiles");
            DropForeignKey("dbo.Deals", "Profile_Id2", "dbo.Profiles");
            DropForeignKey("dbo.Deals", "Profile_Id3", "dbo.Profiles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Deals", new[] { "Profile_Id" });
            DropIndex("dbo.Deals", new[] { "Profile_Id1" });
            DropIndex("dbo.Deals", new[] { "Profile_Id2" });
            DropIndex("dbo.Deals", new[] { "Profile_Id3" });
            DropIndex("dbo.Deals", new[] { "OfferedTo_Id" });
            DropIndex("dbo.Deals", new[] { "ProposedBy_Id1" });
            DropIndex("dbo.PurchaseOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.SupplyOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Profile_Id" });
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ForkAndFarmUser_Id = c.String(),
                        Organization = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Deals", "ForkAndFarmUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Deals", "ForkAndFarmUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Deals", "ForkAndFarmUser_Id2", c => c.String(maxLength: 128));
            AddColumn("dbo.Deals", "ForkAndFarmUser_Id3", c => c.String(maxLength: 128));
            AlterColumn("dbo.Deals", "OfferedTo_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Deals", "ProposedBy_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.PurchaseOffers", "ProposedBy_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SupplyOffers", "ProposedBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Deals", "ForkAndFarmUser_Id");
            CreateIndex("dbo.Deals", "ForkAndFarmUser_Id1");
            CreateIndex("dbo.Deals", "ForkAndFarmUser_Id2");
            CreateIndex("dbo.Deals", "ForkAndFarmUser_Id3");
            CreateIndex("dbo.Deals", "OfferedTo_Id");
            CreateIndex("dbo.Deals", "ProposedBy_Id1");
            CreateIndex("dbo.PurchaseOffers", "ProposedBy_Id");
            CreateIndex("dbo.SupplyOffers", "ProposedBy_Id");
            AddForeignKey("dbo.Deals", "ForkAndFarmUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Deals", "ForkAndFarmUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Deals", "ForkAndFarmUser_Id2", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Deals", "ForkAndFarmUser_Id3", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Deals", "Profile_Id");
            DropColumn("dbo.Deals", "Profile_Id1");
            DropColumn("dbo.Deals", "Profile_Id2");
            DropColumn("dbo.Deals", "Profile_Id3");
            DropTable("dbo.Profiles");
            DropTable("dbo.AspNetUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Profile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Organization = c.String(nullable: false, maxLength: 20),
                        Address = c.String(maxLength: 20),
                        Address2 = c.String(maxLength: 20),
                        Zip = c.String(),
                        Role = c.Int(nullable: false),
                        Profile_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Deals", "Profile_Id3", c => c.Int());
            AddColumn("dbo.Deals", "Profile_Id2", c => c.Int());
            AddColumn("dbo.Deals", "Profile_Id1", c => c.Int());
            AddColumn("dbo.Deals", "Profile_Id", c => c.Int());
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id3", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Deals", "ForkAndFarmUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SupplyOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.PurchaseOffers", new[] { "ProposedBy_Id" });
            DropIndex("dbo.Deals", new[] { "ProposedBy_Id1" });
            DropIndex("dbo.Deals", new[] { "OfferedTo_Id" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id3" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id2" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id1" });
            DropIndex("dbo.Deals", new[] { "ForkAndFarmUser_Id" });
            AlterColumn("dbo.SupplyOffers", "ProposedBy_Id", c => c.Int());
            AlterColumn("dbo.PurchaseOffers", "ProposedBy_Id", c => c.Int());
            AlterColumn("dbo.Deals", "ProposedBy_Id1", c => c.Int());
            AlterColumn("dbo.Deals", "OfferedTo_Id", c => c.Int());
            DropColumn("dbo.Deals", "ForkAndFarmUser_Id3");
            DropColumn("dbo.Deals", "ForkAndFarmUser_Id2");
            DropColumn("dbo.Deals", "ForkAndFarmUser_Id1");
            DropColumn("dbo.Deals", "ForkAndFarmUser_Id");
            DropTable("dbo.AspNetUsers");
            CreateIndex("dbo.AspNetUsers", "Profile_Id");
            CreateIndex("dbo.SupplyOffers", "ProposedBy_Id");
            CreateIndex("dbo.PurchaseOffers", "ProposedBy_Id");
            CreateIndex("dbo.Deals", "ProposedBy_Id1");
            CreateIndex("dbo.Deals", "OfferedTo_Id");
            CreateIndex("dbo.Deals", "Profile_Id3");
            CreateIndex("dbo.Deals", "Profile_Id2");
            CreateIndex("dbo.Deals", "Profile_Id1");
            CreateIndex("dbo.Deals", "Profile_Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "Profile_Id", "dbo.Profiles", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Deals", "Profile_Id3", "dbo.Profiles", "Id");
            AddForeignKey("dbo.Deals", "Profile_Id2", "dbo.Profiles", "Id");
            AddForeignKey("dbo.Deals", "Profile_Id1", "dbo.Profiles", "Id");
            AddForeignKey("dbo.Deals", "Profile_Id", "dbo.Profiles", "Id");
        }
    }
}
