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

            context.Advertisements.AddOrUpdate(
                x => x.Memo,
               
                new Advertisement
                {
                    
                    Memo = "Example 1",
                    CreatedOn = new DateTime(2015,12,1,0,1,0),
                    Invoice = "1",
                    PaymentTerms = "cod",
                    Product = "Potatoes",
                    ProposedBy = "fred@farmer.com",
                    Quantity = 100,
                    Unit = "lb",
                    UnitPrice = 1.25,
                    Delivery = DateTime.Today.Add(new TimeSpan(14,0,0,0)),
                    ExtPrice = 125,
                    ProposedByOrganization = "Fred's Farm",
                    ProposedByPhone = "501-123-4567",
                    AdType = AdType.SupplyOffer
                   

                },

                new Advertisement
                {
                    Memo = "Example 2",
                    CreatedOn = new DateTime(2015, 12, 1, 0, 2, 0),
                    Invoice = "2",
                    PaymentTerms = "cod",
                    Product = "Tomatoes",
                    ProposedBy = "fred@farmer.com",
                    Quantity = 25,
                    Unit = "kg",
                    UnitPrice = 2.24,
                    Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                    ExtPrice = 56,
                    ProposedByOrganization = "Fred's Farm",
                    ProposedByPhone = "501-123-4567",
                    AdType = AdType.SupplyOffer
                    
                },
                 new Advertisement
                 {
                     Memo = "Example 3",
                     CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                     Invoice = "1",
                     PaymentTerms = "net10",
                     Product = "Watermelon",
                     ProposedBy = "bob@buyer.com",
                     Quantity = 50,
                     Unit = "each",
                     UnitPrice = 3.11,
                     Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                     ExtPrice = 155.50,
                     ProposedByOrganization = "Bob's Market",
                     ProposedByPhone = "501-123-4567",
                     AdType = AdType.PurchaseOffer
                 },
                new Advertisement
                {
                    Memo = "Example 4",
                    CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                    Invoice = "2",
                    PaymentTerms = "net10",
                    Product = "Purple Hull Peas",
                    Quantity = 45,
                    Unit = "quart",
                    UnitPrice = 2.95,
                    Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                    ProposedBy = "bob@buyer.com",
                    ExtPrice = 132.75,
                    ProposedByOrganization = "Bob's Market",
                    ProposedByPhone = "501-123-4567",
                    AdType = AdType.PurchaseOffer
                },
                new Advertisement
                {
                    Memo = "Example 5",
                    CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                    Invoice = "2",
                    PaymentTerms = "cod",
                    Product = "Apples",
                    ProposedBy = "fred@farmer.com",
                    Quantity = 25,
                    Unit = "kg",
                    UnitPrice = 2.24,
                    Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                    ExtPrice = 56,
                    ProposedByOrganization = "Fred's Farm",
                    ProposedByPhone = "501-123-4567",
                    AdType = AdType.SupplyOffer

                },
                 new Advertisement
                 {
                     Memo = "Example 6",
                     CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                     Invoice = "1",
                     PaymentTerms = "net10",
                     Product = "Bananas",
                     ProposedBy = "bob@buyer.com",
                     Quantity = 50,
                     Unit = "each",
                     UnitPrice = 3.11,
                     Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                     ExtPrice = 155.50,
                     ProposedByOrganization = "Bob's Market",
                     ProposedByPhone = "501-123-4567",
                     AdType = AdType.PurchaseOffer
                 },
                  new Advertisement
                  {
                      Memo = "Example 7",
                      CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                      Invoice = "2",
                      PaymentTerms = "net10",
                      Product = "Cucumber",
                      Quantity = 45,
                      Unit = "quart",
                      UnitPrice = 2.95,
                      Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                      ProposedBy = "bob@buyer.com",
                      ExtPrice = 132.75,
                      ProposedByOrganization = "Bob's Market",
                      ProposedByPhone = "501-123-4567",
                      AdType = AdType.PurchaseOffer
                  },
                  new Advertisement
                  {
                      Memo = "Example 8",
                      CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                      Invoice = "2",
                      PaymentTerms = "cod",
                      Product = "Dewberries",
                      ProposedBy = "fred@farmer.com",
                      Quantity = 25,
                      Unit = "kg",
                      UnitPrice = 2.24,
                      Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                      ExtPrice = 56,
                      ProposedByOrganization = "Fred's Farm",
                      ProposedByPhone = "501-123-4567",
                      AdType = AdType.SupplyOffer

                  },
                   new Advertisement
                   {
                       Memo = "Example 9",
                       CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                       Invoice = "1",
                       PaymentTerms = "net10",
                       Product = "Elderberries",
                       ProposedBy = "bob@buyer.com",
                       Quantity = 50,
                       Unit = "each",
                       UnitPrice = 3.11,
                       Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                       ExtPrice = 155.50,
                       ProposedByOrganization = "Bob's Market",
                       ProposedByPhone = "501-123-4567",
                       AdType = AdType.PurchaseOffer
                   },
                    new Advertisement
                    {
                        Memo = "Example 10",
                        CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                        Invoice = "2",
                        PaymentTerms = "net10",
                        Product = "Flowers",
                        Quantity = 45,
                        Unit = "quart",
                        UnitPrice = 2.95,
                        Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                        ProposedBy = "bob@buyer.com",
                        ExtPrice = 132.75,
                        ProposedByOrganization = "Bob's Market",
                        ProposedByPhone = "501-123-4567",
                        AdType = AdType.PurchaseOffer
                    },
                    new Advertisement
                    {
                        Memo = "Example 11",
                        CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                        Invoice = "2",
                        PaymentTerms = "cod",
                        Product = "Gooseberries",
                        ProposedBy = "fred@farmer.com",
                        Quantity = 25,
                        Unit = "kg",
                        UnitPrice = 2.24,
                        Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                        ExtPrice = 56,
                        ProposedByOrganization = "Fred's Farm",
                        ProposedByPhone = "501-123-4567",
                        AdType = AdType.SupplyOffer

                    },
                     new Advertisement
                     {
                         Memo = "Example 12",
                         CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                         Invoice = "1",
                         PaymentTerms = "net10",
                         Product = "Hot Peppers",
                         ProposedBy = "bob@buyer.com",
                         Quantity = 50,
                         Unit = "each",
                         UnitPrice = 3.11,
                         Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                         ExtPrice = 155.50,
                         ProposedByOrganization = "Bob's Market",
                         ProposedByPhone = "501-123-4567",
                         AdType = AdType.PurchaseOffer
                     },
                      new Advertisement
                      {
                          Memo = "Example 13",
                          CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                          Invoice = "2",
                          PaymentTerms = "net10",
                          Product = "Indian Corn",
                          Quantity = 45,
                          Unit = "quart",
                          UnitPrice = 2.95,
                          Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                          ExtPrice = 132.75,
                          ProposedByOrganization = "Bob's Market",
                          ProposedByPhone = "501-123-4567",
                          AdType = AdType.PurchaseOffer
                      },
                      new Advertisement
                      {
                          Memo = "Example 14",
                          CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                          Invoice = "2",
                          PaymentTerms = "cod",
                          Product = "Juniper Berries",
                          ProposedBy = "fred@farmer.com",
                          Quantity = 25,
                          Unit = "kg",
                          UnitPrice = 2.24,
                          Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                          ExtPrice = 56,
                          ProposedByOrganization = "Fred's Farm",
                          ProposedByPhone = "501-123-4567",
                          AdType = AdType.SupplyOffer

                      },
                       new Advertisement
                       {
                           Memo = "Example 15",
                           CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                           Invoice = "1",
                           PaymentTerms = "net10",
                           Product = "Kiwi Fruit",
                           ProposedBy = "bob@buyer.com",
                           Quantity = 50,
                           Unit = "each",
                           UnitPrice = 3.11,
                           Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                           ExtPrice = 155.50,
                           ProposedByOrganization = "Bob's Market",
                           ProposedByPhone = "501-123-4567",
                           AdType = AdType.PurchaseOffer
                       },
                         new Advertisement
                         {
                             Memo = "Example 16",
                             CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                             Invoice = "2",
                             PaymentTerms = "net10",
                             Product = "Lemons",
                             Quantity = 45,
                             Unit = "quart",
                             UnitPrice = 2.95,
                             Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                             ProposedBy = "bob@buyer.com",
                             ExtPrice = 132.75,
                             ProposedByOrganization = "Bob's Market",
                             ProposedByPhone = "501-123-4567",
                             AdType = AdType.PurchaseOffer
                         },
                         new Advertisement
                         {
                             Memo = "Example 17",
                             CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                             Invoice = "2",
                             PaymentTerms = "cod",
                             Product = "Mangoes",
                             ProposedBy = "fred@farmer.com",
                             Quantity = 25,
                             Unit = "kg",
                             UnitPrice = 2.24,
                             Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                             ExtPrice = 56,
                             ProposedByOrganization = "Fred's Farm",
                             ProposedByPhone = "501-123-4567",
                             AdType = AdType.SupplyOffer

                         },
                          new Advertisement
                          {
                              Memo = "Example 18",
                              CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                              Invoice = "1",
                              PaymentTerms = "net10",
                              Product = "Nasturtium",
                              ProposedBy = "bob@buyer.com",
                              Quantity = 50,
                              Unit = "each",
                              UnitPrice = 3.11,
                              Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                              ExtPrice = 155.50,
                              ProposedByOrganization = "Bob's Market",
                              ProposedByPhone = "501-123-4567",
                              AdType = AdType.PurchaseOffer
                          },
                           new Advertisement
                           {
                               Memo = "Example 19",
                               CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                               Invoice = "2",
                               PaymentTerms = "net10",
                               Product = "Okra",
                               Quantity = 45,
                               Unit = "quart",
                               UnitPrice = 2.95,
                               Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                               ProposedBy = "bob@buyer.com",
                               ExtPrice = 132.75,
                               ProposedByOrganization = "Bob's Market",
                               ProposedByPhone = "501-123-4567",
                               AdType = AdType.PurchaseOffer
                           },
                           new Advertisement
                           {
                               Memo = "Example 20",
                               CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                               Invoice = "2",
                               PaymentTerms = "cod",
                               Product = "Peas",
                               ProposedBy = "fred@farmer.com",
                               Quantity = 25,
                               Unit = "kg",
                               UnitPrice = 2.24,
                               Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                               ExtPrice = 56,
                               ProposedByOrganization = "Fred's Farm",
                               ProposedByPhone = "501-123-4567",
                               AdType = AdType.SupplyOffer

                           },
                            new Advertisement
                            {
                                Memo = "Example 21",
                                CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                                Invoice = "1",
                                PaymentTerms = "net10",
                                Product = "Quince",
                                ProposedBy = "bob@buyer.com",
                                Quantity = 50,
                                Unit = "each",
                                UnitPrice = 3.11,
                                Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                                ExtPrice = 155.50,
                                ProposedByOrganization = "Bob's Market",
                                ProposedByPhone = "501-123-4567",
                                AdType = AdType.PurchaseOffer
                            },
                             new Advertisement
                             {
                                 Memo = "Example 22",
                                 CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                                 Invoice = "2",
                                 PaymentTerms = "net10",
                                 Product = "Rhubarb",
                                 Quantity = 45,
                                 Unit = "quart",
                                 UnitPrice = 2.95,
                                 Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                                 ProposedBy = "bob@buyer.com",
                                 ExtPrice = 132.75,
                                 ProposedByOrganization = "Bob's Market",
                                 ProposedByPhone = "501-123-4567",
                                 AdType = AdType.PurchaseOffer
                             },
                             new Advertisement
                             {
                                 Memo = "Example 23",
                                 CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                                 Invoice = "2",
                                 PaymentTerms = "cod",
                                 Product = "Snap Beans",
                                 ProposedBy = "fred@farmer.com",
                                 Quantity = 25,
                                 Unit = "kg",
                                 UnitPrice = 2.24,
                                 Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                                 ExtPrice = 56,
                                 ProposedByOrganization = "Fred's Farm",
                                 ProposedByPhone = "501-123-4567",
                                 AdType = AdType.SupplyOffer

                             },
                              new Advertisement
                              {
                                  Memo = "Example 24",
                                  CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                                  Invoice = "1",
                                  PaymentTerms = "net10",
                                  Product = "Tomatoes",
                                  ProposedBy = "bob@buyer.com",
                                  Quantity = 50,
                                  Unit = "each",
                                  UnitPrice = 3.11,
                                  Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                                  ExtPrice = 155.50,
                                  ProposedByOrganization = "Bob's Market",
                                  ProposedByPhone = "501-123-4567",
                                  AdType = AdType.PurchaseOffer
                              },
                               new Advertisement
                               {
                                   Memo = "Example 25",
                                   CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                                   Invoice = "2",
                                   PaymentTerms = "net10",
                                   Product = "Uglifruit",
                                   Quantity = 45,
                                   Unit = "quart",
                                   UnitPrice = 2.95,
                                   Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                                   ProposedBy = "bob@buyer.com",
                                   ExtPrice = 132.75,
                                   ProposedByOrganization = "Bob's Market",
                                   ProposedByPhone = "501-123-4567",
                                   AdType = AdType.PurchaseOffer
                               },
                               new Advertisement
                               {
                                   Memo = "Example 26",
                                   CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0),
                                   Invoice = "2",
                                   PaymentTerms = "cod",
                                   Product = "Velvet",
                                   ProposedBy = "fred@farmer.com",
                                   Quantity = 25,
                                   Unit = "kg",
                                   UnitPrice = 2.24,
                                   Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                                   ExtPrice = 56,
                                   ProposedByOrganization = "Fred's Farm",
                                   ProposedByPhone = "501-123-4567",
                                   AdType = AdType.SupplyOffer

                               },
                                new Advertisement
                                {
                                    Memo = "Example 27",
                                    CreatedOn = new DateTime(2015, 12, 1, 0, 1, 0), 
                                    Invoice = "1",
                                    PaymentTerms = "net10",
                                    Product = "Watermelon",
                                    ProposedBy = "bob@buyer.com",
                                    Quantity = 50,
                                    Unit = "each",
                                    UnitPrice = 3.11,
                                    Delivery = DateTime.Today.Add(new TimeSpan(14, 0, 0, 0)),
                                    ExtPrice = 155.50,
                                    ProposedByOrganization = "Bob's Market",
                                    ProposedByPhone = "501-123-4567",
                                    AdType = AdType.PurchaseOffer
                                }
                );
           
           
             
        }
    }
}
