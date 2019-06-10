using Alliance_for_Life.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class AllocatedBudgets1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AllocatedBudgets1
        public ActionResult Index()
        {
            var allocatedBudget = db.AllocatedBudget.Include(a => a.Invoice).Include(a => a.Subcontractor);
            return View(allocatedBudget.ToList());
        }

        // GET: AllocatedBudgets1/Details/5
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

        // GET: AllocatedBudgets1/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceId", "OrgName");
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");
            return View();
        }

        // POST: AllocatedBudgets1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllocatedBudgetID,SubcontractorId,CycleEndAdjustments,InvoiceID,Year,AllocatedNewBudget,AllocatedOldBudget,AllocationAdjustedDate,Month")] AllocatedBudget allocatedBudget)
        {
            if (ModelState.IsValid)
            {
                allocatedBudget.AllocatedBudgetID = Guid.NewGuid();
                db.AllocatedBudget.Add(allocatedBudget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceId", "OrgName", allocatedBudget.InvoiceID);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "AdministratorId", allocatedBudget.SubcontractorId);
            return View(allocatedBudget);
        }

        // GET: AllocatedBudgets1/Edit/5
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
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceId", "OrgName", allocatedBudget.InvoiceID);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "AdministratorId", allocatedBudget.SubcontractorId);
            return View(allocatedBudget);
        }

        // POST: AllocatedBudgets1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllocatedBudgetID,SubcontractorId,CycleEndAdjustments,InvoiceID,Year,AllocatedNewBudget,AllocatedOldBudget,AllocationAdjustedDate,Month")] AllocatedBudget allocatedBudget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allocatedBudget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceId", "OrgName", allocatedBudget.InvoiceID);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "AdministratorId", allocatedBudget.SubcontractorId);
            return View(allocatedBudget);
        }

        // GET: AllocatedBudgets1/Delete/5
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

        // POST: AllocatedBudgets1/Delete/5
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
