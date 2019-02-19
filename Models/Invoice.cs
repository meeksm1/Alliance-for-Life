using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public string OrgName { get; set; }

        public decimal DirectAdminCost { get; set; }

        public decimal ParticipantServices { get; set; }

        public decimal GrandTotal { get; set; }

        public double LessManagementFee { get; set; }

        public decimal DepositAmount { get; set; }

        public decimal BeginningAllocation { get; set; }

        public decimal AdjustedAllocation { get; set; }

        public DateTime BillingDate { get; set; }

        public decimal BalanceRemaining { get; set; }

        public DateTime SubmittedDate { get; set; }


        /*Navigation Properties*/
        public Region Region { get; set; }
        public Month Month { get; set; }
        public SubContractor Subcontractor { get; set; }
        public Year Year { get; set; }
        public AdminCosts AdminCosts { get; set; }
        public ParticipationService ParticipationService { get; set; }

        public int YearId { get; set; }
        public int RegionId { get; set; }
        public int MonthId { get; set; }
        public int SubcontractorId { get; set; }
        public int AdminCostId { get; set; }
        public int PartServId { get; set; }
    }
}