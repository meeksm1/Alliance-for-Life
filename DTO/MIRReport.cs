using System;

namespace Alliance_for_Life.Models
{
    public class MIRReport
    {
        public int Id { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string OrgName { get; set; }
        public string YearName { get; set; }
        public int TotBedNights { get; set; }
        public int TotA2AEnrollment { get; set; }
        public int TotA2ABedNights { get; set; }
        public string Month { get; set; }
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