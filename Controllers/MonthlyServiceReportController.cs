using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class MonthlyServiceReportController : Controller
    {
        private  ApplicationDbContext db = new ApplicationDbContext();

        // GET: MonthlyServiceReport
        //public ActionResult Index(string sortOrder, int? Year, string Month, Guid? searchString, string currentFilter, int? page, int? pgSize)
        //{

        //    ViewBag.Sub = searchString;
        //    ViewBag.Yr = Year;
        //    ViewBag.Mnth = Month;

        //    var datelist = Enumerable.Range(System.DateTime.Now.Year, 5).ToList();
        //    ViewBag.Year = new SelectList(datelist);
        //    ViewBag.Month = new SelectList(Enum.GetValues(typeof(Months)).Cast<Months>());
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
        //    ViewBag.Subcontractor = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName");

        //    //looking for the searchstring
        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        currentFilter = searchString.ToString();
        //    }
        //    ViewBag.CurrentFilter = searchString;

        //    //assign default values if Year is empty
        //    if (String.IsNullOrEmpty(Year.ToString()))
        //    {
        //        Year = DateTime.Now.Year;
        //    }

        //    //assign default values if Month is empty
        //    if (String.IsNullOrEmpty(Month))
        //    {
        //        Month = DateTime.Now.Month.ToString();
        //        ViewBag.Title = "Monthly Service Report for " + DateTime.Now.ToString("MMMM") + "-" + Year;
        //    }
        //    else
        //    {
        //        ViewBag.Title = "Monthly Service Report for " + Month + "-" + Year;
        //    }



        //    var monthlyservice = from a in db.ResidentialMIRs
        //                         where a.Year == Year && a.Month.ToString() == Month
        //                         from p in db.NonResidentialMIRs
        //                         where p.Year == Year && p.Months.ToString() == Month
        //                         select new MonthlyServices
        //                         {
        //                             Residential = a,
        //                             NonResidential = p
        //                         };


        //    //var monthlyservice = from a in db.ResidentialMIRs
        //    //                     join p in db.NonResidentialMIRs on a.SubcontractorId equals p.SubcontractorId
        //    //                    where (a.Year == Year && p.Year == Year) && (a.Month.ToString() == Month && p.Months.ToString() == Month)
        //    //                    select new MonthlyServices
        //    //                    {
        //    //                        Residential = a,
        //    //                           NonResidential = p
        //    //                    };
        //    //if year is not null
        //    if (Year != null)
        //    {
        //        monthlyservice = monthlyservice.Where(a => a.Residential.Year == Year).Where(b => b.NonResidential.Year == Year);
        //    }
        //    //if month is not null
        //    if (!String.IsNullOrEmpty(Month))
        //    {

        //        monthlyservice = monthlyservice.Where(a => a.Residential.Month.ToString() == Month).Where(b=>b.NonResidential.Months.ToString() == Month);
        //    }

        //    if (!User.IsInRole("Admin"))
        //    {
        //        var organization = "";
        //        var id = User.Identity.GetUserId();
        //        var usr = db.Users.Find(id);
        //        organization = db.SubContractors.Find(usr.SubcontractorId).OrgName;
        //        var usersubid = db.Users.Find(id).SubcontractorId;

        //        monthlyservice = monthlyservice.Where(t => t.Residential.SubcontractorId == usersubid ).Where(b=>b.NonResidential.SubcontractorId == usersubid);

        //        ViewBag.Subcontractor = organization;
        //    }


        //    if (!String.IsNullOrEmpty(searchString.ToString()))
        //    {
        //        monthlyservice = monthlyservice.Where(t => t.Residential.Subcontractor.SubcontractorId == searchString).Where(b => b.NonResidential.Subcontractor.SubcontractorId == searchString);
        //    }

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            monthlyservice = monthlyservice.OrderByDescending(s => s.Residential.Subcontractor.OrgName).ThenBy(t => t.NonResidential.Subcontractor.OrgName);
        //            break;
        //        case "Date":
        //            monthlyservice = monthlyservice.OrderBy(s => s.Residential.SubmittedDate).ThenBy(t => t.NonResidential.SubmittedDate);
        //            break;
        //        case "date_desc":
        //            monthlyservice = monthlyservice.OrderByDescending(s => s.Residential.SubmittedDate).ThenBy(t => t.NonResidential.SubmittedDate);
        //            break;
        //        default:
        //            monthlyservice = monthlyservice.OrderBy(s => s.Residential.Subcontractor.OrgName).ThenBy(t=>t.NonResidential.Subcontractor.OrgName);
        //            break;
        //    }

        //    int pageNumber = (page ?? 1);
        //    int defaSize = (pgSize ?? 10);

        //    ViewBag.psize = defaSize;

        //    ViewBag.PageSize = new List<SelectListItem>()
        //    {
        //        new SelectListItem() { Value="10", Text= "10" },
        //        new SelectListItem() { Value="20", Text= "20" },
        //        new SelectListItem() { Value="30", Text= "30" },
        //        new SelectListItem() { Value="40", Text= "40" },
        //    };

        //    return View(monthlyservice.ToPagedList(pageNumber, defaSize));


        //}

        // GET: MonthlyServiceReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MonthlyServiceReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MonthlyServiceReport/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MonthlyServiceReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MonthlyServiceReport/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult MultiData()
        {
            var mymodel = new MonthlyServices();

            mymodel.Residential = db.ResidentialMIRs.ToList();
            mymodel.NonResidential = db.NonResidentialMIRs.ToList();

            return View(mymodel);
        }
        // GET: MonthlyServiceReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MonthlyServiceReport/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
