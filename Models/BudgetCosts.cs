using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class BudgetCosts
    {
        [Key]
         public System.Guid BudgetInvoiceId { get; set; }
        public double ASalandWages { get; set; }

        public double AEmpBenefits { get; set; }

        public double AEmpTravel { get; set; }

        public double AEmpTraining { get; set; }

        public double AOfficeRent { get; set; }

        public double AOfficeUtilities { get; set; }

        public double AFacilityIns { get; set; }

        public double AOfficeSupplies { get; set; }

        public double AEquipment { get; set; }

        public double AOfficeCommunications { get; set; }

        public double AOfficeMaint { get; set; }

        public double AConsulting { get; set; }

        public double SubConPayCost { get; set; }

        public double BackgrounCheck { get; set; }

        public double Other { get; set; }

        public double AJanitorServices { get; set; }

        public double ADepreciation { get; set; }

        public double ATechSupport { get; set; }

        public double ASecurityServices { get; set; }

        public double ATotCosts { get; set; }

        public double AdminFee { get; set; }
        
        public double Trasportation { get; set; }

        public double JobTraining { get; set; }

        public double TuitionAssistance { get; set; }

        public double ContractedResidential { get; set; }

        public double UtilityAssistance { get; set; }

        public double EmergencyShelter { get; set; }

        public double HousingAssistance { get; set; }

        public double Childcare { get; set; }

        public double Clothing { get; set; }

        public double Food { get; set; }

        public double Supplies { get; set; }

        public double RFO { get; set; }

        public double BTotal { get; set; }

        public double Maxtot { get; set; }

        public DateTime SubmittedDate { get; set; }

        // Navigation Properties
        public Months? Month { get; set; }
        public GeoRegion? Region { get; set; }
        public ApplicationUser User { get; set; }
        public AdminCosts AdminCost { get; set; }
        public ParticipationService ParticipationCost { get; set; }
        public int Year { get; set; }
    }
}