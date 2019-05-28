using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class ResidentialMIR
    { 
        [Required]
        public System.Guid Id { get; set; }

        public DateTime SubmittedDate { get; set; }

        public System.Guid SubcontractorId { get; set; }

        [Required]
        public double TotBedNights { get; set; }

        [Required]
        public double TotA2AEnrollment { get; set; }

        [Required]
        public double TotA2ABedNights { get; set; }

        public double MA2Apercent { get; set; }

        public double ClientsJobEduServ { get; set; }

        public double ParticipatingFathers { get; set; }

        public double TotEduClasses { get; set; }

        public double TotClientsinEduClasses { get; set; }

        public double TotCaseHrs { get; set; }

        public double TotClientsCaseHrs { get; set; }

        public double TotOtherClasses { get; set; }

        public int Year { get; set; }

        public double TotOverallServed { get; set; }

        public Guid NonResidentialId { get; set; }



        /*Navigation Properties*/
        public Months? Month { get; set; }
        public SubContractor Subcontractor { get; set; }

        public NonResidentialMIR NonResidential { get; set; }

    }
}