using Alliance_for_Life.Models;
using ClosedXML.Excel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class BudgetCostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BudgetCosts
        public ActionResult Index()
        {
            var budgetCosts = db.BudgetCosts.Include(b => b.Month).Include(b => b.Year).Include(b => b.Region);
            return View(budgetCosts.ToList());
        }

        public ActionResult AExpenseVSBExpense()
        {
            var expensereport = db.BudgetCosts
                .Include(b => b.AdminCost)
                .Include(b => b.ParticipationCost);

            return View(expensereport.ToList());
        }

        public ActionResult FirstQuarter()
        {
                var budgetCosts = db.BudgetCosts
                .Include(b => b.Month)
                .Include(b => b.Region)
                .Include(b => b.User)
                .Where(b => b.MonthId <= 3);

                return View(budgetCosts.ToList());    
        }

        public ActionResult SecondQuarter()
        {
            var budgetCosts = db.BudgetCosts
            .Include(b => b.Month)
            .Include(b => b.Region)
            .Include(b => b.User)
            .Where(b => b.MonthId >= 4 && b.MonthId <= 6);
            return View(budgetCosts.ToList());
        }

        public ActionResult ThirdQuarter()
        {
            var budgetCosts = db.BudgetCosts
            .Include(b => b.Month)
            .Include(b => b.Region)
            .Include(b => b.User)
            .Where(b => b.MonthId >= 7 && b.MonthId <= 9);
            return View(budgetCosts.ToList());
        }

        public ActionResult FourthQuarter()
        {
            var budgetCosts = db.BudgetCosts
            .Include(b => b.Month)
            .Include(b => b.Region)
            .Include(b => b.User)
            .Where(b => b.MonthId >= 10);
            return View(budgetCosts.ToList());
        }

        // GET: BudgetCosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetCosts budgetCosts = db.BudgetCosts
                .Include(a => a.Month)
                .Include(a => a.Region)
                .Include(a => a.Year)
                .SingleOrDefault(a => a.BudgetInvoiceId == id);
            if (budgetCosts == null)
            {
                return HttpNotFound();
            }
            return View(budgetCosts);
        }

        // GET: BudgetCosts/Create
        public ActionResult Create()
        {
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months");
            ViewBag.YearId = new SelectList(db.Years, "Id", "Years");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions");
            return View();
        }

        // POST: BudgetCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetInvoiceId,ASalandWages,AEmpBenefits,AEmpTravel,AEmpTraining,AOfficeRent,AOfficeUtilities,AFacilityIns,AOfficeSupplies,AEquipment,AOfficeCommunications,AOfficeMaint,AConsulting,SubConPayCost,BackgrounCheck,Other,AJanitorServices,ADepreciation,ATechSupport,ASecurityServices,ATotCosts,AdminFee,Trasportation,JobTraining,TuitionAssistance,ContractedResidential,UtilityAssistance,EmergencyShelter,HousingAssistance,Childcare,Clothing,Food,Supplies,RFO,BTotal,Maxtot,RegionId,MonthId,YearId")] BudgetCosts budgetCosts)
        {
            if (ModelState.IsValid)
            {
                db.BudgetCosts.Add(budgetCosts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", budgetCosts.MonthId);
            ViewBag.YearId = new SelectList(db.Years, "Id", "Years", budgetCosts.YearId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", budgetCosts.RegionId);
            return View(budgetCosts);
        }

        // GET: BudgetCosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetCosts budgetCosts = db.BudgetCosts.Find(id);
            if (budgetCosts == null)
            {
                return HttpNotFound();
            }
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", budgetCosts.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", budgetCosts.RegionId);
            ViewBag.YearId = new SelectList(db.Years, "Id", "Years", budgetCosts.YearId);
            return View(budgetCosts);
        }

        // POST: BudgetCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetInvoiceId,ASalandWages,AEmpBenefits,AEmpTravel,AEmpTraining,AOfficeRent,AOfficeUtilities,AFacilityIns,AOfficeSupplies,AEquipment,AOfficeCommunications,AOfficeMaint,AConsulting,SubConPayCost,BackgrounCheck,Other,AJanitorServices,ADepreciation,ATechSupport,ASecurityServices,ATotCosts,AdminFee,Trasportation,JobTraining,TuitionAssistance,ContractedResidential,UtilityAssistance,EmergencyShelter,HousingAssistance,Childcare,Clothing,Food,Supplies,RFO,BTotal,Maxtot,RegionId,MonthId,YearId")] BudgetCosts budgetCosts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budgetCosts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", budgetCosts.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", budgetCosts.RegionId);
            ViewBag.YearId = new SelectList(db.Years, "Id", "Years", budgetCosts.YearId);
            return View(budgetCosts);
        }

        // GET: BudgetCosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetCosts budgetCosts = db.BudgetCosts
                .Include(a => a.Month)
                .Include(a => a.Region)
                .Include(a => a.Year)
                .SingleOrDefault(a => a.BudgetInvoiceId == id);

            if (budgetCosts == null)
            {
                return HttpNotFound();
            }
            return View(budgetCosts);
        }

        // POST: BudgetCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BudgetCosts budgetCosts = db.BudgetCosts
                .Include(a => a.Month)
                .Include(a => a.Region)
                .Include(a => a.Year)
                .SingleOrDefault(a => a.BudgetInvoiceId == id);

            db.BudgetCosts.Remove(budgetCosts);
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

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[38]
            {
                new DataColumn ("Budget Report Id"),
                new DataColumn ("Month"),
                new DataColumn ("Region"),
                new DataColumn ("Salary/Wages"),
                new DataColumn ("Employee Benefits"),
                new DataColumn ("Employee Travel"),
                new DataColumn ("Employee Training"),
                new DataColumn ("Office Rent"),
                new DataColumn ("Office Utilities"),
                new DataColumn ("Facility Insurance"),
                new DataColumn ("Office Supplies"),
                new DataColumn ("Equipment"),
                new DataColumn ("Office Communications"),
                new DataColumn ("Office Maintenance"),
                new DataColumn ("Consulting Fees"),
                new DataColumn ("EFT Fees"),
                new DataColumn ("Background Check"),
                new DataColumn ("Other"),
                new DataColumn ("Janitorial Services"),
                new DataColumn ("Depreciation"),
                new DataColumn ("Technical Support"),
                new DataColumn ("Security Services"),
                new DataColumn ("Admininstration Totals"),
                new DataColumn ("Administration Fee"),
                new DataColumn ("Transportation"),
                new DataColumn ("Job Training"),
                new DataColumn ("Tuition Assistance"),
                new DataColumn ("Contracted Residential Care"),
                new DataColumn ("Utility Assistance"),
                new DataColumn ("Emergency Shelter"),
                new DataColumn ("Housing Assistance"),
                new DataColumn ("Childcare"),
                new DataColumn ("Clothing"),
                new DataColumn ("Food"),
                new DataColumn ("Supplies"),
                new DataColumn ("RFO Costs"),
                new DataColumn ("Participation Total Costs"),
                new DataColumn ("Direct and Participation Total Costs")
            });

            var costs = from a in db.BudgetCosts
                        join m in db.Months on a.MonthId equals m.Id
                        join r in db.Regions on a.RegionId equals r.Id
                        select new BudgetReport
                        {
                            BudgetInvoiceId = a.BudgetInvoiceId,
                            MonthName = m.Months,
                            RegionName = r.Regions,
                            ASalandWages = a.ASalandWages,
                            AEmpBenefits = a.AEmpBenefits,
                            AEmpTravel = a.AEmpTravel,
                            AEmpTraining = a.AEmpTraining,
                            AOfficeRent = a.AOfficeRent,
                            AOfficeUtilities = a.AOfficeUtilities,
                            AFacilityIns = a.AFacilityIns,
                            AOfficeSupplies = a.AOfficeSupplies,
                            AEquipment = a.AEquipment,
                            AOfficeCommunications = a.AOfficeCommunications,
                            AOfficeMaint = a.AOfficeMaint,
                            AConsulting = a.AConsulting,
                            SubConPayCost = a.SubConPayCost,
                            BackgrounCheck = a.BackgrounCheck,
                            Other = a.Other,
                            AJanitorServices = a.AJanitorServices,
                            ADepreciation = a.ADepreciation,
                            ATechSupport = a.ATechSupport,
                            ASecurityServices = a.ASecurityServices,
                            ATotCosts = a.ATotCosts,
                            AdminFee = a.AdminFee,
                            Trasportation = a.Trasportation,
                            JobTraining = a.JobTraining,
                            TuitionAssistance = a.TuitionAssistance,
                            ContractedResidential = a.ContractedResidential,
                            UtilityAssistance = a.UtilityAssistance,
                            EmergencyShelter = a.EmergencyShelter,
                            HousingAssistance = a.HousingAssistance,
                            Childcare = a.Childcare,
                            Clothing = a.Clothing,
                            Food = a.Food,
                            RFO = a.RFO,
                            BTotal = a.BTotal,
                            Maxtot = a.Maxtot           
                        };

            foreach (var item in costs)
            {
                dt.Rows.Add(item.BudgetInvoiceId, item.MonthName, item.RegionName, item.ASalandWages, item.AEmpBenefits, 
                    item.AEmpTravel, item.AEmpTraining, item.AOfficeRent, item.AOfficeUtilities, item.AFacilityIns, item.AOfficeSupplies, 
                    item.AEquipment, item.AOfficeCommunications, item.AOfficeMaint, item.AConsulting, item.SubConPayCost, item.BackgrounCheck,
                    item.Other, item.AJanitorServices, item.ADepreciation, item.ATechSupport, item.ASecurityServices, item.ATotCosts, 
                    item.AdminFee, item.Trasportation, item.JobTraining, item.TuitionAssistance, item.ContractedResidential,
                    item.UtilityAssistance, item.EmergencyShelter, item.HousingAssistance, item. Childcare, item.Clothing,
                    item.Food, item.RFO, item.BTotal, item.Maxtot);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
    }
}
