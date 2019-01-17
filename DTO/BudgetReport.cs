namespace Alliance_for_Life.Models
{
    public class BudgetReport
    {
        public int BudgetInvoiceId { get; set; }
        public string MonthName { get; set; }
        public decimal ATotCosts { get; set; }
        public decimal BTotal { get; set; }
        public decimal Maxtot { get; set; }
        public string RegionName { get; set; }
        public string OrgName { get; set; }
        public decimal ASalandWages { get; set; }
        public decimal AEmpBenefits { get; set; }
        public decimal AEmpTravel { get; set; }
        public decimal AEmpTraining { get; set; }
        public decimal AOfficeRent { get; set; }
        public decimal AOfficeUtilities { get; set; }
        public decimal AFacilityIns { get; set; }
        public decimal AOfficeSupplies { get; set; }
        public decimal AEquipment { get; set; }
        public decimal AOfficeCommunications { get; set; }
        public decimal AOfficeMaint { get; set; }
        public decimal AConsulting { get; set; }
        public decimal SubConPayCost { get; set; }
        public decimal BackgrounCheck { get; set; }
        public decimal Other { get; set; }
        public decimal AJanitorServices { get; set; }
        public decimal ADepreciation { get; set; }
        public decimal ATechSupport { get; set; }
        public decimal ASecurityServices { get; set; }
        public decimal AdminFee { get; set; }
        public decimal Trasportation { get; set; }
        public decimal JobTraining { get; set; }
        public decimal TuitionAssistance { get; set; }
        public decimal ContractedResidential { get; set; }
        public decimal UtilityAssistance { get; set; }
        public decimal EmergencyShelter { get; set; }
        public decimal HousingAssistance { get; set; }
        public decimal Childcare { get; set; }
        public decimal Clothing { get; set; }
        public decimal Food { get; set; }
        public decimal Supplies { get; set; }
        public decimal RFO { get; set; }
   }
}