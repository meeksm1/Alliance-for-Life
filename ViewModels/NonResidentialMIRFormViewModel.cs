using Alliance_for_Life.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels

{
    public class NonResidentialMIRFormViewModel
    {
        [Required]
        public System.Guid Id { get; set; }

        public DateTime SubmittedDate { get; set; }

        [Required]
        [Display(Name = "Total current A2A enrollment.")]
        public int TotA2AEnrollment { get; set; }

        [Display(Name = "Monthly A2A Clients served.")]
        public double MA2Apercent { get; set; }

        [Display(Name = "Total Clients engaged in Job Training/Placement or Educational Services.")]
        public int ClientsJobEduServ { get; set; }

        [Display(Name = "Total fathers who participated in the A2A program.")]
        public int ParticipatingFathers { get; set; }

        [Display(Name = "Total prenatal & parenting education classes.")]
        public int TotEduClasses { get; set; }

        [Display(Name = "Total Clients who attended prenatal and parenting education classes.")]
        public int TotClientsinEduClasses { get; set; }

        [Display(Name = "Total case management hours provided.")]
        public int TotCaseHrs { get; set; }

        [Display(Name = "Total Clients who participated in case management.")]
        public int TotClientsCaseHrs { get; set; }

        [Display(Name = "Total of other classes offered.")]
        public int TotOtherClasses { get; set; }

        /*Navigation Properties*/
        public IEnumerable<SubContractor> Subcontractor { get; set; }
        public Months? Month { get; set; }

        public System.Guid SubcontractorId { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }
    }
}