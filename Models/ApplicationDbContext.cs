using Alliance_for_Life.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Alliance_for_Life.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SubContractor> SubContractors { get; set; }
        public DbSet<ClientList> ClientLists { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<ResidentialMIR> ResidentialMIRs { get; set; }
        public DbSet<NonResidentialMIR> NonResidentialMIRs { get; set; }
        public DbSet<AdminCosts> AdminCosts { get; set; }
        public DbSet<ParticipationService> ParticipationServices { get; set; }
        public DbSet<BudgetCosts> BudgetCosts  { get; set; }
        public DbSet<Asset> Assets { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ClientList>()
        //        .HasRequired(c => c.Subcontractor)
        //        .WithMany()
        //        .WillCascadeOnDelete(false);

        //    base.OnModelCreating(modelBuilder);
        //}
    }

   
}