using System;
namespace Alliance_for_Life.Models
    
{
    public class AdminReport
    {
        public int AdminCostId { get; set; }
        public DateTime SubmittedDate{ get; set; }
        public int YearName { get; set; }
        public string OrgName { get; set; }
        public string MonthName { get; set; }
        public double ATotCosts { get; set; }
        public double ASalandWages { get; set; }
        public double AEquipment { get; set; }
        public string RegionName { get; set; }
        public double AEmpBenefits { get; set; }
        public double AEmpTravel { get; set; }
        public double AEmpTraining { get; set; }
        public double AOfficeRent { get; set; }
        public double AOfficeUtilities { get; set; }
        public double AFacilityIns { get; set; }
        public double AOfficeSupplies { get; set; }
        public double AOfficeCommunications { get; set; }
        public double AOfficeMaint { get; set; }
        public double AConsulting { get; set; }
        public double AJanitorServices { get; set; }
        public double ADepreciation { get; set; }
        public double ATechSupport { get; set; }
        public double ASecurityServices { get; set; }
        public double AOther { get; set; }
        public double AOther2 { get; set; }
        public double AOther3 { get; set; }
    }
}