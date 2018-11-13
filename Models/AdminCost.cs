using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class AdminCosts
    {
        public int Id { get; set; }

        [Required]
        [Key]
        public int MonthId { get; set; }

        public int RegionId { get; set; }

        public SubContractor Region { get; set; }
        
        public Month Months { get; set; }
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

        public int AJanitorServices { get; set; }

        public int ADepreciation { get; set; }

        public int ATechSupport { get; set; }

        public int ASecurityServices { get; set; }

        public int AOther { get; set; }

        public int AOther2 { get; set; }

        public int AOther3 { get; set; }

        public int ATotCosts { get; set; }

        private int testmath;

        public int TestMath
        {
            get {
                testmath = ATotCosts - ATechSupport;
                return testmath;
            }
            set { testmath = value; }
        }



    }
}