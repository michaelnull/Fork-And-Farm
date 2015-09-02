namespace ForkAndFarm.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;

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
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            if (!(context.Users.Any(u => u.UserName == "bob@buyer.com")))
            {
                var userToInsert = new ApplicationUser { UserName = "bob@buyer.com" };
                userManager.Create(userToInsert, "Abc123!@#");
            }
            if (!(context.Users.Any(u => u.UserName == "fred@farmer.com")))
            {
                var userToInsert = new ApplicationUser { UserName = "fred@farmer.com" };
                userManager.Create(userToInsert, "Abc123!@#");
            }
        }
    }
}
