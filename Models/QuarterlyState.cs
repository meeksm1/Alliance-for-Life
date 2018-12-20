using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class QuarterlyState
    {
        [Key]
        public int QuraterlyStateId { get; set; }

        public decimal TotPSforQuarter { get; set; }

        public decimal TotDAforQuarter { get; set; }

        public double StateFee { get {
                var statefee = AdminCost.ATotCosts * .1; return statefee; } }
        public decimal StateFeeQuarter { get; set; }

        public double TotDACandPSMonthly { get {
                var totmonthly = AdminCost.ATotCosts = ParticipationCost.PTotals;
                return totmonthly; } }

        public decimal TotDAandPSQuarter { get; set; }

        //Navigation Properties

        public AdminCosts AdminCost { get; set; }
        public ParticipationService ParticipationCost { get; set; }
        public SubContractor Subcontractor { get; set; }
        public Month Month { get; set; }

    }
}