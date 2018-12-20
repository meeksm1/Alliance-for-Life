using Alliance_for_Life.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class QuarterlyStatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuarterlyStates
        public ActionResult Index()
        {
            return View(db.QuarterlyStates.ToList());
        }

        public ActionResult Report()
        {
            var quarterfee1 = db.AdminCosts.FirstOrDefault(q => q.Month.Id == 1).ATotCosts;
            var quarterfee2 = db.AdminCosts.FirstOrDefault(q => q.Month.Id == 2).ATotCosts;
            var quarterfee3 = db.AdminCosts.FirstOrDefault(q => q.Month.Id == 3).ATotCosts;
            new QuarterlyState
            {
                TotDAandPSQuarter = quarterfee3 + quarterfee2 + quarterfee1
            };

            return View();
        }

        // GET: QuarterlyStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuarterlyState quarterlyState = db.QuarterlyStates.Find(id);
            if (quarterlyState == null)
            {
                return HttpNotFound();
            }
            return View(quarterlyState);
        }

        // GET: QuarterlyStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuarterlyStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuraterlyStateId,TotPSforQuarter,TotDAforQuarter,StateFeeQuarter,TotDAandPSQuarter")] QuarterlyState quarterlyState)
        {
            if (ModelState.IsValid)
            {
                db.QuarterlyStates.Add(quarterlyState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quarterlyState);
        }

        // GET: QuarterlyStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuarterlyState quarterlyState = db.QuarterlyStates.Find(id);
            if (quarterlyState == null)
            {
                return HttpNotFound();
            }
            return View(quarterlyState);
        }

        // POST: QuarterlyStates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuraterlyStateId,TotPSforQuarter,TotDAforQuarter,StateFeeQuarter,TotDAandPSQuarter")] QuarterlyState quarterlyState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quarterlyState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quarterlyState);
        }

        // GET: QuarterlyStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuarterlyState quarterlyState = db.QuarterlyStates.Find(id);
            if (quarterlyState == null)
            {
                return HttpNotFound();
            }
            return View(quarterlyState);
        }

        // POST: QuarterlyStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuarterlyState quarterlyState = db.QuarterlyStates.Find(id);
            db.QuarterlyStates.Remove(quarterlyState);
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
