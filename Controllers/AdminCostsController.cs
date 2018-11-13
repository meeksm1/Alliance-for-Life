using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
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
                RegionId = viewModel.Region,
                MonthId = viewModel.Month,
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
                
                RegionId = viewModel.Region,
                MonthId = viewModel.Month,
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
            var org = _context.AdminCosts.Single(s => s.Id == id);
            var viewModel = new AdminCostsViewModel

            {
                Heading = "Edit Region Budget Analysis",
                Id = org.Id,
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

        public ActionResult ExpenseReports()
        {
            var budgets = from b in _context.BudgetCosts
                          join m in _context.Months on b.Month.Id equals m.Id
                          join r in _context.Regions on b.Region.Id equals r.Id
                          where b.BudgetInvoiceId > 0
                          select new BudgetReport
                          {
                              BudgetInvoiceId = b.BudgetInvoiceId,
                              MonthName = m.Months,
                              RegionName = r.Regions,
                              ATotCosts = b.ATotCosts,
                              BTotal = b.BTotal,
                              Maxtot = b.Maxtot
                          };

            return View(budgets);
        }
 
        public ActionResult Reports()
        {
            var budgets = from b in _context.BudgetCosts
                          join m in _context.Months on b.Month.Id equals m.Id
                          join r in _context.Regions on b.Region.Id equals r.Id
                          where b.BudgetInvoiceId > 0
                          select new BudgetReport
                          {
                              BudgetInvoiceId = b.BudgetInvoiceId,
                              MonthName = m.Months,
                              RegionName = r.Regions,
                              ATotCosts = b.ATotCosts,
                              BTotal = b.BTotal,
                              Maxtot = b.Maxtot
                          };

            return View(budgets);
        }
    }
}