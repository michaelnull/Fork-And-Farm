namespace ForkAndFarm.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ForkAndFarm.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ForkAndFarm.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var userStore = new UserStore<ForkAndFarmUser>(context);
            var userManager = new UserManager<ForkAndFarmUser>(userStore);
            if (!(context.Users.Any(u => u.UserName == "bob@buyer.com")))
            {
                var userToInsert = new ForkAndFarmUser { UserName = "bob@buyer.com", Organization = "Bob's Arkansas Grown Groceries", UserRole = ForkAndFarmUser.Portal.Purchaser, Phone = "5017654321" };
                userManager.Create(userToInsert, "Abc123!@#");
            }
            if (!(context.Users.Any(u => u.UserName == "fred@farmer.com")))
            {
                var userToInsert = new ForkAndFarmUser { UserName = "fred@farmer.com", Organization = "Fred's Natural and Organic Farm", UserRole = ForkAndFarmUser.Portal.Supplier, Phone = "5012222222" };
                userManager.Create(userToInsert, "Abc123!@#");
            }
            context.SupplyOffers.AddOrUpdate(
                x => x.Memo,
                new SupplyOffer
                {
                    Memo = "Example 1",
                    CreatedOn = DateTime.Now,
                    Invoice = "1",
                    PaymentTerms = "cod",
                    Product = "Potatoes",
                    ProposedBy = "fred@farmer.com",
                    Quantity = 100,
                    Unit = "lb",
                    UnitPrice = 1.25,
                    Delivery = DateTime.Today,
                    ExtPrice = 125,
                    ProposedByOrganization = "Fred's Farm",
                    ProposedByPhone = "501-123-4567"


                },

                new SupplyOffer
                {
                    Memo = "Example 2",
                    CreatedOn = DateTime.Now,
                    Invoice = "2",
                    PaymentTerms = "cod",
                    Product = "Tomatoes",
                    ProposedBy = "fred@farmer.com",
                    Quantity = 25,
                    Unit = "kg",
                    UnitPrice = 2.24,
                    Delivery = DateTime.Today,
                    ExtPrice = 56,
                    ProposedByOrganization = "Fred's Farm",
                    ProposedByPhone = "501-123-4567"
                    
                });
            context.PurchaseOffers.AddOrUpdate(
                x => x.Memo,
                new PurchaseOffer
                {
                    Memo = "Example 1",
                    CreatedOn = DateTime.Now,
                    PurchaseOrder = "1",
                    PaymentTerms = "net10",
                    Product = "Watermelon",
                    ProposedBy = "bob@buyer.com",
                    Quantity = 50,
                    Unit = "each",
                    UnitPrice = 3.11,
                    Delivery = DateTime.Today,
                    ExtPrice = 155.50,
                    ProposedByOrganization = "Bob's Market",
                    ProposedByPhone = "501-123-4567"
                },
                new PurchaseOffer
                {
                    Memo = "Example 2",
                    CreatedOn = DateTime.Now,
                    PurchaseOrder= "2",
                    PaymentTerms = "net10",
                    Product = "Purple Hull Peas",
                    Quantity = 45,
                    Unit = "quart",
                    UnitPrice = 2.95,
                    Delivery = DateTime.Today,
                    ProposedBy = "bob@buyer.com",
                    ExtPrice = 132.75,
                    ProposedByOrganization = "Bob's Market",
                    ProposedByPhone = "501-123-4567"
                }
                );
            context.ForkAndFarmCategories.AddOrUpdate(
                x => x.ListName,
                new ForkAndFarmCategory
                {
                    ListName = "Product",
                    ItemList = {new ForkAndFarmItem {ItemName = "Potatoes" },
                        new ForkAndFarmItem {ItemName = "Tomatoes" },
                        new ForkAndFarmItem {ItemName = "Watermelons" },
                        new ForkAndFarmItem {ItemName = "Okra" },
                        new ForkAndFarmItem {ItemName = "Yellow Squash" },
                        new ForkAndFarmItem {ItemName = "Blackberries" } }
                },
                new ForkAndFarmCategory
                {
                    ListName = "Unit",
                    ItemList = {new ForkAndFarmItem {ItemName = "lb" },
                    new ForkAndFarmItem {ItemName = "kg" },
                    new ForkAndFarmItem {ItemName = "each" },
                    new ForkAndFarmItem {ItemName = "gallon" },
                    new ForkAndFarmItem {ItemName = "pint" },
                    new ForkAndFarmItem {ItemName = "bushel" }
                    }
                },
                new ForkAndFarmCategory { ListName = "Payment Terms",
                    ItemList = { new ForkAndFarmItem { ItemName = "cash on delivery" },
                        new ForkAndFarmItem { ItemName = "net 10 days" } } }
                );
             
        }
    }
}
