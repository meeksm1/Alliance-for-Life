using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Alliance_for_Life.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SubContractor> SubContractors { get; set; }
        public DbSet<ClientList> User { get; set; }
        public DbSet<ResidentialMIR> ResidentialMIRs { get; set; }
        public DbSet<NonResidentialMIR> NonResidentialMIRs { get; set; }
        public DbSet<AdminCosts> AdminCosts { get; set; }
        public DbSet<ParticipationService> ParticipationServices { get; set; }
        public DbSet<BudgetCosts> BudgetCosts { get; set; }
        public DbSet<Surveys> Surveys { get; set; }
       public DbSet<Invoice> Invoices { get; set; }
        public DbSet<QuarterlyState> QuarterlyStates { get; set; }

        public ApplicationDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }   
}