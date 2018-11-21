namespace Alliance_for_Life.Models
{
    public class AdminReport
    {
        public int AdminCostId { get; set; }
        public string MonthName { get; set; }
        public double ATotCosts { get; set; }
        public double ASalandWages { get; set; }
        public double AEquipment { get; set; }
        public string RegionName { get; set; }
    }
}