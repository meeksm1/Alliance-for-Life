using Alliance_for_Life.Models;
using System.Data.Entity;
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
            var surveys = db.Surveys.Include(s => s.Months).Include(s => s.Subcontractors);
            return View(surveys.ToList());
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
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

        // GET: Surveys/Create
        public ActionResult Create()
        {
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months");
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyId,SubcontractorId,MonthId,Date,SurveysCompleted")] Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(surveys);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", surveys.Months);
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
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", surveys.MonthId);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", surveys.SubcontractorId);
            return View(surveys);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyId,SubcontractorId,MonthId,Date,SurveysCompleted")] Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", surveys.MonthId);
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
    }
}
