using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.ViewModels
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }

        [Display(Name = "Organization")]
        public string OrgName { get; set; }

        public string Region { get; set; }

        public string Month { get; set; }

        public int Year { get; set; }
        
        [UIHint("Currency")]
        [Display(Name = "Direct Admin Cost")]
        public decimal DirectAdminCost { get; set; }

        [UIHint("Currency")]
        [Display(Name = "Paricipant Services Totals")]
        public decimal ParticipantServices { get; set; }

        [UIHint("Currency")]
        [Display(Name = "")]
        public decimal GrandTotal { get; set; }

        [Display(Name = "")]
        public double LessManagementFee { get; set; }

        [UIHint("Currency")]
        [Display(Name = "")]
        public decimal DepositAmount { get; set; }

        [UIHint("Currency")]
        [Display(Name = "")]
        public decimal BeginningAllocation { get; set; }

        [UIHint("Currency")]
        [Display(Name = "")]
        public decimal AdjustedAllocation { get; set; }

        [Display(Name = "")]
        public DateTime BillingDate { get; set; }

        [UIHint("Currency")]
        [Display(Name = "")]
        public decimal BalanceRemaining { get; set; }
    }
}