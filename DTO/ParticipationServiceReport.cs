using System;

namespace Alliance_for_Life.Models
{
    public class ParticipationServiceReport
    {
        public System.Guid  PSId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string OrgName { get; set; }
        public string MonthName { get; set; }
        public string RegionName { get; set; }
        public int YearName { get; set; }
        public int EIN { get; set; }
        public double PTranspotation { get; set; }
        public double PJobTrain { get; set; }
        public double PEducationAssistance { get; set; }
        public double PResidentialCare { get; set; }
        public double PUtilities { get; set; }
        public double PHousingEmergency { get; set; }
        public double PHousingAssistance { get; set; }
        public double PChildCare { get; set; }
        public double PClothing { get; set; }
        public double PFood { get; set; }
        public double PSupplies { get; set; }
        public double POther { get; set; }
        public double POther2 { get; set; }
        public double POther3 { get; set; }
        public double PTotals { get; set; }
    }
}