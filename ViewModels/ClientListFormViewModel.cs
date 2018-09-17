using Alliance_for_Life.Controllers;
using Alliance_for_Life.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Alliance_for_Life.ViewModels

{
    public class ClientListFormViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public int Subcontractor { get; set; }

        public IEnumerable<SubContractor> SubContractors { get; set; }

        public SubContractor OrgName { get; set; }
        
        [Required]
        [Display(Name = "Clients First Initial of First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Clients Last Name")]
        public string LastName { get; set; }

        [Required]
        [BirthDate]
        [Display(Name = "Clients Birth Date")]
        public string DOB { get; set; }

        public DateTime GetDateTimeDOB()
        {
            return DateTime.Parse(string.Format(DOB));
        }

        [Required]
        [FutureDate]
        [Display(Name = "Due Date")]
        public string DueDate { get; set; }
    
        public DateTime GetDateTimeDueDate()
        {
            return DateTime.Parse(string.Format(DueDate));
        }

        public bool Active { get; set; }

        //used to set the heading of the page
        public string Heading { get; set; }

        //used to switch between actions in the controller
        public string Action
        {
            get
            {
                Expression<Func<ClientListController, ActionResult>> update =
                    (c => c.Update(this));

                Expression<Func<ClientListController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }

        }

    }   
}