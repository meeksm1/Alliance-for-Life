﻿using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class NonResidentialMIR
    {
        [Required]
        public int Id { get; set; }

        public int Subcontractor { get; set; }

        [Required]
        public int TotBedNights { get; set; }

        [Required]
        public int TotA2AEnrollment { get; set; }

        [Required]
        public int TotA2ABedNights { get; set; }

        [Required]
        public int MonthId { get; set; }

        public Month Months { get; set; }

        public double MA2Apercent { get; set; }

        public int ClientsJobEduServ { get; set; }

        public int ParticipatingFathers { get; set; }

        public int TotEduClasses { get; set; }

        public int TotClientsinEduClasses { get; set; }

        public int TotCaseHrs { get; set; }

        public int TotClientsCaseHrs { get; set; }

        public int TotOtherClasses { get; set; }
    }
}