using Alliance_for_Life.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class ParticipationServicesViewModel
    {
        [Display(Name = "Transportation")]
        public decimal PTranspotation { get; set; }

        [Display(Name = "Job Training")]
        public decimal PJobTrain { get; set; }

        [Display(Name = "Education Assistance")]
        public decimal PEducationAssistance { get; set; }

        [Display(Name = "Residential Care")]
        public decimal PResidentialCare { get; set; }

        [Display(Name = "Utilities")]
        public decimal PUtilities { get; set; }

        [Display(Name = "Housing Emergency")]
        public decimal PHousingEmergency { get; set; }

        [Display(Name = "Housing Assistance")]
        public decimal PHousingAssistance { get; set; }

        [Display(Name = "Child Care")]
        public decimal PChildCare { get; set; }

        [Display(Name = "Clothing")]
        public decimal PClothing { get; set; }

        [Display(Name = "Food")]
        public decimal PFood { get; set; }

        [Display(Name = "Supplies")]
        public decimal PSupplies { get; set; }

        [Display(Name = "Other")]
        public decimal POther { get; set; }

        [Display(Name = "Other")]
        public decimal POther2 { get; set; }

        [Display(Name = "Other")]
        public decimal POther3 { get; set; }

        [Display(Name = "Participation Totals")]
        public decimal PTotals { get; set; }

        public int Id { get; internal set; }

        //Navigation Properties
        public int SubcontractorId { get; set; }
        public IEnumerable<SubContractor> Subcontractors { get; set; }

        [Required]
        public int Month { get; set; }

        public IEnumerable<Month> Months { get; set; }

        [Required]
        public int Region { get; set; }

        public IEnumerable<Region> Regions { get; set; }
    }
}