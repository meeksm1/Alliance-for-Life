namespace Alliance_for_Life.Models
{
    public class AExpenseVsBExpense
    {
        public string Director { get; set; }
        public decimal TotPSforQuarter { get; set; }
        public decimal TotPS { get; set; }
        public decimal TotDACandPSMonthly { get; set; }
        public decimal TotDAandPSQuarter { get; set; }
        public AdminCosts AdminCost { get; set; }
        public ParticipationService ParticipationCost { get; set; }
        public SubContractor Subcontractor { get; set; }
        public Month Month { get; set; }

    }
}