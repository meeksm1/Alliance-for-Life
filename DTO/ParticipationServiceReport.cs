namespace Alliance_for_Life.Models
{
    public class ParticipationServiceReport
    {
        public int PSId { get; set; }
        public string OrgName { get; set; }
        public string MonthName { get; set; }
        public string RegionName { get; set; }
        public int EIN { get; set; }
        public decimal PTranspotation { get; set; }
        public decimal PJobTrain { get; set; }
        public decimal PEducationAssistance { get; set; }
        public decimal PResidentialCare { get; set; }
        public decimal PUtilities { get; set; }
        public decimal PHousingEmergency { get; set; }
        public decimal PHousingAssistance { get; set; }
        public decimal PChildCare { get; set; }
        public decimal PClothing { get; set; }
        public decimal PFood { get; set; }
        public decimal PSupplies { get; set; }
        public decimal POther { get; set; }
        public decimal POther2 { get; set; }
        public decimal POther3 { get; set; }
        public decimal PTotals { get; set; }
    }
}