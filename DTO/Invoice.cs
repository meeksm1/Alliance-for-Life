using System;

namespace Alliance_for_Life.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public string OrgName { get; set; }

        public decimal DirectAdminCost { get; set; }

        public double ParticipantServices { get; set; }

        public double GrandTotal { get; set; }

        public double LessManagementFee { get; set; }

        public double DepositAmount { get; set; }

        public double BeginningAllocation { get; set; }

        public double AdjustedAllocation { get; set; }

        public DateTime BillingDate { get; set; }

        public double BalanceRemaining { get; set; }

    }
}