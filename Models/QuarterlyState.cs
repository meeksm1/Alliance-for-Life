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

        public double TotPSforQuarter { get; set; }

        public double TotDAforQuarter { get; set; }

        public double StateFee { get; set; }

        public double StateFeeQuarter { get; set; }

        public double TotDACandPSMonthly { get; set; }

        public double TotDAandPSQuarter { get; set; }

        //Navigation Properties

        public AdminCosts AdminCost { get; set; }
        public ParticipationService ParticipationCost { get; set; }
        public SubContractor Subcontractor { get; set; }
        public Months? Month { get; set; }

    
        public int SubcontractorId { get; set; }
        public int AdminCostId { get; set; }
        public int ParticipationCostId { get; set; }


    }
}