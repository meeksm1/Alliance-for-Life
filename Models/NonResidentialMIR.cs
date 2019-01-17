using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class NonResidentialMIR
    {
        [Required]
        public int Id { get; set; }

        public int Subcontractor { get; set; }

        [Required]
        public decimal TotBedNights { get; set; }

        [Required]
        public decimal TotA2AEnrollment { get; set; }

        [Required]
        public decimal TotA2ABedNights { get; set; }

        [Required]
        public int MonthId { get; set; }

        public Month Months { get; set; }

        public decimal MA2Apercent { get; set; }

        public decimal ClientsJobEduServ { get; set; }

        public decimal ParticipatingFathers { get; set; }

        public decimal TotEduClasses { get; set; }

        public decimal TotClientsinEduClasses { get; set; }

        public decimal TotCaseHrs { get; set; }

        public decimal TotClientsCaseHrs { get; set; }

        public decimal TotOtherClasses { get; set; }
    }
}