using Alliance_for_Life.Models;
using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Alliance_for_Life.Controllers
{
    public class SurveysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Surveys
        public ActionResult Index()
        {
            var surveys = db.Surveys.Include(s => s.Month).Include(s => s.Subcontractors);
            return View(surveys.ToList());
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
                .Include(s => s.Month)
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
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");
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
                                s.Month == surveys.Month
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
                    return RedirectToAction("Index");
                }
            }

            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", surveys.Subcontractors);

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
            Surveys surveys = db.Surveys.Find(id);
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
