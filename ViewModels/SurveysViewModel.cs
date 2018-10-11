using Alliance_for_Life.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.ViewModels
{
    public class SurveysViewModel
    {
        [Key]
        [Required]
        public int SurveyId { get; set; }


        public SubContractor SubcontractorId { get; set; }


        [Required]
        public SubContractor OrgName { get; set; }


        public int MonthId { get; set; }

        [Required]
        public Month Months { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int SurveysCompleted { get; set; }

        //used to set the heading of the page
        public string Heading { get; set; }

        //public string Action
        //{
        //    get
        //    {
        //        Expression<Func<SubContractorController, ActionResult>> update =
        //            (c => c.Update(this));

        //        Expression<Func<SubContractorController, ActionResult>> create =
        //            (c => c.Create(this));

        //        var action = (Id != 0) ? update : create;
        //        return (action.Body as MethodCallExpression).Method.Name;
        //    }

        //}
    }
}