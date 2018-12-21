using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class QuarterlyState
    {
        [Key]
        public int QuraterlyStateId { get; set; }

        public double TotPSforQuarter { get; set; }

        public double TotDAforQuarter { get; set; }

        public double StateFee { get {
                var statefee = AdminCost.ATotCosts * .1; return statefee; } }
        public double StateFeeQuarter { get; set; }

        public double TotDACandPSMonthly { get; set; }

        public double TotDAandPSQuarter { get; set; }

        //Navigation Properties

        public AdminCosts AdminCost { get; set; }
        public ParticipationService ParticipationCost { get; set; }
        public SubContractor Subcontractor { get; set; }
        public Month Month { get; set; }

        public int MonthId { get; set; }
        public int SubcontractorId { get; set; }
        public int AdminCostId { get; set; }
        public int ParticipationCostId { get; set; }


    }
}