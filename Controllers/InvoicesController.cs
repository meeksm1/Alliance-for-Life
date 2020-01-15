﻿using Alliance_for_Life.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoices1
        public ActionResult Index(string sortOrder, string searchString, string SubcontractorId, string Month, string Year, string billingdate, string currentFilter, int? page, int? pgSize)
        {
            var datelist = Enumerable.Range(System.DateTime.Now.Year-1, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            var Subcontractors = db.SubContractors.ToList();
            var id = User.Identity.GetUserId();
            var usersubid = db.Users.Find(id).SubcontractorId;

            if (!User.IsInRole("Admin"))
            {
                Subcontractors = db.SubContractors.Where(s => s.SubcontractorId == usersubid).ToList();
            }


            ViewBag.SubcontractorId = new SelectList(Subcontractors, "SubcontractorId", "OrgName");
            ViewBag.Month = new SelectList(Enum.GetValues(typeof(Months)));
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
                        + " for " + Month + ", " + year + " already exists. Please find the invoice below";

                    searchString = db.SubContractors.Find(new Guid(SubcontractorId)).OrgName;
                }

                else
                {
                    GenerateInvoice(SubcontractorId, Month, year, billingdate);

                }

            }

            var invoices = db.Invoices.Include(i => i.AdminCosts).Include(i => i.AllocatedBudget).Include(i => i.ParticipationService).Include(i => i.Subcontractor);

            var yrs = DateTime.Now.Year;

            if (!String.IsNullOrEmpty(Year))
            {
                yrs = Convert.ToInt16(Year);
            }


            if (!User.IsInRole("Admin"))
            {
                invoices = from s in invoices
                           where usersubid == s.SubcontractorId
                           select s;
            }

            if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(Year))
            {
                var yearSearch = Convert.ToInt16(Year);

                invoices = from s in invoices
                           where ((int)s.Month < 7 && s.Year == yrs) || ((int)s.Month >= 7 && s.Year == yrs + 1) || s.Subcontractor.OrgName.Contains(searchString)
                           select s;
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


            invoices = invoices.OrderBy(a => a.Year).OrderBy(a => a.Month);
            return View(invoices.ToPagedList(pageNumber, defaSize));
        }

        public ActionResult Download(string encryptReportId)
        {
            //business login to bind this view
            //bind view using this encryptReportId
            return View();
        }

        //public ActionResult DownloadViewAsPDF(string encryptId)
        //{
        //    return new Rotativa.ActionAsPdf("Download", new { encryptReportId = encryptId }) { FileName = "Invoice.pdf" };
        //}

        //Generate Invoice
        public ActionResult GenerateInvoice(string orgname, string Month, int Year, string billingdate)
        {
            Invoices invoice = new Invoices();

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

            var allocatedbudget = db.AllocatedBudget
                .Where(s => s.SubcontractorId == invoice.SubcontractorId && s.Year == invoice.Year);

            //set the invoice Admin and Participation ID to Null
            invoice.AdminCostId = Guid.Empty;
            invoice.PSId = Guid.Empty;

            //set totals to zero
            invoice.DirectAdminCost = 0;
            invoice.ParticipantServices = 0;

            if ((admincost.Count() == 0) || (particost.Count() == 0))
            {
                ViewBag.error = "Invoice for " + db.SubContractors.Find(new Guid(orgname)).OrgName
                         + " for " + Month + ", " + Year + " cannot be generated. Admin cost or Participation cost does not exist.";
                ModelState.Clear();
            }
            else if (allocatedbudget.Count() == 0)
            {
                ViewBag.error = "Invoice for " + db.SubContractors.Find(new Guid(orgname)).OrgName
                        + " for " + Year + " cannot be generated. Budget is not allocated for the given year.";
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

                invoice.LessManagementFee = invoice.DirectAdminCost * 0.03;

                //grand total
                invoice.GrandTotal = invoice.DirectAdminCost + invoice.ParticipantServices;

                //lessmanagementFee

                invoice.DepositAmount = invoice.GrandTotal - invoice.LessManagementFee;

                //getting contractor Allocated amount
                var subcontractorbalance = db.SubContractors
                    .Where(s => s.SubcontractorId == invoice.SubcontractorId);

                //calculating the Balance Remaining
                //check and getting the balance remaining 

                //*******************************************************************************************************************
                //converting month to integer for comparing from the table
                var month_TO = (int)Enum.Parse(typeof(Months), Month);

                var begbalance = from s in db.Invoices
                                 where s.Year == Year || s.Year == Year + 1 || s.Year == Year - 1 && s.SubcontractorId == invoice.SubcontractorId
                                 select s;

                //*******************************************************************************************************************
                //Finding the balance remaining before the invoice is created
                var balanceRemaining = allocatedbudget.Where(a => a.Year == Year && a.SubcontractorId == invoice.SubcontractorId).FirstOrDefault().AllocatedOldBudget;
                var i = 1;

                foreach (var items in begbalance.Where(a => a.Year == Year && a.SubcontractorId == invoice.SubcontractorId))
                {

                    if ((int)items.Month <= month_TO - i)
                    {
                        balanceRemaining = items.BalanceRemaining;
                    }
                    i++;
                }

                if (month_TO == 7)
                {

                    foreach (var items in begbalance.Where(a => a.Year == Year - 1 && a.SubcontractorId == invoice.SubcontractorId))
                    {
                        if ((int)items.Month <= month_TO - i)
                        {
                            balanceRemaining = items.BalanceRemaining;
                        }
                       ;
                    }
                }
                else if (month_TO > 7)
                {
                    foreach (var items in begbalance.Where(a => a.Year == Year + 1 && a.SubcontractorId == invoice.SubcontractorId))
                    {
                        if ((int)items.Month <= month_TO - i)
                        {
                            balanceRemaining = items.BalanceRemaining;
                        }
                       ;
                    }
                }



                //calculating the rest

                invoice.Region = subcontractorbalance.FirstOrDefault().Region;
                invoice.BillingDate = DateTime.Parse(billingdate);
                invoice.SubmittedDate = DateTime.Now;
                invoice.OrgName = db.SubContractors.Find(invoice.SubcontractorId).OrgName;
                invoice.AllocatedBudgetId = allocatedbudget.FirstOrDefault().AllocatedBudgetId;



                if (month_TO >= 7)
                {
                    var allocationupdated = from s in db.AllocatedBudget
                                            where s.Year == (Year - 1) && s.SubcontractorId == invoice.SubcontractorId
                                            select s.AllocatedNewBudget;

                    balanceRemaining = balanceRemaining + allocationupdated.SingleOrDefault();

                }
                else
                {
                    var allocationupdated = from s in db.AllocatedBudget
                                            where s.Year == (Year) && s.SubcontractorId == invoice.SubcontractorId
                                            select s.AllocatedNewBudget;

                    balanceRemaining = balanceRemaining + allocationupdated.SingleOrDefault();


                }


                //calculating the rest
                invoice.BalanceRemaining = balanceRemaining - begbalance.Sum(a => a.GrandTotal);

                //add to the Invoice table and save data
                db.Invoices.Add(invoice);
                db.SaveChanges();
            }

            //check if the data exists
            ModelState.Clear();
            invoice.Year = DateTime.Now.Year;
            return View("Index");
        }

        //update invoice
        [HttpPost]
        public ActionResult UpdateInvoice()
        {
            var id = new Guid(Request["InvoiceId"]);

            var invoice = db.Invoices.Find(id);

            invoice.SubmittedDate = System.DateTime.Now;

            var admincost = db.AdminCosts
                            .Where(s => s.SubcontractorId == invoice.SubcontractorId &&
                             s.Year == invoice.Year &&
                             s.Month == invoice.Month);

            //getting participation total
            var particost = db.ParticipationServices
                .Where(s => s.SubcontractorId == invoice.SubcontractorId && s.Year == invoice.Year && s.Month == invoice.Month);

            //allocation budget
            var allocatedbudget = db.AllocatedBudget
              .Where(s => s.SubcontractorId == invoice.SubcontractorId && s.Year == invoice.Year);

            //set totals to zero
            invoice.DirectAdminCost = 0;
            invoice.ParticipantServices = 0;

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
            //set totals to zero


            invoice.GrandTotal = invoice.DirectAdminCost + invoice.ParticipantServices;
            invoice.LessManagementFee = invoice.DirectAdminCost * .03;
            invoice.DepositAmount = invoice.GrandTotal - invoice.LessManagementFee;

            //calculating balance remaining
            var begbalance = from s in db.Invoices
                             where s.Year == invoice.Year || s.Year == invoice.Year + 1 || s.Year == invoice.Year - 1 && s.SubcontractorId == invoice.SubcontractorId
                             select s;

            var balanceRemaining = allocatedbudget.Where(a => a.Year == invoice.Year).FirstOrDefault().AllocatedOldBudget + invoice.AllocatedBudget.AllocatedNewBudget;
            var i = 1;

            foreach (var items in begbalance.Where(a => a.Year == invoice.Year && a.SubcontractorId == invoice.SubcontractorId).OrderBy(b => b.Year).OrderBy(c => c.Month))
            {

                if ((int)items.Month <= (int)invoice.Month - i)
                {
                    balanceRemaining = items.BalanceRemaining;
                }
                i++;
            }

            if ((int)invoice.Month == 7)
            {

                foreach (var items in begbalance.Where(a => a.Year == invoice.Year - 1 && a.SubcontractorId == invoice.SubcontractorId).OrderBy(b => b.Year).OrderBy(c => c.Month))
                {
                    if ((int)items.Month < (int)invoice.Month)
                    {
                        balanceRemaining = items.BalanceRemaining;
                    }
                }
            }
            else if ((int)invoice.Month > 7)
            {
                foreach (var items in begbalance.Where(a => a.Year == invoice.Year + 1 && a.SubcontractorId == invoice.SubcontractorId).OrderBy(b => b.Year).OrderBy(c => c.Month))
                {
                    if ((int)items.Month <= (int)invoice.Month - i)
                    {
                        balanceRemaining = items.BalanceRemaining;
                    }
                }
            }


            invoice.BalanceRemaining = balanceRemaining - invoice.GrandTotal;

            // calculating the rest
            invoice.OrgName = db.SubContractors.Find(invoice.SubcontractorId).OrgName;
            db.Entry(invoice).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //print invoice
        public ActionResult Invoice(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices
                .Include(a => a.Subcontractor)
                .Include(i => i.AdminCosts).Include(i => i.AllocatedBudget)
                .Include(i => i.ParticipationService)
                .SingleOrDefault(a => a.InvoiceId == id);

            if (invoices.Month != null && (int)invoices.Month >= 7)
            {
                var allocation = from s in db.AllocatedBudget
                                 where s.Year == invoices.Year - 1 && s.SubcontractorId == invoices.SubcontractorId
                                 select s;

                ViewBag.AllocatedOldBudget = allocation.FirstOrDefault().AllocatedOldBudget;
                ViewBag.AllocatedAdjustments = allocation.FirstOrDefault().AllocatedNewBudget;
            }
            else
            {
                var allocation = from s in db.AllocatedBudget
                                 where s.Year == invoices.Year && s.SubcontractorId == invoices.SubcontractorId
                                 select s;

                ViewBag.AllocatedOldBudget = allocation.FirstOrDefault().AllocatedOldBudget;
                ViewBag.AllocatedAdjustments = allocation.FirstOrDefault().AllocatedNewBudget;
            }

            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }
        // GET: Invoices1/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }




        // GET: Invoices1/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }

        // POST: Invoices1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Invoices invoices = db.Invoices.Find(id);
            db.Invoices.Remove(invoices);
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
