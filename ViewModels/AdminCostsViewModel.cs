﻿using Alliance_for_Life.Controllers;
using Alliance_for_Life.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Alliance_for_Life.ViewModels

{
    public class AdminCostsViewModel
    {

        [Required]
        public int Month { get; set; }

        public IEnumerable<Month> Months { get; set; }

        [Required]
        public int Region { get; set; }

        public IEnumerable<Region> Regions { get; set; }

        [Display(Name = "Salaries and Wages.")]
        public int ASalandWages { get; set; }

        [Display(Name = "Employee Benefits.")]
        public int AEmpBenefits { get; set; }

        [Display(Name = "Employe Travel.")]
        public int AEmpTravel { get; set; }

        [Display(Name = "Employee Training.")]
        public int AEmpTraining { get; set; }

        [Display(Name = "Office Rent.")]
        public int AOfficeRent { get; set; }

        [Display(Name = "Office Utilities.")]
        public int AOfficeUtilities { get; set; }

        [Display(Name = "Facility Insurance.")]
        public int AFacilityIns { get; set; }

        [Display(Name = "Office Supplies.")]
        public int AOfficeSupplies { get; set; }

        [Display(Name = "Equipment.")]
        public int AEquipment { get; set; }

        [Display(Name = "Office Communications.")]
        public int AOfficeCommunications { get; set; }

        [Display(Name = "Office Maintenance.")]
        public int AOfficeMaint { get; set; }

        [Display(Name = "Consulting Fees.")]
        public int AConsulting { get; set; }

        [Display(Name = "Janitorial Services.")]
        public int AJanitorServices { get; set; }

        [Display(Name = "Depreciation.")]
        public int ADepreciation { get; set; }

        [Display(Name = "Technical Support.")]
        public int ATechSupport { get; set; }

        [Display(Name = "Security Services.")]
        public int ASecurityServices { get; set; }

        [Display(Name = "Other.")]
        public int AOther { get; set; }

        [Display(Name = "Other.")]
        public int AOther2 { get; set; }

        [Display(Name = "Other.")]
        public int AOther3 { get; set; }

        [Display(Name = "Total Administative Costs.")]
        public int ATotCosts { get; set; }

        //used to set the heading of the page
        public string Heading { get; set; }

        //used to switch between actions in the controller
        public string Action
        {
            get
            {
                Expression<Func<AdminCostsController, ActionResult>> update =
                    (c => c.Update(this));

                Expression<Func<AdminCostsController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }

        }

        public int Id { get; internal set; }
    }
}