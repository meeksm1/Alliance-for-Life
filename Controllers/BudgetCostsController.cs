using Alliance_for_Life.Models;
using ClosedXML.Excel;
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
    public class BudgetCostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BudgetCosts
        public ViewResult Index(string sortOrder, string searchString, int? yearsearch, string currentFilter, int? page, string pgSize)
        {
            int pageSize = Convert.ToInt16(pgSize);

            //paged view
            ViewBag.CurrentSort = sortOrder;

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
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var budgetsearch = from s in db.BudgetCosts
                               select s;



            if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(yearsearch.ToString()))
            {
                var yearSearch = (yearsearch);

                if (String.IsNullOrEmpty(searchString))
                {
                    budgetsearch = budgetsearch.Where(r => r.Year == yearsearch).OrderBy(r => r.Region);
                }
                else if (String.IsNullOrEmpty(yearsearch.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch).OrderBy(r => r.Region);

                }
                else
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch && r.Year == yearsearch).OrderBy(r => r.Region);

                }
            }

            switch (sortOrder)
            {
                case "Year":
                    budgetsearch = budgetsearch.OrderBy(s => s.Year);
                    break;
                case "Region":
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
                case "year_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Year);
                    break;
                case "region_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Region);
                    break;
                default:
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(budgetsearch.ToPagedList(pageNumber, pageSize));

        }

        //#############################################################################################
        //Graphing the data
        public ActionResult GraphIndex()
        {
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist, "Year");
            ViewBag.Region = new SelectList(Enum.GetNames(typeof(GeoRegion)), "Region");
            return View();
        }

        //populate graph based on the user 

        public JsonResult GraphDataIndex(string yearsearch)
        {
            int thisyear = DateTime.Now.Year;
            var region = 1;

            if (!String.IsNullOrEmpty(yearsearch))
            {
                region = Int32.Parse(yearsearch.Substring(10, 1));
                thisyear = Int32.Parse(yearsearch.Substring(0, 4));
            }

            var costlist = db.BudgetCosts
                .Where(s => s.Year == thisyear && (int)s.Region.Value == region);

            return Json(costlist.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GraphDataIndexTest(string yearsearch)
        {
            int thisyear = DateTime.Now.Year;
            var region = 1;

            //checking the url string for the returned value
            if (!String.IsNullOrEmpty(yearsearch))
            {
                if (yearsearch.Length > 7)
                {
                    thisyear = Int32.Parse(yearsearch.Substring(0, 4));
                    region = Int32.Parse(yearsearch.Substring(10, 1));
                }
                else
                {
                    region = Int32.Parse(yearsearch.Substring(6, 1));
                }
            }

            var costlist = db.BudgetCosts
                .Where(s => s.Year == thisyear && (int)s.Region.Value == region);

            return Json(costlist.ToList(), JsonRequestBehavior.AllowGet);
        }
        //#############################################################################################
        public ViewResult FirstQuarter(string sortOrder, string searchString, int? yearsearch, string currentFilter, int? page, string pgSize)
        {
            int pageSize = Convert.ToInt16(pgSize);
            //paged view
            ViewBag.CurrentSort = sortOrder;

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
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var budgetsearch = db.BudgetCosts
            .Include(b => b.User);


            if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(yearsearch.ToString()))
            {
                var yearSearch = (yearsearch);

                if (String.IsNullOrEmpty(searchString))
                {
                    budgetsearch = budgetsearch.Where(r => r.Year == yearsearch).OrderBy(r => r.Region);
                }
                else if (String.IsNullOrEmpty(yearsearch.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch).OrderBy(r => r.Region);

                }
                else
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch && r.Year == yearsearch).OrderBy(r => r.Region);

                }
            }
            switch (sortOrder)
            {
                case "Year":
                    budgetsearch = budgetsearch.OrderBy(s => s.Year);
                    break;
                case "Region":
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
                case "year_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Year);
                    break;
                case "region_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Region);
                    break;
                default:
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(budgetsearch.ToPagedList(pageNumber, pageSize));

        }

        public ViewResult SecondQuarter(string sortOrder, string searchString, int? yearsearch, string currentFilter, int? page, string pgSize)
        {
            int pageSize = Convert.ToInt16(pgSize);
            //paged view
            ViewBag.CurrentSort = sortOrder;

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
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var budgetsearch = db.BudgetCosts
            .Include(b => b.User);


            if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(yearsearch.ToString()))
            {
                var yearSearch = (yearsearch);

                if (String.IsNullOrEmpty(searchString))
                {
                    budgetsearch = budgetsearch.Where(r => r.Year == yearsearch).OrderBy(r => r.Region);
                }
                else if (String.IsNullOrEmpty(yearsearch.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch).OrderBy(r => r.Region);

                }
                else
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch && r.Year == yearsearch).OrderBy(r => r.Region);

                }
            }
            switch (sortOrder)
            {
                case "Year":
                    budgetsearch = budgetsearch.OrderBy(s => s.Year);
                    break;
                case "Region":
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
                case "year_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Year);
                    break;
                case "region_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Region);
                    break;
                default:
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(budgetsearch.ToPagedList(pageNumber, pageSize));

        }

        public ViewResult ThirdQuarter(string sortOrder, string searchString, int? yearsearch, string currentFilter, int? page, string pgSize)
        {
            int pageSize = Convert.ToInt16(pgSize);
            //paged view
            ViewBag.CurrentSort = sortOrder;

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
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var budgetsearch = db.BudgetCosts
            .Include(b => b.User);

            if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(yearsearch.ToString()))
            {
                var yearSearch = (yearsearch);

                if (String.IsNullOrEmpty(searchString))
                {
                    budgetsearch = budgetsearch.Where(r => r.Year == yearsearch).OrderBy(r => r.Region);
                }
                else if (String.IsNullOrEmpty(yearsearch.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch).OrderBy(r => r.Region);

                }
                else
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch && r.Year == yearsearch).OrderBy(r => r.Region);

                }
            }

            switch (sortOrder)
            {
                case "Year":
                    budgetsearch = budgetsearch.OrderBy(s => s.Year);
                    break;
                case "Region":
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
                case "year_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Year);
                    break;
                case "region_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Region);
                    break;
                default:
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
            }


            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(budgetsearch.ToPagedList(pageNumber, pageSize));

        }

        public ViewResult FourthQuarter(string sortOrder, string searchString, int? yearsearch, string currentFilter, int? page, string pgSize)
        {
            int pageSize = Convert.ToInt16(pgSize);
            //paged view
            ViewBag.CurrentSort = sortOrder;

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
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var budgetsearch = db.BudgetCosts
            .Include(b => b.User);


            if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(yearsearch.ToString()))
            {
                var yearSearch = (yearsearch);

                if (String.IsNullOrEmpty(searchString))
                {
                    budgetsearch = budgetsearch.Where(r => r.Year == yearsearch).OrderBy(r => r.Region);
                }
                else if (String.IsNullOrEmpty(yearsearch.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch).OrderBy(r => r.Region);

                }
                else
                {
                    var regionSearch = Enum.Parse(typeof(GeoRegion), searchString);
                    budgetsearch = budgetsearch.Where(r => r.Region == (GeoRegion)regionSearch && r.Year == yearsearch).OrderBy(r => r.Region);

                }
            }
            switch (sortOrder)
            {
                case "Year":
                    budgetsearch = budgetsearch.OrderBy(s => s.Year);
                    break;
                case "Region":
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
                case "year_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Year);
                    break;
                case "region_desc":
                    budgetsearch = budgetsearch.OrderByDescending(s => s.Region);
                    break;
                default:
                    budgetsearch = budgetsearch.OrderBy(s => s.Region);
                    break;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(budgetsearch.ToPagedList(pageNumber, pageSize));

        }

        // GET: BudgetCosts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetCosts budgetCosts = db.BudgetCosts

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
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            return View();
        }

        // POST: BudgetCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetInvoiceId,ASalandWages,AEmpBenefits,AEmpTravel,AEmpTraining,AOfficeRent,AOfficeUtilities,AFacilityIns,AOfficeSupplies,AEquipment,AOfficeCommunications,AOfficeMaint,AConsulting,SubConPayCost,BackgrounCheck,Other,AJanitorServices,ADepreciation,ATechSupport,ASecurityServices,ATotCosts,AdminFee,Trasportation,JobTraining,TuitionAssistance,ContractedResidential,UtilityAssistance,EmergencyShelter,HousingAssistance,Childcare,Clothing,Food,Supplies,RFO,BTotal,Maxtot,Region,Month,Year,SubmittedDate")] BudgetCosts budgetCosts)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.BudgetCosts
                                where
                                s.Year == budgetCosts.Year &&
                                s.Region == budgetCosts.Region
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    budgetCosts.BudgetInvoiceId = Guid.NewGuid();
                    budgetCosts.SubmittedDate = DateTime.Now;
                    db.BudgetCosts.Add(budgetCosts);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);

            return View(budgetCosts);
        }

        // GET: BudgetCosts/Edit/5
        public ActionResult Edit(Guid? id)
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

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);

            return View(budgetCosts);
        }

        // POST: BudgetCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetInvoiceId,ASalandWages,AEmpBenefits,AEmpTravel,AEmpTraining,AOfficeRent,AOfficeUtilities,AFacilityIns,AOfficeSupplies,AEquipment,AOfficeCommunications,AOfficeMaint,AConsulting,SubConPayCost,BackgrounCheck,Other,AJanitorServices,ADepreciation,ATechSupport,ASecurityServices,ATotCosts,AdminFee,Trasportation,JobTraining,TuitionAssistance,ContractedResidential,UtilityAssistance,EmergencyShelter,HousingAssistance,Childcare,Clothing,Food,Supplies,RFO,BTotal,Maxtot,Region,Month,Year,SubmittedDate")] BudgetCosts budgetCosts)
        {
            if (ModelState.IsValid)
            {
                budgetCosts.SubmittedDate = DateTime.Now;
                db.Entry(budgetCosts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var datelist = Enumerable.Range(DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);


            return View(budgetCosts);
        }

        // GET: BudgetCosts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetCosts budgetCosts = db.BudgetCosts

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
        public ActionResult DeleteConfirmed(Guid id)
        {
            BudgetCosts budgetCosts = db.BudgetCosts

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
            //get the invoice id
            var exportid = Request["BudgetInvoiceId"];


            //create datatable
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[39]
            {
                new DataColumn ("Budget Report Id"),
                new DataColumn ("Date Submitted"),
                new DataColumn ("Region"),
                new DataColumn ("Year"),
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
                        select new BudgetReport
                        {
                            BudgetInvoiceId = a.BudgetInvoiceId,
                            SubmittedDate = a.SubmittedDate,
                            RegionName = a.Region.ToString(),
                            YearName = a.Year,
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

            if (exportid.Length == 32)
            {
                costs = costs.Where(s => s.BudgetInvoiceId == new Guid(exportid));
            }

            foreach (var item in costs)
            {
                dt.Rows.Add(item.BudgetInvoiceId, item.SubmittedDate, item.RegionName, item.YearName, item.ASalandWages, item.AEmpBenefits,
                    item.AEmpTravel, item.AEmpTraining, item.AOfficeRent, item.AOfficeUtilities, item.AFacilityIns, item.AOfficeSupplies,
                    item.AEquipment, item.AOfficeCommunications, item.AOfficeMaint, item.AConsulting, item.SubConPayCost, item.BackgrounCheck,
                    item.Other, item.AJanitorServices, item.ADepreciation, item.ATechSupport, item.ASecurityServices, item.ATotCosts,
                    item.AdminFee, item.Trasportation, item.JobTraining, item.TuitionAssistance, item.ContractedResidential,
                    item.UtilityAssistance, item.EmergencyShelter, item.HousingAssistance, item.Childcare, item.Clothing,
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