using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class MonthlyServiceReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       // GET: MonthlyServiceReport
      [Authorize ]
        public ActionResult Index(string sortOrder, int? Year, string Month, Guid? searchString, string currentFilter, int? page, int? pgSize)
        {

            ViewBag.Sub = searchString;
            ViewBag.Yr = Year;
            ViewBag.Mnth = Month;

            var datelist = Enumerable.Range(System.DateTime.Now.Year-1, 5).ToList();
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
                ViewBag.Title = "Monthly Service Report for " + DateTime.Now.ToString("MMMM") + "-" + Year;
            }
            else
            {
                ViewBag.Title = "Monthly Service Report for " + Month + "-" + Year;
            }



            var mymodel = new MonthlyServices();
            mymodel.Residential = from a in db.ResidentialMIRs
                                  join b in db.SubContractors on a.SubcontractorId equals b.SubcontractorId
                                  select a;

            mymodel.NonResidential = from a in db.NonResidentialMIRs.ToList()
                                     join b in db.SubContractors on a.SubcontractorId equals b.SubcontractorId
                                     select a;
            //if year is not null
            if (Year != null)
            {
              
                mymodel.Residential = mymodel.Residential.Where(a => a.Year == Year); 
                mymodel.NonResidential = mymodel.NonResidential.Where(a => a.Year == Year);

            }
            ////if month is not null
            if (!String.IsNullOrEmpty(Month))
            {
                mymodel.Residential = mymodel.Residential.Where(a => a.Month.ToString() == Month);
                mymodel.NonResidential = mymodel.NonResidential.Where(a => a.Months.ToString() == Month);
            }

            if (!User.IsInRole("Admin"))
            {
                var organization = "";
                var id = User.Identity.GetUserId();
                var usr = db.Users.Find(id);
                organization = db.SubContractors.Find(usr.SubcontractorId).OrgName;
                var usersubid = db.Users.Find(id).SubcontractorId;

               
                mymodel.Residential = mymodel.Residential.Where(t => t.SubcontractorId == usersubid);
                mymodel.NonResidential = mymodel.NonResidential.Where(t => t.SubcontractorId == usersubid);

                ViewBag.Subcontractor = organization;
            }


            if (!String.IsNullOrEmpty(searchString.ToString()))
            {
              
                mymodel.Residential = mymodel.Residential.Where(t => t.Subcontractor.SubcontractorId == searchString);
                mymodel.NonResidential = mymodel.NonResidential.Where(t => t.Subcontractor.SubcontractorId == searchString);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    mymodel.Residential = mymodel.Residential.OrderByDescending(s => s.Subcontractor.OrgName);
                    mymodel.NonResidential = mymodel.NonResidential.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    mymodel.Residential = mymodel.Residential.OrderBy(s => s.SubmittedDate);
                    mymodel.NonResidential = mymodel.NonResidential.OrderBy(s => s.SubmittedDate);
                    break;
                case "date_desc":
                    mymodel.Residential = mymodel.Residential.OrderByDescending(s => s.SubmittedDate);
                    mymodel.NonResidential = mymodel.NonResidential.OrderByDescending(s => s.SubmittedDate);
                    break;
                default:
                    mymodel.Residential = mymodel.Residential.OrderBy(s => s.Subcontractor.OrgName);
                    mymodel.NonResidential = mymodel.NonResidential.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }

            int pageNumber = (page ?? 1);
            int defaSize = (pgSize ?? 15);

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
