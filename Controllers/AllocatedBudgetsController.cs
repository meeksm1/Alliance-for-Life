using Alliance_for_Life.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class AllocatedBudgetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AllocatedBudgets
        public ActionResult Index()
        {
            var allocatedBudget = db.AllocatedBudget.Include(a => a.Subcontractor);
            return View(allocatedBudget.ToList());
        }

        // GET: AllocatedBudgets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocatedBudget allocatedBudget = db.AllocatedBudget.Find(id);
            if (allocatedBudget == null)
            {
                return HttpNotFound();
            }
            return View(allocatedBudget);
        }

        // GET: AllocatedBudgets/Create
        public ActionResult Create()
        {
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");
            return View();
        }

        // POST: AllocatedBudgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllocatedBudgetID,SubcontractorId,Region,Month,Year,AllocatedNewBudget,AllocatedOldBudget,AllocationAdjustedDate")] AllocatedBudget allocatedBudget)
        {
            if (ModelState.IsValid)
            {
                allocatedBudget.AllocatedBudgetID = Guid.NewGuid();
                db.AllocatedBudget.Add(allocatedBudget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", allocatedBudget.SubcontractorId);
            return View(allocatedBudget);
        }

        // GET: AllocatedBudgets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocatedBudget allocatedBudget = db.AllocatedBudget.Find(id);
            if (allocatedBudget == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", allocatedBudget.SubcontractorId);
            return View(allocatedBudget);
        }

        // POST: AllocatedBudgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllocatedBudgetID,SubcontractorId,Region,Month,Year,AllocatedNewBudget,AllocatedOldBudget,AllocationAdjustedDate")] AllocatedBudget allocatedBudget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allocatedBudget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", allocatedBudget.SubcontractorId);
            return View(allocatedBudget);
        }

        // GET: AllocatedBudgets/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocatedBudget allocatedBudget = db.AllocatedBudget.Find(id);
            if (allocatedBudget == null)
            {
                return HttpNotFound();
            }
            return View(allocatedBudget);
        }

        // POST: AllocatedBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AllocatedBudget allocatedBudget = db.AllocatedBudget.Find(id);
            db.AllocatedBudget.Remove(allocatedBudget);
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
