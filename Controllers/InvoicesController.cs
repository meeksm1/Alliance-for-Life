using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.ReportControllers
{
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(a => a.Month).Include(a => a.Region).Include(a => a.Subcontractor)
                .Include(a => a.AdminCosts).Include(a => a.ParticipationService);

            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoices = db.Invoices
                .Include(a => a.Month)
                .Include(a => a.Region)
                .Include(a => a.Subcontractor)
                .Include(a => a.AdminCosts)
                .Include(a => a.ParticipationService)
                .SingleOrDefault(a => a.InvoiceId == id);

            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions");
            ViewBag.YearId = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");
            ViewBag.AdminCostId = new SelectList(db.AdminCosts, "AdminCostId", "AdminCostId");
            ViewBag.PartServId = new SelectList(db.ParticipationServices, "PSId", "PSId");

            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,OrgName,MonthId,RegionId,YearId,SubcontractorId,AdminCostId,PartServId,DirectAdminCost,ParticipantServices,GrandTotal,LessManagementFee,DepositAmount,BeginningAllocation,AdjustedAllocation,BillingDate,BalanceRemaining")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.OrgName = User.Identity.Name;
                invoice.SubmittedDate = System.DateTime.Now;
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", invoice.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", invoice.RegionId);
            ViewBag.YearId = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", invoice.SubcontractorId);
            ViewBag.AdminCostId = new SelectList(db.AdminCosts, "AdminCostId", "AdminCostId");
            ViewBag.PartServId = new SelectList(db.ParticipationServices, "PSId", "PSId");


            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);

            if (invoice == null)
            {
                return HttpNotFound();
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", invoice.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", invoice.RegionId);
            ViewBag.YearId = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", invoice.SubcontractorId);
            ViewBag.AdminCostId = new SelectList(db.AdminCosts, "AdminCostId", "AdminCosts");
            ViewBag.PartServId = new SelectList(db.ParticipationServices, "PSId", "PSId");


            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,OrgName,MonthId,RegionId,YearId,SubcontractorId,AdminCostId,PartServId,DirectAdminCost,ParticipantServices,GrandTotal,LessManagementFee,DepositAmount,BeginningAllocation,AdjustedAllocation,BillingDate,BalanceRemaining")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.SubmittedDate = System.DateTime.Now;
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions");
            ViewBag.YearId = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");
            ViewBag.AdminCostId = new SelectList(db.AdminCosts, "AdminCostId", "AdminCostId");
            ViewBag.PartServId = new SelectList(db.ParticipationServices, "PSId", "PSId");

            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices
                .Include(a => a.Month)
                .Include(a => a.Region)
                .Include(a => a.Subcontractor)
                .Include(a => a.AdminCosts)
                .Include(a => a.ParticipationService)
                .SingleOrDefault(a => a.InvoiceId == id);

            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices
                .Include(a => a.Month)
                .Include(a => a.Region)
                .Include(a => a.Subcontractor)
                .Include(a => a.AdminCosts)
                .Include(a => a.ParticipationService)
                .SingleOrDefault(a => a.InvoiceId == id);

            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [WordDocument]
        public ActionResult Print()
        {
            ViewBag.WordDocumentFilename = "Invoice";
            return View("Details");
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
