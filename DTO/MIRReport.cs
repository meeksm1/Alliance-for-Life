namespace Alliance_for_Life.Models
{
    public class MIRReport
    {
        public int Id { get; set; }

        public string OrgName { get; set; }

        public decimal TotBedNights { get; set; }

        public decimal TotA2AEnrollment { get; set; }

        public decimal TotA2ABedNights { get; set; }

        public string Month { get; set; }

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