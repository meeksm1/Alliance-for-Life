using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class Invoice
    {
        [Key]
        public System.Guid InvoiceId { get; set; }

        public string OrgName { get; set; }

        public double DirectAdminCost { get; set; }

        public double ParticipantServices { get; set; }

        public double GrandTotal { get; set; }

        public double LessManagementFee { get; set; }

        public double DepositAmount { get; set; }

        //public double BeginningAllocation { get; set; }

        //public double AdjustedAllocation { get; set; }

        public DateTime BillingDate { get; set; }

        public double BalanceRemaining { get; set; }

        public DateTime SubmittedDate { get; set; }
        public Guid AdminCostId { get; set; }
        public Guid PSId { get; set; }
        public Guid SubcontractorId { get; set; }
        public Guid AllocatedBudgetId { get; set; }
        public int Year { get; set; }

        /*Navigation Properties*/
        public GeoRegion? Region { get; set; }
        public Months? Month { get; set; }
        public SubContractor Subcontractor { get; set; }
        public AdminCosts AdminCosts { get; set; }
        public ParticipationService ParticipationService { get; set; }
        public AllocatedBudget AllocatedBudget { get; set; }


    }
}