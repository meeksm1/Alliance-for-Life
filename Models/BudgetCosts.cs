using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class BudgetCosts
    {
        [Key]
        public int BudgetInvoiceId { get; set; }

        public int ASalandWages { get; set; }

        public int AEmpBenefits { get; set; }

        public int AEmpTravel { get; set; }

        public int AEmpTraining { get; set; }

        public int AOfficeRent { get; set; }

        public int AOfficeUtilities { get; set; }

        public int AFacilityIns { get; set; }

        public int AOfficeSupplies { get; set; }

        public int AEquipment { get; set; }

        public int AOfficeCommunications { get; set; }

        public int AOfficeMaint { get; set; }

        public int AConsulting { get; set; }

        public int SubConPayCost { get; set; }

        public int BackgrounCheck { get; set; }

        public int Other { get; set; }

        public int AJanitorServices { get; set; }

        public int ADepreciation { get; set; }

        public int ATechSupport { get; set; }

        public int ASecurityServices { get; set; }

        public double ATotCosts { get; set; }

        public double AdminFee { get; set; }
        
        public int Trasportation { get; set; }

        public int JobTraining { get; set; }

        public int TuitionAssistance { get; set; }

        public int ContractedResidential { get; set; }

        public int UtilityAssistance { get; set; }

        public int EmergencyShelter { get; set; }

        public int HousingAssistance { get; set; }

        public int Childcare { get; set; }

        public int Clothing { get; set; }

        public int Food { get; set; }

        public int Supplies { get; set; }

        public int RFO { get; set; }

        public double BTotal { get; set; }

        public double Maxtot { get; set; }

        // Navigation Properties
        public Month Month { get; set; }
        public Region Region { get; set; }
        //public SubContractor Subcontractor { get; set; }
        public ApplicationUser User { get; set; }
        public AdminCosts AdminCost { get; set; }
        public ParticipationService ParticipationCost { get; set; }
        public Year Year { get; set; }

        public int RegionId { get; set; }
        public int MonthId { get; set; }
        //public int SubcontractorId { get; set; }
        public int YearId { get; set; }
    }
}