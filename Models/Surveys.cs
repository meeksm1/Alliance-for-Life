using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class Surveys
    {
        [Key]
        [Required]
        public int SurveyId { get; set; }

        public DateTime SubmittedDate { get; set; }

        [Required]
        [Display(Name = "Surveys Returned")]
        public int SurveysCompleted { get; set; }


        //Navigation Properties
        public SubContractor Subcontractors { get; set; }
        public Months? Months { get; set; }

        public int SubcontractorId { get; set; }


    }
}