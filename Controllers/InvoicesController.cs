﻿using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using PagedList;
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
        // GET: Invoices
        public ActionResult Index(string sortOrder, string searchString, string SubcontractorId, string Month, string Year, string billingdate, string currentFilter, int? page, string pgSize)
        {
            int pageSize = Convert.ToInt16(pgSize);
            //paged view
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            //generate invoices 
            if (SubcontractorId != null && Month != null && Year != null && billingdate != null)
            {
                var year = Convert.ToInt16(Year);
                var mon = (Months)Enum.Parse(typeof(Months), Month);
                var dataexist = from s in db.Invoices
                                where
                                s.SubcontractorId == new Guid(SubcontractorId) &&
                                s.Year == year &&
                                s.Month == mon
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Invoice for " + db.SubContractors.Find(new Guid(SubcontractorId)).OrgName
                        + "for " + Month + ", " + year + " already exists. Please find the invoice below";

                    searchString = db.SubContractors.Find(new Guid(SubcontractorId)).OrgName;
                    ModelState.Clear();
                }
                else
                {
                    GenerateInvoice(SubcontractorId, Month, Year, billingdate);
                }
            }
            //return the index



            var invoices = db.Invoices.Include(s => s.Subcontractor);

            if (!String.IsNullOrEmpty(searchString))
            {
                invoices = invoices.Where(a => a.Subcontractor.OrgName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    invoices = invoices.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                default:
                    invoices = invoices.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");
            ViewBag.Month = new SelectList(Enum.GetValues(typeof(Months)));
            //ModelState.Clear();

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            int pageNumber = (page ?? 1);
            return View(invoices.ToPagedList(pageNumber, pageSize));
            // return View(invoices.ToList());
        }

        //generate invoice function
        public ActionResult GenerateInvoice(string orgname, string Month, string Year, string billingdate)
        {
            Invoice invoice = new Invoice();

            invoice.InvoiceId = Guid.NewGuid();
            invoice.SubcontractorId = new Guid(orgname);
            invoice.Month = (Months)Enum.Parse(typeof(Months), Month);
            invoice.Year = Convert.ToInt16(Year);

            //getting admin total
            var admincost = db.AdminCosts
                .Where(s => s.SubcontractorId == invoice.SubcontractorId && s.Year == invoice.Year && s.Month == invoice.Month);


            //getting participation total
            var particost = db.ParticipationServices
                .Where(s => s.SubcontractorId == invoice.SubcontractorId && s.Year == invoice.Year && s.Month == invoice.Month);


            //set totals to zero
            invoice.DirectAdminCost = 0;
            invoice.ParticipantServices = 0;


            //check if both admincost and participation cost does not exists
            if ((admincost.Count() == 0) && (particost.Count() == 0))
            {
                ViewBag.error = "Invoice for " + db.SubContractors.Find(new Guid(orgname)).OrgName
                         + " for " + Month + ", " + Year + " cannot be generates. Admin cost or Participation cost does not exist.";
                ModelState.Clear();
            }
            else
            {
                if (admincost.Count() != 0)
                {
                    invoice.DirectAdminCost = admincost.FirstOrDefault().ATotCosts;
                    invoice.AdminCostId = admincost.FirstOrDefault().AdminCostId;
                }

                if (particost.Count() != 0)
                {
                    invoice.ParticipantServices = particost.FirstOrDefault().PTotals;
                    invoice.PSId = particost.FirstOrDefault().PSId;
                }

                //grand total
                invoice.GrandTotal = invoice.DirectAdminCost + invoice.ParticipantServices;

                //lessmanagementFee
                invoice.LessManagementFee = invoice.GrandTotal * .03;
                invoice.DepositAmount = invoice.GrandTotal - invoice.LessManagementFee;

                //getting contractor Allocated amount
                var subcontractorbalance = db.SubContractors
                    .Where(s => s.SubcontractorId == invoice.SubcontractorId);

                invoice.BeginningAllocation = subcontractorbalance.FirstOrDefault().AllocatedContractAmount;
                invoice.AdjustedAllocation = subcontractorbalance.FirstOrDefault().AllocatedAdjustments;

                //calculating the rest
                invoice.BalanceRemaining = invoice.BeginningAllocation - invoice.AdjustedAllocation;
                invoice.Region = subcontractorbalance.FirstOrDefault().Region;
                invoice.BillingDate = DateTime.Parse(billingdate);

                //filling in the tables
                invoice.OrgName = db.SubContractors.Find(invoice.SubcontractorId).OrgName;
                invoice.SubcontractorId = invoice.SubcontractorId;
                invoice.SubmittedDate = DateTime.Now;

                //add to the Invoice table and save data
                db.Invoices.Add(invoice);
                db.SaveChanges();
            }

            //check if the data exists
            ModelState.Clear();
            return View();
        }
        //needs work on

        [HttpPost]
        public ActionResult UpdateInvoice()
        {
            var id = new Guid(Request["InvoiceId"]);

            var invoice = db.Invoices.Find(id);

            invoice.SubmittedDate = System.DateTime.Now;

            var admincost = db.AdminCosts
    .Where(s => s.SubcontractorId == invoice.SubcontractorId && s.Year == invoice.Year && s.Month == invoice.Month);

            //getting participation total
            var particost = db.ParticipationServices
                .Where(s => s.SubcontractorId == invoice.SubcontractorId && s.Year == invoice.Year && s.Month == invoice.Month);


            //set totals to zero
            invoice.DirectAdminCost = 0;
            invoice.ParticipantServices = 0;
            invoice.DirectAdminCost = admincost.FirstOrDefault().ATotCosts;
            invoice.ParticipantServices = particost.FirstOrDefault().PTotals;
            invoice.GrandTotal = invoice.DirectAdminCost + invoice.ParticipantServices;
            invoice.LessManagementFee = invoice.GrandTotal * .03;
            invoice.DepositAmount = invoice.GrandTotal - invoice.LessManagementFee;

            //getting contractor Allocated amount
            var subcontractorbalance = db.SubContractors
                .Where(s => s.SubcontractorId == invoice.SubcontractorId);

            invoice.BeginningAllocation = subcontractorbalance.FirstOrDefault().AllocatedContractAmount;
            invoice.AdjustedAllocation = subcontractorbalance.FirstOrDefault().AllocatedAdjustments;

            //calculating the rest
            invoice.BalanceRemaining = invoice.BeginningAllocation - invoice.AdjustedAllocation;

            db.Entry(invoice).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }


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
