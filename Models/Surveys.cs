using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alliance_for_Life.Models
{
    public class Surveys
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SurveyId { get; set; }

        public DateTime SubmittedDate { get; set; }

        [Required]
        [Display(Name = "Surveys Returned")]
        public int SurveysCompleted { get; set; }


        //Navigation Properties
        public SubContractor Subcontractors { get; set; }

        public Months? Month { get; set; }

        public System.Guid SubcontractorId { get; set; }


    }
}