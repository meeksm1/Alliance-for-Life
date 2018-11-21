using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class AdminCostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminCostsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View(_context.AdminCosts.ToList());
        }

        public ActionResult Reports()
        {
            var costs = from a in _context.AdminCosts
                        join m in _context.Months on a.MonthId equals m.Id
                        join r in _context.Regions on a.RegionId equals r.Id
                        where a.AdminCostId > 0
                        select new AdminReport
                        {
                            AdminCostId = a.AdminCostId,
                            MonthName = m.Months,
                            RegionName = r.Regions,
                            ASalandWages = a.ASalandWages,
                            AEquipment = a.AEquipment,
                            ATotCosts = a.ATotCosts
                        };

            return View(costs);
        }

        public ActionResult ExpenseReports()
        {
            var costs = from a in _context.AdminCosts
                        join m in _context.Months on a.MonthId equals m.Id
                        join r in _context.Regions on a.RegionId equals r.Id
                        where a.AdminCostId > 0
                        select new AdminReport
                        {
                            AdminCostId = a.AdminCostId,
                            RegionName = r.Regions,
                            MonthName = m.Months,
                            ATotCosts = a.ATotCosts,
                            ASalandWages = a.ASalandWages,
                            AEquipment = a.AEquipment
                        };

            return View(costs);
        }

        public ActionResult Create()
        {
            var viewModel = new AdminCostsViewModel
            {
                Regions = _context.Regions.ToList(),
                Months = _context.Months.ToList(),
                Heading = "Direct Administrative Costs"
            };
            return View("AdminCostsForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminCostsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Regions = _context.Regions.ToList();
                viewModel.Months = _context.Months.ToList();
                return View("AdminCostsForm", viewModel);
            }


            var invoice = new AdminCosts
            {
                MonthId = viewModel.Month,
                RegionId = viewModel.Region,
                ASalandWages = viewModel.ASalandWages,
                AConsulting = viewModel.AConsulting,
                ADepreciation = viewModel.ADepreciation,
                AEmpBenefits = viewModel.AEmpBenefits,
                AEmpTraining = viewModel.AEmpTraining,
                AEmpTravel = viewModel.AEmpTravel,
                AEquipment = viewModel.AEquipment,
                AFacilityIns = viewModel.AFacilityIns,
                AJanitorServices = viewModel.AJanitorServices,
                AOfficeCommunications = viewModel.AOfficeCommunications,
                AOfficeMaint = viewModel.AOfficeMaint,
                AOfficeRent = viewModel.AOfficeRent,
                AOfficeSupplies = viewModel.AOfficeSupplies,
                AOfficeUtilities = viewModel.AOfficeUtilities,
                AOther = viewModel.AOther,
                AOther2 = viewModel.AOther2,
                AOther3 = viewModel.AOther3,
                ASecurityServices = viewModel.ASecurityServices,
                ATechSupport = viewModel.ATechSupport,
                ATotCosts = viewModel.ATotCosts

            };

            _context.AdminCosts.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AdminCostsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Regions = _context.Regions.ToList();
                viewModel.Months = _context.Months.ToList();
                return View("AdminCostsForm", viewModel);
            }


            var invoice = new AdminCosts
            {
                MonthId = viewModel.Month,
                RegionId = viewModel.Region,
                ASalandWages = viewModel.ASalandWages,
                AConsulting = viewModel.AConsulting,
                ADepreciation = viewModel.ADepreciation,
                AEmpBenefits = viewModel.AEmpBenefits,
                AEmpTraining = viewModel.AEmpTraining,
                AEmpTravel = viewModel.AEmpTravel,
                AEquipment = viewModel.AEquipment,
                AFacilityIns = viewModel.AFacilityIns,
                AJanitorServices = viewModel.AJanitorServices,
                AOfficeCommunications = viewModel.AOfficeCommunications,
                AOfficeMaint = viewModel.AOfficeMaint,
                AOfficeRent = viewModel.AOfficeRent,
                AOfficeSupplies = viewModel.AOfficeSupplies,
                AOfficeUtilities = viewModel.AOfficeUtilities,
                AOther = viewModel.AOther,
                AOther2 = viewModel.AOther2,
                AOther3 = viewModel.AOther3,
                ASecurityServices = viewModel.ASecurityServices,
                ATechSupport = viewModel.ATechSupport,
                ATotCosts = viewModel.ATotCosts

            };

            _context.AdminCosts.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            var org = _context.AdminCosts.Single(s => s.AdminCostId == id);
            var viewModel = new AdminCostsViewModel

            {
                Heading = "Edit Region Budget Analysis",
                Id = org.AdminCostId,
                Regions = _context.Regions.ToList(),
                Months = _context.Months.ToList(),
                ASalandWages = org.ASalandWages,
                AConsulting = org.AConsulting,
                ADepreciation = org.ADepreciation,
                AEmpBenefits = org.AEmpBenefits,
                AEmpTraining = org.AEmpTraining,
                AEmpTravel = org.AEmpTravel,
                AEquipment = org.AEquipment,
                AFacilityIns = org.AFacilityIns,
                AJanitorServices = org.AJanitorServices,
                AOfficeCommunications = org.AOfficeCommunications,
                AOfficeMaint = org.AOfficeMaint,
                AOfficeRent = org.AOfficeRent,
                AOfficeSupplies = org.AOfficeSupplies,
                AOfficeUtilities = org.AOfficeUtilities,
                AOther = org.AOther,
                AOther2 = org.AOther2,
                AOther3 = org.AOther3,
                ASecurityServices = org.ASecurityServices,
                ATechSupport = org.ATechSupport,
                ATotCosts = org.ATotCosts

            };
            return View("AdminCostsForm", viewModel);
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn ("Administration Invoice ID"),
                new DataColumn ("Month"),
                new DataColumn ("Region"),
                new DataColumn ("Salary/Wages"),
                new DataColumn ("Equipment Totals"),
                new DataColumn ("Total Cost")
            });

            var costs = from a in _context.AdminCosts
                        join m in _context.Months on a.MonthId equals m.Id
                        join r in _context.Regions on a.RegionId equals r.Id
                        where a.AdminCostId > 0
                        select new AdminReport
                        {
                            AdminCostId = a.AdminCostId,
                            MonthName = m.Months,
                            RegionName = r.Regions,
                            ASalandWages = a.ASalandWages,
                            AEquipment = a.AEquipment,
                            ATotCosts = a.ATotCosts
                        };

            foreach (var item in costs)
            {
                dt.Rows.Add(item.AdminCostId, item.MonthName, item.RegionName, item.ASalandWages, item.AEquipment, item.ATotCosts);
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