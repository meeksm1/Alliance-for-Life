namespace Alliance_for_Life.Models
{
    public class ParticipationServiceReport
    {
        public int PSId { get; set; }
        public string OrgName { get; set; }
        public string MonthName { get; set; }
        public string RegionName { get; set; }
        public int EIN { get; set; }
        public int PTranspotation { get; set; }
        public int PJobTrain { get; set; }
        public int PEducationAssistance { get; set; }
        public int PResidentialCare { get; set; }
        public int PUtilities { get; set; }
        public int PHousingEmergency { get; set; }
        public int PHousingAssistance { get; set; }
        public int PChildCare { get; set; }
        public int PClothing { get; set; }
        public int PFood { get; set; }
        public int PSupplies { get; set; }
        public int POther { get; set; }
        public int POther2 { get; set; }
        public int POther3 { get; set; }
        public int PTotals { get; set; }
    }
}