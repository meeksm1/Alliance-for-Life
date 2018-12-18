namespace Alliance_for_Life.Models
{
    public class AdminReport
    {
        public int AdminCostId { get; set; }
        public string OrgName { get; set; }
        public string MonthName { get; set; }
        public double ATotCosts { get; set; }
        public double ASalandWages { get; set; }
        public double AEquipment { get; set; }
        public string RegionName { get; set; }
        public int AEmpBenefits { get; set; }
        public int AEmpTravel { get; set; }
        public int AEmpTraining { get; set; }
        public int AOfficeRent { get; set; }
        public int AOfficeUtilities { get; set; }
        public int AFacilityIns { get; set; }
        public int AOfficeSupplies { get; set; }
        public int AOfficeCommunications { get; set; }
        public int AOfficeMaint { get; set; }
        public int AConsulting { get; set; }
        public int AJanitorServices { get; set; }
        public int ADepreciation { get; set; }
        public int ATechSupport { get; set; }
        public int ASecurityServices { get; set; }
        public int AOther { get; set; }
        public int AOther2 { get; set; }
        public int AOther3 { get; set; }
    }
}