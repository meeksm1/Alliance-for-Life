using Alliance_for_Life.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class BudgetCostViewModel
    {
        [Required]
        public GeoRegion? Region { get; set; }

        [Display(Name = "Salaries and Wages")]
        public int ASalandWages { get; set; }

        [Display(Name = "Employee Benefits")]
        public int AEmpBenefits { get; set; }

        [Display(Name = "Employe Travel")]
        public int AEmpTravel { get; set; }

        [Display(Name = "Employee Training")]
        public int AEmpTraining { get; set; }

        [Display(Name = "Office Rent")]
        public int AOfficeRent { get; set; }

        [Display(Name = "Office Utilities")]
        public int AOfficeUtilities { get; set; }

        [Display(Name = "Facility Insurance")]
        public int AFacilityIns { get; set; }

        [Display(Name = "Office Supplies")]
        public int AOfficeSupplies { get; set; }

        [Display(Name = "Equipment")]
        public int AEquipment { get; set; }

        [Display(Name = "Office Communications")]
        public int AOfficeCommunications { get; set; }

        [Display(Name = "Office Maintenance")]
        public int AOfficeMaint { get; set; }

        [Display(Name = "Consulting Fees")]
        public int AConsulting { get; set; }

        [Display(Name = "Subcontractor Payment Costs")]
        public int SubConPayCost { get; set; }

        [Display(Name = "Background Checks")]
        public int BackgrounCheck { get; set; }

        public int Other { get; set; }

        [Display(Name = "Janitorial Services")]
        public int AJanitorServices { get; set; }

        [Display(Name = "Depreciation")]
        public int ADepreciation { get; set; }

        [Display(Name = "Technical Support")]
        public int ATechSupport { get; set; }

        [Display(Name = "Security Services")]
        public int ASecurityServices { get; set; }

        [Display(Name = "Total")]
        public double ATotCosts { get; set; }

        [Display(Name = "Administration Fee's")]
        public double AdminFee { get; set; }

        //used to set the heading of the page
        public string Heading { get; set; }

        public int Trasportation { get; set; }

        [Display(Name = "Job Training")]
        public int JobTraining { get; set; }

        [Display(Name = "Tuition Assistance")]
        public int TuitionAssistance { get; set; }

        [Display(Name = "Contracted Residential")]
        public int ContractedResidential { get; set; }

        [Display(Name = "Utility Assistance")]
        public int UtilityAssistance { get; set; }

        [Display(Name = "Emergency Shelter")]
        public int EmergencyShelter { get; set; }

        [Display(Name = "Housing Assistance")]
        public int HousingAssistance { get; set; }

        public int Childcare { get; set; }

        public int Clothing { get; set; }

        public int Food { get; set; }

        public int Supplies { get; set; }

        public int RFO { get; set; }

        [Display(Name = "Total")]
        public double BTotal { get; set; }

        [Display(Name = "Maximum Annual Total Price")]
        public double Maxtot { get; set; }

        public int Id { get; internal set; }
    }
}