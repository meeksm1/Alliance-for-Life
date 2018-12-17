using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class ParticipationService
    {
        [Key]
        public int PSId { get; set; }

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

        //Navigation Properties
        public Region Region { get; set; }

        public Month Month { get; set; }

        public SubContractor Subcontractor { get; set; }

        public int RegionId { get; set; }
        public int MonthId { get; set; }
        public int SubcontractorId { get; set; }
    }
}