using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.Models
{
    public class MonthlyServices
    {
        public Guid Id { get; set;}
        public DateTime SubmittedDate { get; set; }

        public System.Guid SubcontractorId { get; set; }

        [Required]
        public double RTotBedNights { get; set; }

        [Required]
        public double RTotA2AEnrollment { get; set; }

        [Required]
        public double RTotA2ABedNights { get; set; }

        public double RMA2Apercent { get; set; }

        public double RClientsJobEduServ { get; set; }

        public double RParticipatingFathers { get; set; }

        public double RTotEduClasses { get; set; }

        public double RTotClientsinEduClasses { get; set; }

        public double RTotCaseHrs { get; set; }

        public double RTotClientsCaseHrs { get; set; }

        public double RTotOtherClasses { get; set; }

        [Required]
        public double NTotA2AEnrollment { get; set; }

        public double NMA2Apercent { get; set; }

        public double NClientsJobEduServ { get; set; }

        public double NParticipatingFathers { get; set; }

        public double NTotEduClasses { get; set; }

        public double NTotClientsinEduClasses { get; set; }

        public double NTotCaseHrs { get; set; }

        public double NTotClientsCaseHrs { get; set; }

        public double NTotOtherClasses { get; set; }

        public int Year { get; set; }


        /*Navigation Properties*/
        public Months? Month { get; set; }
        public SubContractor Subcontractor { get; set; }
    }
}
