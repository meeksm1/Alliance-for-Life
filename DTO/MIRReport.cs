using System;

namespace Alliance_for_Life.Models
{
    public class MIRReport
    {
        public System.Guid Id { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string OrgName { get; set; }
        public int YearName { get; set; }
        public double TotBedNights { get; set; }
        public double TotA2AEnrollment { get; set; }
        public double TotA2ABedNights { get; set; }
        public string Month { get; set; }
        public double MA2Apercent { get; set; }
        public double ClientsJobEduServ { get; set; }
        public double ParticipatingFathers { get; set; }
        public double TotEduClasses { get; set; }
        public double TotClientsinEduClasses { get; set; }
        public double TotCaseHrs { get; set; }
        public double TotClientsCaseHrs { get; set; }
        public double TotOtherClasses { get; set; }
    }
}