using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class NonResidentialMIR
    {
        [Required]
        public int Id { get; set; }

        public int SubcontractorId { get; set; }

        [Required]
        public double TotBedNights { get; set; }

        [Required]
        public double TotA2AEnrollment { get; set; }

        [Required]
        public double TotA2ABedNights { get; set; }

        public DateTime SubmittedDate { get; set; }

        public double MA2Apercent { get; set; }

        public double ClientsJobEduServ { get; set; }

        public double ParticipatingFathers { get; set; }

        public double TotEduClasses { get; set; }

        public double TotClientsinEduClasses { get; set; }

        public double TotCaseHrs { get; set; }

        public double TotClientsCaseHrs { get; set; }

        public double TotOtherClasses { get; set; }

        public int YearId { get; set; }

        /*Navigation Properties*/
        public Months? Months { get; set; }

       
    }
}