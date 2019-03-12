using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using System;
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
            var invoices = db.Invoices.Include(a => a.Subcontractor);

            return View(invoices.ToList());
        }
        //needs work on
        public ActionResult Invoice(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoices = db.Invoices

                .Include(a => a.Subcontractor)
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
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");

            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,OrgName,Month,Region,Year,SubcontractorId,DirectAdminCost,ParticipantServices,GrandTotal,LessManagementFee,DepositAmount,BeginningAllocation,AdjustedAllocation,BillingDate,BalanceRemaining")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.Invoices
                                where
                                s.SubcontractorId == invoice.SubcontractorId &&
                                s.Year == invoice.Year &&
                                s.Region == invoice.Region &&
                                s.Month == invoice.Month
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    invoice.InvoiceId = Guid.NewGuid();
                    invoice.OrgName = User.Identity.Name;
                    invoice.SubmittedDate = System.DateTime.Now;
                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", invoice.SubcontractorId);

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
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", invoice.SubcontractorId);

            return View(invoice);
        }


        //Detail
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoices = db.Invoices

                .Include(a => a.Subcontractor)
                .SingleOrDefault(a => a.InvoiceId == id);

            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }
        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,OrgName,Month,Region,Year,SubcontractorId,DirectAdminCost,ParticipantServices,GrandTotal,LessManagementFee,DepositAmount,BeginningAllocation,AdjustedAllocation,BillingDate,BalanceRemaining")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.SubmittedDate = System.DateTime.Now;
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");

            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices

                .Include(a => a.Subcontractor)
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
        public ActionResult DeleteConfirmed(Guid id)
        {
            Invoice invoice = db.Invoices
                .Include(a => a.Month)

                .Include(a => a.Subcontractor)
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
