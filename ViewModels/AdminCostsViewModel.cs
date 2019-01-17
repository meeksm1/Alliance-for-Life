using Alliance_for_Life.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class AdminCostsViewModel
    {
        public int SubcontractorId { get; set; }
        public IEnumerable<SubContractor> Subcontractors { get; set; }

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

        [Display(Name = "Janitorial Services")]
        public decimal AJanitorServices { get; set; }

        [Display(Name = "Depreciation")]
        public decimal ADepreciation { get; set; }

        [Display(Name = "Technical Support")]
        public decimal ATechSupport { get; set; }

        [Display(Name = "Security Services")]
        public decimal ASecurityServices { get; set; }

        [Display(Name = "Other")]
        public decimal AOther { get; set; }

        [Display(Name = "Other")]
        public decimal AOther2 { get; set; }

        [Display(Name = "Other")]
        public decimal AOther3 { get; set; }

        [Display(Name = "Total Administative Costs")]
        public decimal ATotCosts { get; set; }
    }
}