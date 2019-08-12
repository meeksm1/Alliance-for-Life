namespace Alliance_for_Life.Models

{
    public class Report
    {
        //general Info
        public int Year { get; set; }
        public string OrgName { get; set; }
        public string Month { get; set; }
        public string Region { get; set; }
        public int EIN { get; set; }
        //AdminCost
        public double ATotCosts { get; set; }
        public double ASalandWages { get; set; }
        public double AEquipment { get; set; }
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
        public double AflBillable { get; set; }

        //Participation Cost

        public double PTranspotation { get; set; }
        public double PJobTrain { get; set; }
        public double PEducationAssistance { get; set; }
        public double PResidentialCare { get; set; }
        public double PUtilities { get; set; }
        public double PHousingEmergency { get; set; }
        public double PHousingAssistance { get; set; }
        public double PChildCare { get; set; }
        public double PClothing { get; set; }
        public double PFood { get; set; }
        public double PSupplies { get; set; }
        public double POther { get; set; }
        public double POther2 { get; set; }
        public double POther3 { get; set; }
        public double PTotals { get; set; }

    }
}