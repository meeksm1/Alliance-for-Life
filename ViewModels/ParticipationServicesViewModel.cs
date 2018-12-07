using Alliance_for_Life.Controllers;
using Alliance_for_Life.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Alliance_for_Life.ViewModels

{
    public class ParticipationServicesViewModel
    {
        [Display(Name = "Transportation")]
        public int PTranspotation { get; set; }

        [Display(Name = "Job Training")]
        public int PJobTrain { get; set; }

        [Display(Name = "Education Assistance")]
        public int PEducationAssistance { get; set; }

        [Display(Name = "Residential Care")]
        public int PResidentialCare { get; set; }

        [Display(Name = "Utilities")]
        public int PUtilities { get; set; }

        [Display(Name = "Housing Emergency")]
        public int PHousingEmergency { get; set; }

        [Display(Name = "Housing Assistance")]
        public int PHousingAssistance { get; set; }

        [Display(Name = "Child Care")]
        public int PChildCare { get; set; }

        [Display(Name = "Clothing")]
        public int PClothing { get; set; }

        [Display(Name = "Food")]
        public int PFood { get; set; }

        [Display(Name = "Supplies")]
        public int PSupplies { get; set; }

        [Display(Name = "Other")]
        public int POther { get; set; }

        [Display(Name = "Other")]
        public int POther2 { get; set; }

        [Display(Name = "Other")]
        public int POther3 { get; set; }

        [Display(Name = "Participation Totals")]
        public int PTotals { get; set; }

        //used to set the heading of the page
        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ParticipationServicesController, ActionResult>> update =
                    (c => c.Update(this));

                Expression<Func<ParticipationServicesController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Month != 0 && Region != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }

        }

        public int Id { get; internal set; }

        //Navigation Properties
        public int SubcontractorId { get; set; }
        public IEnumerable<SubContractor> Subcontractors { get; set; }

        [Required]
        public int Month { get; set; }

        public IEnumerable<Month> Months { get; set; }

        [Required]
        public int Region { get; set; }

        public IEnumerable<Region> Regions { get; set; }
    }
}