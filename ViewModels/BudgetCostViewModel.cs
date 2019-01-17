using Alliance_for_Life.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class BudgetCostViewModel
    {
        [Required]
        public int Month { get; set; }

        public IEnumerable<Month> Months { get; set; }

        [Required]
        public int Region { get; set; }

        public IEnumerable<Region> Regions { get; set; }

        [Display(Name = "Salaries and Wages")]
        public decimal ASalandWages { get; set; }

        [Display(Name = "Employee Benefits")]
        public decimal AEmpBenefits { get; set; }

        [Display(Name = "Employe Travel")]
        public decimal AEmpTravel { get; set; }

        [Display(Name = "Employee Training")]
        public decimal AEmpTraining { get; set; }

        [Display(Name = "Office Rent")]
        public decimal AOfficeRent { get; set; }

        [Display(Name = "Office Utilities")]
        public decimal AOfficeUtilities { get; set; }

        [Display(Name = "Facility Insurance")]
        public decimal AFacilityIns { get; set; }

        [Display(Name = "Office Supplies")]
        public decimal AOfficeSupplies { get; set; }

        [Display(Name = "Equipment")]
        public decimal AEquipment { get; set; }

        [Display(Name = "Office Communications")]
        public decimal AOfficeCommunications { get; set; }

        [Display(Name = "Office Maintenance")]
        public decimal AOfficeMaint { get; set; }

        [Display(Name = "Consulting Fees")]
        public decimal AConsulting { get; set; }

        [Display(Name = "Subcontractor Payment Costs")]
        public decimal SubConPayCost { get; set; }

        [Display(Name = "Background Checks")]
        public decimal BackgrounCheck { get; set; }

        public decimal Other { get; set; }

        [Display(Name = "Janitorial Services")]
        public decimal AJanitorServices { get; set; }

        [Display(Name = "Depreciation")]
        public decimal ADepreciation { get; set; }

        [Display(Name = "Technical Support")]
        public decimal ATechSupport { get; set; }

        [Display(Name = "Security Services")]
        public decimal ASecurityServices { get; set; }

        [Display(Name = "Total")]
        public decimal ATotCosts { get; set; }

        [Display(Name = "Administration Fee's")]
        public decimal AdminFee { get; set; }

        //used to set the heading of the page
        public string Heading { get; set; }

        public decimal Trasportation { get; set; }

        [Display(Name = "Job Training")]
        public decimal JobTraining { get; set; }

        [Display(Name = "Tuition Assistance")]
        public decimal TuitionAssistance { get; set; }

        [Display(Name = "Contracted Residential")]
        public decimal ContractedResidential { get; set; }

        [Display(Name = "Utility Assistance")]
        public decimal UtilityAssistance { get; set; }

        [Display(Name = "Emergency Shelter")]
        public decimal EmergencyShelter { get; set; }

        [Display(Name = "Housing Assistance")]
        public decimal HousingAssistance { get; set; }

        public decimal Childcare { get; set; }

        public decimal Clothing { get; set; }

        public decimal Food { get; set; }

        public decimal Supplies { get; set; }

        public decimal RFO { get; set; }

        [Display(Name = "Total")]
        public decimal BTotal { get; set; }

        [Display(Name = "Maximum Annual Total Price")]
        public decimal Maxtot { get; set; }

        public int Id { get; internal set; }
    }
}