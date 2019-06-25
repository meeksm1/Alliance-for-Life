using Alliance_for_Life.Models;
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

            //Starting Deposit Balance
            var beginingBalance = 0;
            ViewBag.BegBal = beginingBalance;
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

            // quarterly calculations
            QuaterlyViewBag(year_search);

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

        public ActionResult Balance(string sortOrder, string Year, /*string searchString, string currentFilter, */int? page, string pgSize)
        {
            //variable that stores the current year
            var year_search = DateTime.Now.Year;
            int pageSize = Convert.ToInt16(pgSize);



            if ((Year != null) && Year.Length > 1)
            {
                year_search = Convert.ToInt16(Year);
            }

            var allocatedBudget = db.AllocatedBudget
                .Include(a => a.Subcontractor)
                .Include(a => a.Invoice)
                .Include(a => a.AdminCost)
                .Where(a => a.Year == year_search);

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.ReportTitle = "Report Year -  " + year_search;
            ViewBag.yearselected = year_search;

            //calling main function
            beginingbalance(year_search);

            //CREATING THE VIEWBAG TO PULL afl ALLOCATED BUDGET
            ViewBag.aflallocation = db.AFLAllocation.Where(a => a.Year == year_search).ToList();

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

        //calculate quaterly
        public void QuaterlyViewBag(int year_search)
        {
            var FirstQuarter = from qs in db.Invoices
                               where (int)qs.Month <= 3 && qs.Year == year_search
                               select qs.GrandTotal;

            if (FirstQuarter.Count() > 0)
            {
                ViewBag.FirstQuarter = FirstQuarter.Sum().ToString("C");
            }

            var SecondQuarter = from qs in db.Invoices
                                where (int)qs.Month >= 4 && (int)qs.Month <= 6 && qs.Year == year_search
                                select qs.GrandTotal;

            if (SecondQuarter.Count() > 0)
            {
                ViewBag.SecondQuarter = SecondQuarter.Sum().ToString("C");
            }

            var ThirdQuarter = from qs in db.Invoices
                               where (int)qs.Month >= 7 && (int)qs.Month <= 9 && qs.Year == year_search
                               select qs.GrandTotal;

            if (ThirdQuarter.Count() > 0)
            {
                ViewBag.ThirdQuarter = ThirdQuarter.Sum().ToString("C");
            }



            var FourthQuarter = from qs in db.Invoices
                                where (int)qs.Month >= 10 && qs.Year == year_search
                                select qs.GrandTotal;

            if (FourthQuarter.Count() > 0)
            {
                ViewBag.FourthQuarter = FourthQuarter.Sum().ToString("C");
            }

            var QuarterTotals = from qs in db.Invoices
                                where (int)qs.Month <= 12 && qs.Year == year_search
                                select qs.GrandTotal;
            if (QuarterTotals.Count() > 0)
            {
                ViewBag.QuarterTotals = QuarterTotals.Sum().ToString("C");

            }

        }

        public void beginingbalance(int year_search)
        {
            //initializing the viewbag to zero
            ViewBag.DepoJan = 0;
            ViewBag.DepoFeb = 0;
            ViewBag.DepoMar = 0;
            ViewBag.DepoApr = 0;
            ViewBag.DepoMay = 0;
            ViewBag.DepoJun = 0;
            ViewBag.DepoJul = 0;
            ViewBag.DepoAug = 0;
            ViewBag.DepoSep = 0;
            ViewBag.DepoOct = 0;
            ViewBag.DepoNov = 0;
            ViewBag.DepoDec = 0;


            //statedeposite loop

            var statedepo = db.StateDeposit.Where(b => b.Year == year_search);
            if (statedepo.Count() != 0)
            {
                if (statedepo.Where(a => a.Month == Months.January).Count() != 0)
                {
                    ViewBag.DepoJan = statedepo.Where(a => a.Month == Months.January).FirstOrDefault().StateDeposits;
                }
                if (statedepo.Where(a => a.Month == Months.February).Count() != 0)
                {
                    ViewBag.DepoFeb = statedepo.Where(a => a.Month == Months.February).FirstOrDefault().StateDeposits;
                }
                if (statedepo.Where(a => a.Month == Months.March).Count() != 0)
                {
                    ViewBag.DepoMar = statedepo.Where(a => a.Month == Months.March).FirstOrDefault().StateDeposits;

                }
                if (statedepo.Where(a => a.Month == Months.April).Count() != 0)
                {

                    ViewBag.DepoApr = statedepo.Where(a => a.Month == Months.April).FirstOrDefault().StateDeposits;
                }
                if (statedepo.Where(a => a.Month == Months.May).Count() != 0)
                {
                    ViewBag.DepoMay = statedepo.Where(a => a.Month == Months.May).FirstOrDefault().StateDeposits;

                }
                if (statedepo.Where(a => a.Month == Months.June).Count() != 0)
                {

                    ViewBag.DepoJun = statedepo.Where(a => a.Month == Months.June).FirstOrDefault().StateDeposits;
                }
                if (statedepo.Where(a => a.Month == Months.July).Count() != 0)
                {

                    ViewBag.DepoJul = statedepo.Where(a => a.Month == Months.July).FirstOrDefault().StateDeposits;
                }
                if (statedepo.Where(a => a.Month == Months.August).Count() != 0)
                {

                    ViewBag.DepoAug = statedepo.Where(a => a.Month == Months.August).FirstOrDefault().StateDeposits;
                }
                if (statedepo.Where(a => a.Month == Months.September).Count() != 0)
                {
                    ViewBag.DepoSep = statedepo.Where(a => a.Month == Months.September).FirstOrDefault().StateDeposits;

                }
                if (statedepo.Where(a => a.Month == Months.October).Count() != 0)
                {
                    ViewBag.DepoOct = statedepo.Where(a => a.Month == Months.October).FirstOrDefault().StateDeposits;

                }
                if (statedepo.Where(a => a.Month == Months.November).Count() != 0)
                {
                    ViewBag.DepoNov = statedepo.Where(a => a.Month == Months.November).FirstOrDefault().StateDeposits;

                }
                if (statedepo.Where(a => a.Month == Months.December).Count() != 0)
                {
                    ViewBag.DepoDec = statedepo.Where(a => a.Month == Months.December).FirstOrDefault().StateDeposits;

                }

                //total state deposit
                ViewBag.StateDeposit = statedepo.Sum(a => a.StateDeposits).ToString("C");
            }

            var admincost = db.AdminCosts.Where(a => a.Year == year_search).ToList();

            //calculating Admin Total Cost per month and returning it as viewbag
            ViewBag.JanFee = (admincost.Where(a => a.Month == Months.January && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.FebFee = (admincost.Where(a => a.Month == Months.February && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.MarFee = (admincost.Where(a => a.Month == Months.March && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.AprFee = (admincost.Where(a => a.Month == Months.April && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.MayFee = (admincost.Where(a => a.Month == Months.May && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.JunFee = (admincost.Where(a => a.Month == Months.June && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.JulFee = (admincost.Where(a => a.Month == Months.July && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.AugFee = (admincost.Where(a => a.Month == Months.August && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.SepFee = (admincost.Where(a => a.Month == Months.September && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.OctFee = (admincost.Where(a => a.Month == Months.October && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.NovFee = (admincost.Where(a => a.Month == Months.November && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.DecFee = (admincost.Where(a => a.Month == Months.December && a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));
            ViewBag.TotalFee = (admincost.Where(a => a.SubcontractorId.ToString() == "1d8212d8-7eb9-46b1-a6b2-bb26f08b6d87").Sum(a => a.ATotCosts));

            //calculating other studd
            var invoice = db.Invoices.Where(a => a.Year == year_search);

            //for July
            ViewBag.JulTot = ViewBag.DepoJul ?? 0;
            ViewBag.JulInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.July).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.JulFee * .13), 2);
            ViewBag.JulRem = Math.Round((ViewBag.JulTot - ViewBag.JulInv), 2);

            //for august
            ViewBag.AugBeg = ViewBag.JulRem;
            ViewBag.AugTot = Math.Round((ViewBag.AugBeg + ViewBag.DepoAug), 2);
            ViewBag.AugInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.August).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.AugFee * .13), 2);
            ViewBag.AugRem = Math.Round((ViewBag.AugTot - ViewBag.AugInv), 2);

            //for september
            ViewBag.SepBeg = ViewBag.AugRem;
            ViewBag.SepTot = Math.Round((ViewBag.SepBeg + ViewBag.DepoSep), 2);
            ViewBag.SepInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.September).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.SepFee * .13), 2);
            ViewBag.SepRem = Math.Round((ViewBag.SepTot - ViewBag.SepInv), 2);
            //for october
            ViewBag.OctBeg = ViewBag.SepRem;
            ViewBag.OctTot = Math.Round((ViewBag.OctBeg + ViewBag.DepoOct), 2);
            ViewBag.OctInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.October).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.OctFee * .13), 2);
            ViewBag.OctRem = Math.Round((ViewBag.OctTot - ViewBag.OctInv), 2);
            //for november
            ViewBag.NovBeg = ViewBag.OctRem;
            ViewBag.NovTot = Math.Round((ViewBag.NovBeg + ViewBag.DepoNov), 2);
            ViewBag.NovInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.November).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.NovFee * .13), 2);
            ViewBag.NovRem = Math.Round((ViewBag.NovTot - ViewBag.NovInv), 2);
            //for december
            ViewBag.DecBeg = ViewBag.NovRem;
            ViewBag.DecTot = Math.Round((ViewBag.DecBeg + ViewBag.DepoDec), 2);
            ViewBag.DecInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.December).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.DecFee * .13), 2);
            ViewBag.DecRem = Math.Round((ViewBag.DecTot - ViewBag.DecInv), 2);
            //for january
            ViewBag.JanBeg = ViewBag.DecRem;
            ViewBag.JanTot = Math.Round((ViewBag.JanBeg + ViewBag.DepoJan), 2);
            ViewBag.JanInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.January).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.JanFee * .13), 2);
            ViewBag.JanRem = Math.Round((ViewBag.JanTot - ViewBag.JanInv), 2);
            //for february
            ViewBag.FebBeg = ViewBag.JanRem;
            ViewBag.FebTot = Math.Round((ViewBag.FebBeg + ViewBag.DepoFeb), 2);
            ViewBag.FebInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.February).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.FebFee * .13), 2);
            ViewBag.FebRem = Math.Round((ViewBag.FebTot - ViewBag.FebInv), 2);
            //for march
            ViewBag.MarBeg = ViewBag.FebRem;
            ViewBag.MarTot = Math.Round((ViewBag.MarBeg + ViewBag.DepoMar), 2);
            ViewBag.MarInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.March).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.MarFee * .13), 2);
            ViewBag.MarRem = Math.Round((ViewBag.MarTot - ViewBag.MarInv), 2);
            //for april
            ViewBag.AprBeg = ViewBag.MarRem;
            ViewBag.AprTot = Math.Round((ViewBag.AprBeg + ViewBag.DepoApr), 2);
            ViewBag.AprInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.April).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.AprFee * .13), 2);
            ViewBag.AprRem = Math.Round((ViewBag.AprTot - ViewBag.AprInv), 2);
            //for may
            ViewBag.MayBeg = ViewBag.AprRem;
            ViewBag.MayTot = Math.Round((ViewBag.MayBeg + ViewBag.DepoMay), 2);
            ViewBag.MayInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.May).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.MayFee * .13), 2);
            ViewBag.MayRem = Math.Round((ViewBag.MayTot - ViewBag.MayInv), 2);
            //for june
            ViewBag.JunBeg = ViewBag.MayRem;
            ViewBag.JunTot = Math.Round((ViewBag.JunBeg + ViewBag.DepoJun), 2);
            ViewBag.JunInv = Math.Round(invoice.Where(k => k.Month == Alliance_for_Life.Models.Months.June).Select(b => b.GrandTotal).DefaultIfEmpty(0).Sum() + (ViewBag.JunFee * .13), 2);
            ViewBag.JunRem = Math.Round((ViewBag.JunTot - ViewBag.JunInv), 2);

            // Totals
            var totalcost = db.StateDeposit.Where(a => a.Year == year_search).ToList();
            ViewBag.TotalCost = (totalcost.Sum(a => a.StateDeposits));
            ViewBag.Total = Math.Round((ViewBag.JulTot + ViewBag.AugTot + ViewBag.SepTot + ViewBag.OctTot + ViewBag.NovTot + ViewBag.DecTot + ViewBag.JanTot + ViewBag.FebTot + ViewBag.MarTot + ViewBag.AprTot + ViewBag.MayTot + ViewBag.JunTot), 2);

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
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
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
                var dataexist = from s in db.AllocatedBudget.ToList()
                                where
                                s.SubcontractorId == allocatedBudget.SubcontractorId &&
                                s.Year == allocatedBudget.Year
                                select s;

                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Allocation Budget for " + db.SubContractors.Find(allocatedBudget.SubcontractorId).OrgName
                        + " for " + allocatedBudget.Year + " already exists.";

                    ModelState.Clear();
                }
                else
                {
                    allocatedBudget.AllocatedBudgetId = Guid.NewGuid();
                    db.AllocatedBudget.Add(allocatedBudget);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
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
