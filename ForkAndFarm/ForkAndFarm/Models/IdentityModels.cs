using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ForkAndFarm.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string Organization { get; set; }
        public virtual List<PurchaseOffer> PurchaseOffers { get; set; }
        public virtual List<SupplyOffer> SupplyOffers { get; set; }
        public virtual List<Deal> DealsFromMe { get; set; }
        public virtual List<Deal> DealsToMe { get; set; }
        public virtual List<Deal> AcceptedDealsFromMe { get; set; }
        public virtual List<Deal> DealsIAccepted { get; set; }
        public Portal UserRole { get; set; }
        public enum Portal
        {
            Purchaser,
            Supplier
        }
        
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public System.Data.Entity.DbSet<ForkAndFarm.Models.PurchaseOffer> PurchaseOffers { get; set; }

        public System.Data.Entity.DbSet<ForkAndFarm.Models.SupplyOffer> SupplyOffers { get; set; }

        public System.Data.Entity.DbSet<ForkAndFarm.Models.Deal> Deals { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ForkAndFarm.Models.User> ForkAndFarmUsers { get; set; }
    }
}