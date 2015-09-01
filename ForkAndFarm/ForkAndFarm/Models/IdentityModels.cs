using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForkAndFarm.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public Trader profile { get; set; }
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<PurchaseOffer> PurchaseOffers { get; set; }
        public DbSet<SupplyOffer> SupplyOffers { get; set; }
        public DbSet<Deal> Deals { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ForkAndFarm.Models.Trader> Traders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trader>()
                .HasMany<PurchaseOffer>(p => p.MyPurchaseOffers);
            
            modelBuilder.Entity<Trader>()
                .HasMany<SupplyOffer>(s => s.MySupplyOffers);

            modelBuilder.Entity<Trader>()
                .HasMany<Deal>(d => d.ProposedByMeAndOpen);

            modelBuilder.Entity<Trader>()
                .HasMany<Deal>(d => d.ProposedByMeAndComplete)
                .WithRequired(d => d.AcceptedBy)
                .HasForeignKey(d => d.Deal_Id);

            modelBuilder.Entity<Trader>()
                .HasMany<Deal>(d => d.AcceptedByMe)
                .WithRequired(d => d.ProposedBy)
                .HasForeignKey(d => d.Deal_Id);
                
        }
    }
   
}