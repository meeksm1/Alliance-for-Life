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
    public class AdministrationCostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdministrationCost
        public ActionResult Index()
        {
            var adminCosts = db.AdminCosts.Include(a => a.Month).Include(a => a.Region).Include(a => a.Subcontractor);
            return View(adminCosts.ToList());
        }

        // GET: AdministrationCost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminCosts adminCosts = db.AdminCosts
                .Include(a => a.Month)
                .Include(a => a.Region)
                .Include(a => a.Subcontractor)
                .SingleOrDefault(a => a.AdminCostId == id);
            if (adminCosts == null)
            {
                return HttpNotFound();
            }
            return View(adminCosts);
        }

        // GET: AdministrationCost/Create
        public ActionResult Create()
        {
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions");
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");
            return View();
        }

        // POST: AdministrationCost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminCostId,ASalandWages,AEmpBenefits,AEmpTravel,AEmpTraining,AOfficeRent,AOfficeUtilities,AFacilityIns,AOfficeSupplies,AEquipment,AOfficeCommunications,AOfficeMaint,AConsulting,AJanitorServices,ADepreciation,ATechSupport,ASecurityServices,AOther,AOther2,AOther3,ATotCosts,RegionId,MonthId,SubcontractorId")] AdminCosts adminCosts)
        {
            if (ModelState.IsValid)
            {
                db.AdminCosts.Add(adminCosts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", adminCosts.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", adminCosts.RegionId);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", adminCosts.SubcontractorId);
            return View(adminCosts);
        }

        // GET: AdministrationCost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminCosts adminCosts = db.AdminCosts.Find(id);
            if (adminCosts == null)
            {
                return HttpNotFound();
            }
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", adminCosts.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", adminCosts.RegionId);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", adminCosts.SubcontractorId);
            return View(adminCosts);
        }

        // POST: AdministrationCost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminCostId,ASalandWages,AEmpBenefits,AEmpTravel,AEmpTraining,AOfficeRent,AOfficeUtilities,AFacilityIns,AOfficeSupplies,AEquipment,AOfficeCommunications,AOfficeMaint,AConsulting,AJanitorServices,ADepreciation,ATechSupport,ASecurityServices,AOther,AOther2,AOther3,ATotCosts,RegionId,MonthId,SubcontractorId")] AdminCosts adminCosts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminCosts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", adminCosts.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", adminCosts.RegionId);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", adminCosts.SubcontractorId);
            return View(adminCosts);
        }

        // GET: AdministrationCost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminCosts adminCosts = db.AdminCosts
                .Include(a => a.Month)
                .Include(a => a.Region)
                .Include(a => a.Subcontractor)
                .SingleOrDefault(a => a.AdminCostId == id);

            if (adminCosts == null)
            {
                return HttpNotFound();
            }
            return View(adminCosts);
        }

        // POST: AdministrationCost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminCosts adminCosts = db.AdminCosts
                .Include(a => a.Month)
                .Include(a => a.Region)
                .Include(a => a.Subcontractor)
                .SingleOrDefault(a => a.AdminCostId == id);

            db.AdminCosts.Remove(adminCosts);
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
            dt.Columns.AddRange(new DataColumn[24]
            {
                new DataColumn ("Administration Invoice Id"),
                new DataColumn ("Organization"),
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
                new DataColumn ("Janitor Services"),
                new DataColumn ("Depreciation"),
                new DataColumn ("Technical Support"),
                new DataColumn ("Security Services"),
                new DataColumn ("Other"),
                new DataColumn ("Other 2"),
                new DataColumn ("Other 3"),
                new DataColumn ("Total Costs"),
            });

            var costs = from a in db.AdminCosts
                        join m in db.Months on a.MonthId equals m.Id
                        join r in db.Regions on a.RegionId equals r.Id
                        join s in db.SubContractors on a.SubcontractorId equals s.SubcontractorId
                        where a.SubcontractorId == s.SubcontractorId
                        select new AdminReport
                        {
                            AdminCostId = a.AdminCostId,
                            OrgName = s.OrgName,
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
                            AJanitorServices = a.AJanitorServices,
                            ADepreciation = a.ADepreciation,
                            ATechSupport = a.ATechSupport,
                            ASecurityServices = a.ASecurityServices,
                            AOther = a.AOther,
                            AOther2 = a.AOther2,
                            AOther3 = a.AOther3,
                            ATotCosts = a.ATotCosts
                        };

            foreach (var item in costs)
            {
                dt.Rows.Add(item.AdminCostId, item.OrgName, item.MonthName, item.RegionName, item.ASalandWages, item.AEmpBenefits, item.AEmpTravel,
                    item.AEmpTraining, item.AOfficeRent, item.AOfficeUtilities, item.AFacilityIns, item.AOfficeSupplies, item.AEquipment, 
                    item.AOfficeCommunications, item.AOfficeMaint, item.AConsulting, item.AJanitorServices, item.ADepreciation,
                    item.ATechSupport, item.ASecurityServices, item.AOther, item.AOther2, item.AOther3, item.ATotCosts);
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
