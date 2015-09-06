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
            AutomaticMigrationsEnabled = false;
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
                var userToInsert = new ForkAndFarmUser { UserName = "bob@buyer.com", Organization = "Bob's Arkansas Grown Groceries", UserRole = ForkAndFarmUser.Portal.Purchaser };
                userManager.Create(userToInsert, "Abc123!@#");
            }
            if (!(context.Users.Any(u => u.UserName == "fred@farmer.com")))
            {
                var userToInsert = new ForkAndFarmUser { UserName = "fred@farmer.com", Organization = "Fred's Natural and Organic Farm", UserRole = ForkAndFarmUser.Portal.Supplier };
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
                    ExtPrice = 125
                    
                    
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
                    ExtPrice = 56
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
                    ExtPrice = 155.50
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
                    ExtPrice = 132.75

                }
                );
             
        }
    }
}
