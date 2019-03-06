using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class AdminCosts
    {
        [Key]
        public System.Guid AdminCostId { get; set; }
        public double ASalandWages { get; set; }

        public double ABackgroundCheck { get; set; }

        public double EFTFees { get; set; }
        
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

        public double AJanitorServices { get; set; }

        public double ADepreciation { get; set; }

        public double ATechSupport { get; set; }

        public double ASecurityServices { get; set; }

        public double AOther { get; set; }

        public double AOther2 { get; set; }

        public double AOther3 { get; set; }

        public double StateAdminFee { get; set; }
        
        public double ATotCosts { get; set; }

        public DateTime SubmittedDate { get; set; }


        //Navigation Properties
        public Months? Month { get; set; }
        public SubContractor Subcontractor { get; set; }

        public System.Guid SubcontractorId { get; set; }
        public int Year { get; set; }




    }
}