using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Alliance_for_Life.Models
{
    public class QuarterlyState
    {
        [Key]
        public int QuraterlyStateId { get; set; }

        public decimal TotPSforQuarter { get; set; }
        public decimal TotDAforQuarter { get; set; }

        public decimal StateFee { get; set; }

        public decimal StateFeeQuarter { get; set; }

        public decimal TotDACandPSMonthly { get; set; }

        public decimal TotDAandPSQuarter { get; set; }

        //Navigation Properties

        public AdminCosts AdminCost { get; set; }
        public ParticipationService ParticipationService { get; set; }
        public SubContractor Subcontractor { get; set; }
        public Month Month { get; set; }

        public int MonthId { get; set; }
        public int SubcontractorId { get; set; }
        public int AdminCostId { get; set; }
        public int ParticipationServiceId { get; set; }


    }
}