﻿using Alliance_for_Life.Models;
using PagedList;
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
        public ActionResult Index(string sortOrder, string Year, string searchString, string currentFilter, int? page, string pgSize)
        {

            //variable that stores the current year
            var year_search = DateTime.Now.Year;

            int pageSize = Convert.ToInt16(pgSize);

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

            if ((Year != null) && Year.Length > 1)
            {
                year_search = Convert.ToInt16(Year);
            }

            var admincosts = db.AdminCosts;

            var allocatedBudget = db.AllocatedBudget
                .Include(a => a.Subcontractor)
                .Include(a => a.Invoice)
                .Include(a => a.AdminCost)
                .Where(a => a.Year == year_search);

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.ReportTitle = "Report Year -  " + year_search;
            ViewBag.yearselected = year_search;
            //checking to see if there are any data available


            //pulling admincost 
            var admincost = db.AdminCosts.Where(a => a.Year == year_search).ToList();

            //calculating Sub 3% Admin Fee per month and returning it as viewbag
            ViewBag.JanFee = (admincost.Where(a => a.Month == Months.January).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.FebFee = (admincost.Where(a => a.Month == Months.February).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.MarFee = (admincost.Where(a => a.Month == Months.March).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.AprFee = (admincost.Where(a => a.Month == Months.April).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.MayFee = (admincost.Where(a => a.Month == Months.May).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.JunFee = (admincost.Where(a => a.Month == Months.June).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.JulFee = (admincost.Where(a => a.Month == Months.July).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.AugFee = (admincost.Where(a => a.Month == Months.August).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.SepFee = (admincost.Where(a => a.Month == Months.September).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.OctFee = (admincost.Where(a => a.Month == Months.October).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.NovFee = (admincost.Where(a => a.Month == Months.November).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.DecFee = (admincost.Where(a => a.Month == Months.December).Sum(a => a.ATotCosts) * 0.03).ToString("C");
            ViewBag.TotalFee = (admincost.Sum(a => a.ATotCosts) * 0.03).ToString("C");

            // quarterly calculations
            var FirstQuarter = from qs in db.Invoices
                               where (int)qs.Month <= 3 && qs.Year == year_search
                               select qs.GrandTotal;

            if (FirstQuarter.Count() > 0)
            {
                ViewBag.FirstQuarter = FirstQuarter.Sum().ToString("C");
            }

            var SecondQuarter = from qs in db.Invoices
                               where (int)qs.Month >= 4 && (int)qs.Month <= 6  && qs.Year == year_search
                               select qs.GrandTotal;

            if (SecondQuarter.Count() > 0)
            {
                ViewBag.SecondQuarter = SecondQuarter.Sum().ToString("C");
            }






            if (allocatedBudget.Count() == 0)
            {
                ViewBag.error = "No Report available for the year " + year_search;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            int pageNumber = (page ?? 1);
            return View(allocatedBudget.OrderBy(y => y.Year).ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create([Bind(Include = "AllocatedBudgetId,SubcontractorId,CycleEndAdjustments,Year,AllocatedNewBudget,AllocatedOldBudget,AllocationAdjustedDate")] AllocatedBudget allocatedBudget)
        {
            if (ModelState.IsValid)
            {
                allocatedBudget.AllocatedBudgetId = Guid.NewGuid();
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
        public ActionResult Edit([Bind(Include = "AllocatedBudgetId,SubcontractorId,CycleEndAdjustments,Year,AllocatedNewBudget,AllocatedOldBudget,AllocationAdjustedDate")] AllocatedBudget allocatedBudget)
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
