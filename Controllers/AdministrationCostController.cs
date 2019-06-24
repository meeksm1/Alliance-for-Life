using Alliance_for_Life.Models;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    [Authorize]
    public class AdministrationCostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdministrationCost
        public ActionResult Index(string sortOrder, Guid? searchString, Enum Month, string Year, string currentFilter, int? page, string pgSize)
        {
            int pageSize = Convert.ToInt16(pgSize);

            //paged view
            ViewBag.CurrentSort = sortOrder;
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.RegionSortParm = sortOrder == "Region" ? "region_desc" : "Region";

            //looking for the searchstring
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                currentFilter = searchString.ToString();
            }

            ViewBag.CurrentFilter = searchString;

            var adminSearch = db.AdminCosts
            .Include(a => a.Subcontractor)
            .Where(a => a.SubcontractorId == a.Subcontractor.SubcontractorId);

            if (!User.IsInRole("Admin"))
            {
                var organization = "";
                var id = User.Identity.GetUserId();
                var usr = db.Users.Find(id);
                organization = db.SubContractors.Find(usr.SubcontractorId).OrgName;
                var usersubid = db.Users.Find(id).SubcontractorId;

                adminSearch = from s in adminSearch
                              where usersubid == s.SubcontractorId
                              select s;

                ViewBag.Subcontractor = organization;
            }

            var year_search = "";

            if ((Year != null) && Year.Length > 1)
            {
                year_search = (Year);
            }

            if (!String.IsNullOrEmpty(searchString.ToString()) || !String.IsNullOrEmpty(Year)/* || !String.IsNullOrEmpty(Month.ToString())*/)
            {
                var yearSearch = (Year);

                if (String.IsNullOrEmpty(searchString.ToString()))
                {
                    adminSearch = adminSearch.Where(r => r.Year.ToString() == Year).OrderBy(r => r.Month);
                }
                else if (String.IsNullOrEmpty(Year.ToString()))
                {
                    var monthSearch = Enum.Parse(typeof(Months), Month.ToString());
                    adminSearch = adminSearch.Where(r => r.Month == (Months)monthSearch).OrderBy(r => r.Month);
                }
                else
                {
                    var monthSearch = Enum.Parse(typeof(Months), Month.ToString());
                    adminSearch = adminSearch.Where(r => r.Month == (Months)monthSearch && r.Year.ToString() == Year).OrderBy(r => r.Month);
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    adminSearch = adminSearch.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    adminSearch = adminSearch.OrderBy(s => s.SubmittedDate);
                    break;
                case "date_desc":
                    adminSearch = adminSearch.OrderByDescending(s => s.SubmittedDate);
                    break;
                default:
                    adminSearch = adminSearch.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(adminSearch.ToPagedList(pageNumber, pageSize));
        }

        // GET: AdministrationCost/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminCosts adminCosts = db.AdminCosts
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
            var list = db.SubContractors.ToList();
            if (!User.IsInRole("Admin"))
            {
                var id = User.Identity.GetUserId();
                var usersubid = db.Users.Find(id).SubcontractorId;

                list = list.Where(s => s.SubcontractorId == usersubid).ToList();

            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(list, "SubcontractorId", "OrgName");

            return View();
        }

        // POST: AdministrationCost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminCostId,AOtherInput,AOtherInput2,AOtherInput3,ASalandWages,AflBillable,AEmpBenefits,AEmpTravel,AEmpTraining,AOfficeRent,AOfficeUtilities,AFacilityIns,AOfficeSupplies,AEquipment,AOfficeCommunications,AOfficeMaint,AConsulting,AJanitorServices,ADepreciation,ATechSupport,ASecurityServices,AOther,AOther2,AOther3,ATotCosts,Region,Month,SubcontractorId,Year,SubmittedDate")] AdminCosts adminCosts)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.AdminCosts
                                where s.SubcontractorId == adminCosts.SubcontractorId &&
                                s.Year == adminCosts.Year &&
                                s.Month == adminCosts.Month
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    adminCosts.AdminCostId = Guid.NewGuid();
                    adminCosts.SubmittedDate = System.DateTime.Now;
                    db.AdminCosts.Add(adminCosts);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", adminCosts.SubcontractorId);

            return View(adminCosts);
        }

        // GET: AdministrationCost/Edit/5
        public ActionResult Edit(Guid? id)
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

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors.Where(a => a.SubcontractorId == adminCosts.SubcontractorId), "SubcontractorId", "OrgName");
            
            return View(adminCosts);
        }

        // POST: AdministrationCost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminCostId,AOtherInput,AOtherInput2,AOtherInput3,ASalandWages,AEmpBenefits,AflBillable,AEmpTravel,AEmpTraining,AOfficeRent,AOfficeUtilities,AFacilityIns,AOfficeSupplies,AEquipment,AOfficeCommunications,AOfficeMaint,AConsulting,AJanitorServices,ADepreciation,ATechSupport,ASecurityServices,AOther,AOther2,AOther3,ATotCosts,Region,Month,SubcontractorId,Year,SubmittedDate")] AdminCosts adminCosts)
        {
            if (ModelState.IsValid)
            {
                adminCosts.SubmittedDate = System.DateTime.Now;
                db.Entry(adminCosts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();


            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors.Where(s => s.SubcontractorId == adminCosts.SubcontractorId), "SubcontractorId", "OrgName");
            return View(adminCosts);
        }

        // GET: AdministrationCost/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminCosts adminCosts = db.AdminCosts

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
        public ActionResult DeleteConfirmed(Guid id)
        {
            AdminCosts adminCosts = db.AdminCosts

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
            dt.Columns.AddRange(new DataColumn[27]
            {
                new DataColumn ("Administration Invoice Id"),
                new DataColumn ("Date Submitted"),
                new DataColumn ("Organization"),
                new DataColumn ("Month"),
                new DataColumn ("Region"),
                new DataColumn ("Year"),
                new DataColumn ("AflBillable"),
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
                        join s in db.SubContractors on a.SubcontractorId equals s.SubcontractorId
                        where a.SubcontractorId == s.SubcontractorId
                        select new AdminReport
                        {
                            AdminCostId = a.AdminCostId,
                            SubmittedDate = a.SubmittedDate,
                            OrgName = s.OrgName,
                            MonthName = a.Month.ToString(),
                            YearName = a.Year,
                            AflBillable = a.AflBillable,
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
                dt.Rows.Add(item.AdminCostId, item.SubmittedDate, item.OrgName, item.MonthName, item.RegionName, item.YearName, item.AflBillable, item.ASalandWages, item.AEmpBenefits,
                    item.AEmpTravel, item.AEmpTraining, item.AOfficeRent, item.AOfficeUtilities, item.AFacilityIns, item.AOfficeSupplies, item.AEquipment,
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
