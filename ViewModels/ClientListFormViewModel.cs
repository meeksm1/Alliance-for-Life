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
        public System.Guid Id { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public System.Guid SubcontractorId { get; set; }

        public int UserId { get; set; }

        [Required]
        [Display(Name = "Clients First Initial of First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Clients Last Name")]
        public string LastName { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Clients Birth Date")]
        public DateTime DOB { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        public bool Active { get; set; }

        public DateTime SubmittedDate { get; set; }

        //Used for Navigation Properties
        public IEnumerable<SubContractor> Subcontractors { get; set; }

        public IEnumerable<Role> User { get; set; }

        //used to set the heading of the page
        public string Heading { get; set; }

        //  used to switch between actions in the controller
        public string Action
        {
            get
            {
                Expression<Func<ClientListController, ActionResult>> update =
                    (c => c.Update(this));

                Expression<Func<ClientListController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != Guid.Empty) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }

        }

    }
}