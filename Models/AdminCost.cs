using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class AdminCosts
    {
        [Key]
        public int AdminCostId { get; set; }

        public decimal ASalandWages { get; set; }

        public decimal ABackgroundCheck { get; set; }

        public decimal EFTFees { get; set; }
        
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

        public decimal AJanitorServices { get; set; }

        public decimal ADepreciation { get; set; }

        public decimal ATechSupport { get; set; }

        public decimal ASecurityServices { get; set; }

        public decimal AOther { get; set; }

        public decimal AOther2 { get; set; }

        public decimal AOther3 { get; set; }

        public decimal StateAdminFee { get; set; }
        
        public decimal ATotCosts { get; set; }


        //Navigation Properties
        public Region Region { get; set; }

        public Month Month { get; set; }

        public SubContractor Subcontractor { get; set; }

        public int RegionId { get; set; }
        public int MonthId { get; set; }
        public int SubcontractorId { get; set; }



    }
}