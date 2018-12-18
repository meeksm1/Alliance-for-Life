using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class Surveys
    {
        [Key]
        [Required]
        public int SurveyId { get; set; }

        public int SubcontractorId { get; set; }

        public int MonthId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Surveys Returned")]
        public int SurveysCompleted { get; set; }

        //Navigation Properties
        public SubContractor Subcontractors { get; set; }

        public Month Months { get; set; }
    }
}