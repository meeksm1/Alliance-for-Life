using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public string OrgName { get; set; }

        [UIHint("Currency")]
        public decimal DirectAdminCost { get; set; }

        [UIHint("Currency")]
        public decimal ParticipantServices { get; set; }

        [UIHint("Currency")]
        public decimal GrandTotal { get; set; }


        public double LessManagementFee { get; set; }

        [UIHint("Currency")]
        public decimal DepositAmount { get; set; }

        [UIHint("Currency")]
        public decimal BeginningAllocation { get; set; }

        [UIHint("Currency")]
        public decimal AdjustedAllocation { get; set; }

        public DateTime BillingDate { get; set; }

        [UIHint("Currency")]
        public decimal BalanceRemaining { get; set; }

    }
}