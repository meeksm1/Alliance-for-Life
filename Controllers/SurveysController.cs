using Alliance_for_Life.Models;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Alliance_for_Life.Controllers
{
    [Authorize]
    public class SurveysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Surveys
        public ActionResult Index(string sortOrder, Guid? searchString, string Month, string currentFilter, int? Year, int? page, int? pgSize)
        {
            ViewBag.Sub = searchString;
            ViewBag.Yr = Year;
            ViewBag.Mnth = Month;

            //paged view
            ViewBag.CurrentSort = sortOrder;
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 1, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.Subcontractor = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName");
            ViewBag.Month = new SelectList(Enum.GetValues(typeof(Months)).Cast<Months>());

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

            var surveys = db.Surveys.Include(s => s.Subcontractors);

            if (!User.IsInRole("Admin"))
            {
                var id = User.Identity.GetUserId();
                var usersubid = db.Users.Find(id).SubcontractorId;

                surveys = from s in db.Surveys
                          where usersubid == s.SubcontractorId
                          select s;
            }

           

            if ((Year != null) && Year.ToString().Length > 1)
            {

                surveys = surveys.Where(a => a.SubmittedDate.Year == Year);

            }

            if (!String.IsNullOrEmpty(Month) || !String.IsNullOrEmpty(searchString.ToString()))
            {

                if (String.IsNullOrEmpty(Month))
                {
                    surveys = surveys.Where(a => a.Subcontractors.SubcontractorId == searchString);
                }
                else if (String.IsNullOrEmpty(searchString.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    surveys = surveys.Where(r => r.Month == (Months)regionSearch);
                }
                else
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    surveys = surveys.Where(r => r.Month == (Months)regionSearch && r.Subcontractors.SubcontractorId == searchString);
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    surveys = surveys.OrderByDescending(s => s.Subcontractors.OrgName);
                    break;
                case "Date":
                    surveys = surveys.OrderBy(s => s.SubmittedDate);
                    break;
                case "date_desc":
                    surveys = surveys.OrderByDescending(s => s.SubmittedDate);
                    break;
                default:
                    surveys = surveys.OrderBy(s => s.Subcontractors.OrgName);
                    break;
            }


            ViewBag.TotalSum = surveys.Count();

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

            return View(surveys.ToPagedList(pageNumber, defaSize));
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = db.Surveys
                .Include(s => s.Subcontractors)
                .SingleOrDefault(s => s.SurveyId == id);
            if (surveys == null)
            {
                return HttpNotFound();
            }
            return View(surveys);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {
            ViewBag.SubcontractorId = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName");
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyId,SubcontractorId,Month,Date,SurveysCompleted,SubmittedDate")] Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.Surveys
                                where
                                s.SubcontractorId == surveys.SubcontractorId &&
                                s.Month == surveys.Month && s.SubmittedDate.Year == DateTime.Today.Year
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    surveys.SubmittedDate = DateTime.Now;
                    db.Surveys.Add(surveys);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
            }

            ViewBag.SubcontractorId = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName", surveys.Subcontractors);

            return View(surveys);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = db.Surveys.Find(id);
            if (surveys == null)
            {
                return HttpNotFound();
            }

            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", surveys.SubcontractorId);
            return View(surveys);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyId,SubcontractorId,Month,Date,SurveysCompleted,SubmittedDate")] Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                surveys.SubmittedDate = DateTime.Now;
                db.Entry(surveys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", surveys.SubcontractorId);
            return View(surveys);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = db.Surveys
                 .Include(s => s.Subcontractors)
                 .SingleOrDefault(s => s.SurveyId == id);
            if (surveys == null)
            {
                return HttpNotFound();
            }
            return View(surveys);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Surveys surveys = db.Surveys.Find(id);
            db.Surveys.Remove(surveys);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5]
            {
                new DataColumn ("Survey ID"),
                new DataColumn ("Month"),
                new DataColumn ("Organization"),
                new DataColumn ("Surveys Returned"),
                new DataColumn ("Date Submitted")
            });

            var query = from s in db.Surveys
                        join sc in db.SubContractors on s.SubcontractorId equals sc.SubcontractorId

                        select new SurveyReport
                        {
                            SurveyId = s.SurveyId,
                            Month = s.Month,
                            Orgname = sc.OrgName,
                            SurveysCompleted = s.SurveysCompleted,
                            SubmittedDate = DateTime.Now
                        };

            foreach (var item in query)
            {
                dt.Rows.Add(item.SurveyId, item.Month, item.Orgname, item.SurveysCompleted, item.SubmittedDate);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
    }
}
