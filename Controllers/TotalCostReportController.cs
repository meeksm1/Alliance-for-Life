using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class TotalCostReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index(string sortOrder, int? Year, string Month, Guid? searchString, string currentFilter, int? page, int? pgSize)
        {

            ViewBag.Sub = searchString;
            ViewBag.Yr = Year;
            ViewBag.Mnth = Month;

            var datelist = Enumerable.Range(System.DateTime.Now.Year, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.Month = new SelectList(Enum.GetValues(typeof(Months)).Cast<Months>());
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Subcontractor = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName");

            //looking for the searchstring
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                currentFilter = searchString.ToString();
            }
            ViewBag.CurrentFilter = searchString;

            //assign default values if Year is empty
            if (String.IsNullOrEmpty(Year.ToString()))
            {
                Year = DateTime.Now.Year;
            }

            //assign default values if Month is empty
            if (String.IsNullOrEmpty(Month))
            {
                Month = DateTime.Now.Month.ToString();
                ViewBag.Title = "Total Cost Report for " + DateTime.Now.ToString("MMMM") + "-" + Year;
            }
            else
            {
                ViewBag.Title = "Total Cost Report for " + Month + "-" + Year;
            }



            var mymodel = new TotalCostReport();
            mymodel.AdminCosts = from a in db.AdminCosts
                                  join b in db.SubContractors on a.SubcontractorId equals b.SubcontractorId
                                  select a;

            mymodel.ParticipationCost = from a in db.ParticipationServices.ToList()
                                     join b in db.SubContractors on a.SubcontractorId equals b.SubcontractorId
                                     select a;
            //if year is not null
            if (Year != null)
            {

                mymodel.AdminCosts= mymodel.AdminCosts .Where(a => a.Year == Year);
                mymodel.ParticipationCost = mymodel.ParticipationCost.Where(a => a.Year == Year);

            }
            ////if month is not null
            if (!String.IsNullOrEmpty(Month))
            {
                mymodel.AdminCosts= mymodel.AdminCosts .Where(a => a.Month.ToString() == Month);
                mymodel.ParticipationCost = mymodel.ParticipationCost.Where(a => a.Month.ToString() == Month);
            }

            if (!User.IsInRole("Admin"))
            {
                var organization = "";
                var id = User.Identity.GetUserId();
                var usr = db.Users.Find(id);
                organization = db.SubContractors.Find(usr.SubcontractorId).OrgName;
                var usersubid = db.Users.Find(id).SubcontractorId;


                mymodel.AdminCosts= mymodel.AdminCosts .Where(t => t.SubcontractorId == usersubid);
                mymodel.ParticipationCost = mymodel.ParticipationCost.Where(t => t.SubcontractorId == usersubid);

                ViewBag.Subcontractor = organization;
            }


            if (!String.IsNullOrEmpty(searchString.ToString()))
            {

                mymodel.AdminCosts= mymodel.AdminCosts .Where(t => t.Subcontractor.SubcontractorId == searchString);
                mymodel.ParticipationCost = mymodel.ParticipationCost.Where(t => t.Subcontractor.SubcontractorId == searchString);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    mymodel.AdminCosts= mymodel.AdminCosts .OrderByDescending(s => s.Subcontractor.OrgName);
                    mymodel.ParticipationCost = mymodel.ParticipationCost.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    mymodel.AdminCosts= mymodel.AdminCosts .OrderBy(s => s.SubmittedDate);
                    mymodel.ParticipationCost = mymodel.ParticipationCost.OrderBy(s => s.SubmittedDate);
                    break;
                case "date_desc":
                    mymodel.AdminCosts= mymodel.AdminCosts .OrderByDescending(s => s.SubmittedDate);
                    mymodel.ParticipationCost = mymodel.ParticipationCost.OrderByDescending(s => s.SubmittedDate);
                    break;
                default:
                    mymodel.AdminCosts= mymodel.AdminCosts .OrderBy(s => s.Subcontractor.OrgName);
                    mymodel.ParticipationCost = mymodel.ParticipationCost.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }

            int pageNumber = (page ?? 1);
            int defaSize = (pgSize ?? 10);

            ViewBag.psize = defaSize;

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="20", Text= "20" },
                new SelectListItem() { Value="30", Text= "30" },
                new SelectListItem() { Value="40", Text= "40" },
            };


            return View(mymodel);
        }

    }
}
