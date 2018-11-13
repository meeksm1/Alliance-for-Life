namespace Alliance_for_Life.Models
{
    public class BudgetReport
    {
        public int BudgetInvoiceId { get; set; }
        public string MonthName { get; set; }
        public double ATotCosts { get; set; }
        public double BTotal { get; set; }
        public double Maxtot { get; set; }
        public string RegionName { get; set; }
    }
}