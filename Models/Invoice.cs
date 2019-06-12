using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class Invoice
    {
        [Key]
        public System.Guid InvoiceId { get; set; }

        public string OrgName { get; set; }

        [UIHint("Currency")]
        public double DirectAdminCost { get; set; }

        [UIHint("Currency")]
        public double ParticipantServices { get; set; }

        [UIHint("Currency")]
        public double GrandTotal { get; set; }

        public double LessManagementFee { get; set; }

        [UIHint("Currency")]
        public double DepositAmount { get; set; }

        //[UIHint("Currency")]
        //public double BeginningAllocation { get; set; }

        //[UIHint("Currency")]
        //public double AdjustedAllocation { get; set; }

        public DateTime BillingDate { get; set; }

        [UIHint("Currency")]
        public double BalanceRemaining { get; set; }

        public DateTime SubmittedDate { get; set; }
        public System.Guid AdminCostId { get; set; }
        public System.Guid PSId { get; set; }
        
        /*Navigation Properties*/
        public GeoRegion? Region { get; set; }
        public Months? Month { get; set; }
        public SubContractor Subcontractor { get; set; }
        public AdminCosts AdminCosts { get; set; }
        public ParticipationService ParticipationService { get; set; }

        public int Year { get; set; }
        public System.Guid SubcontractorId { get; set; }

        public System.Guid AllocatedBudgetId { get; set; }
        public AllocatedBudget AllocatedBudget { get; set; }


    }
}