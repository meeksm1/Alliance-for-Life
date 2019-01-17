using Alliance_for_Life.Models;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class ResidentialMIRFormViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Subcontractor { get; set; }

        public IEnumerable<SubContractor> Subcontractors { get; set; }

        [Required]
        public int Month { get; set; }

        public IEnumerable<Month> Months { get; set; }

        [Required]
        [Display(Name = "Total of overall Client bed nights for the month")]
        public decimal TotBedNights { get; set; }

        [Required]
        [Display(Name = "Total current A2A enrollment")]
        public decimal TotA2AEnrollment { get; set; }

        [Required]
        [Display(Name = "Total A2A Client bed nights")]
        public decimal TotA2ABedNights { get; set; }

        [Display(Name = "Monthly A2A Clients served")]
        public decimal MA2Apercent { get; set; }

        [Display(Name = "Total Clients engaged in Job Training/Placement or Educational Services")]
        public decimal ClientsJobEduServ { get; set; }

        [Display(Name = "Total fathers who participated in the A2A program")]
        public decimal ParticipatingFathers { get; set; }

        [Display(Name = "Total prenatal & parenting education classes")]
        public decimal TotEduClasses { get; set; }

        [Display(Name = "Total Clients who attended prenatal and parenting education classes")]
        public decimal TotClientsinEduClasses { get; set; }

        [Display(Name = "Total case management hours provided")]
        public decimal TotCaseHrs { get; set; }

        [Display(Name = "Total Clients who participated in case management")]
        public decimal TotClientsCaseHrs { get; set; }

        [Display(Name = "Total of other classes offered")]
        public decimal TotOtherClasses { get; set; }

    }
}