using System;
namespace Alliance_for_Life.Models

{
    public class AdminReport
    {
        public System.Guid AdminCostId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public int YearName { get; set; }
        public string OrgName { get; set; }
        public string MonthName { get; set; }
        public string ATotCosts { get; set; }
        public string ASalandWages { get; set; }
        public string AEquipment { get; set; }
        public string RegionName { get; set; }
        public string AEmpBenefits { get; set; }
        public string AEmpTravel { get; set; }
        public string AEmpTraining { get; set; }
        public string AOfficeRent { get; set; }
        public string AOfficeUtilities { get; set; }
        public string AFacilityIns { get; set; }
        public string AOfficeSupplies { get; set; }
        public string AOfficeCommunications { get; set; }
        public string AOfficeMaint { get; set; }
        public string AConsulting { get; set; }
        public string AJanitorServices { get; set; }
        public string ADepreciation { get; set; }
        public string ATechSupport { get; set; }
        public string ASecurityServices { get; set; }
        public string AOther { get; set; }
        public string AOther2 { get; set; }
        public string AOther3 { get; set; }
        public string AOtherInput { get; set; }

        public string AOtherInput2 { get; set; }

        public string AOtherInput3 { get; set; }

        public string AflBillable { get; set; }

    }
}