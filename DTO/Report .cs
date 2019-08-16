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
        public string ATotCosts { get; set; }
        public string ASalandWages { get; set; }
        public string AEquipment { get; set; }
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
        public string AflBillable { get; set; }

        //Participation Cost

        public string PTranspotation { get; set; }
        public string PJobTrain { get; set; }
        public string PEducationAssistance { get; set; }
        public string PResidentialCare { get; set; }
        public string PUtilities { get; set; }
        public string PHousingEmergency { get; set; }
        public string PHousingAssistance { get; set; }
        public string PChildCare { get; set; }
        public string PClothing { get; set; }
        public string PFood { get; set; }
        public string PSupplies { get; set; }
        public string POther { get; set; }
        public string POther2 { get; set; }
        public string POther3 { get; set; }
        public string PTotals { get; set; }

    }
}