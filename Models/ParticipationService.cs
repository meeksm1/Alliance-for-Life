using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class ParticipationService
    {
        [Key]
        public System.Guid PSId { get; set; }

        public double BackgroudCheck { get; set; }

        public double PTranspotation { get; set; }

        public double PJobTrain { get; set; }

        public double PEducationAssistance { get; set; }

        public double PBirthCerts { get; set; }
        
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

        public DateTime SubmittedDate { get; set; }

        public int Year { get; set; }


        //Navigation Properties
        public Months? Month { get; set; }
        public SubContractor Subcontractor { get; set; }

        public System.Guid SubcontractorId { get; set; }
    }
}