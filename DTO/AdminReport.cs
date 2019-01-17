namespace Alliance_for_Life.Models
{
    public class AdminReport
    {
        public int AdminCostId { get; set; }
        public string OrgName { get; set; }
        public string MonthName { get; set; }
        public decimal ATotCosts { get; set; }
        public decimal ASalandWages { get; set; }
        public decimal AEquipment { get; set; }
        public string RegionName { get; set; }
        public decimal AEmpBenefits { get; set; }
        public decimal AEmpTravel { get; set; }
        public decimal AEmpTraining { get; set; }
        public decimal AOfficeRent { get; set; }
        public decimal AOfficeUtilities { get; set; }
        public decimal AFacilityIns { get; set; }
        public decimal AOfficeSupplies { get; set; }
        public decimal AOfficeCommunications { get; set; }
        public decimal AOfficeMaint { get; set; }
        public decimal AConsulting { get; set; }
        public decimal AJanitorServices { get; set; }
        public decimal ADepreciation { get; set; }
        public decimal ATechSupport { get; set; }
        public decimal ASecurityServices { get; set; }
        public decimal AOther { get; set; }
        public decimal AOther2 { get; set; }
        public decimal AOther3 { get; set; }
    }
}