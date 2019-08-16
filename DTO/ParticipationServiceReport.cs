using System;

namespace Alliance_for_Life.Models
{
    public class ParticipationServiceReport
    {
        public System.Guid PSId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string OrgName { get; set; }
        public string MonthName { get; set; }
        public string RegionName { get; set; }
        public int YearName { get; set; }
        public int EIN { get; set; }
        public string PTranspotation { get; set; }
        public string PJobTrain { get; set; }
        public string PEducationAssistance { get; set; }
        public string PResidentialCare { get; set; }
        public string PUtilities { get; set; }
        public string PHousingEmergency { get; set; }
        public string PHousingAssistance { get; set; }
        public string PChildCare { get; set; }
        public string PClothing { get; set; }
        public string PFood { get; set; }
        public string PSupplies { get; set; }
        public string POther { get; set; }
        public string POther2 { get; set; }
        public string POther3 { get; set; }
        public string PTotals { get; set; }
    }
}