using Alliance_for_Life.Models;
using System.Linq;
using System.Web.Mvc;
using Alliance_for_Life.ViewModels;

namespace Alliance_for_Life.Controllers
{
    public class BudgetCostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BudgetCostController()

        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new BudgetCostViewModel
            {
                Regions = _context.Regions.ToList(),
                Months = _context.Months.ToList(),
                Heading = "Monthly Budget Numbers"
            };
            return View("BudgetCostForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BudgetCostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Regions = _context.Regions.ToList();
                viewModel.Months = _context.Months.ToList();
                return View("BudgetCostForm", viewModel);
            }

            var invoice = new BudgetCosts
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
                Other = viewModel.Other,
                BackgrounCheck = viewModel.BackgrounCheck,
                SubConPayCost = viewModel.SubConPayCost,
                AJanitorServices = viewModel.AJanitorServices,
                AOfficeCommunications = viewModel.AOfficeCommunications,
                AOfficeMaint = viewModel.AOfficeMaint,
                AOfficeRent = viewModel.AOfficeRent,
                AOfficeSupplies = viewModel.AOfficeSupplies,
                AOfficeUtilities = viewModel.AOfficeUtilities,
                ASecurityServices = viewModel.ASecurityServices,
                ATechSupport = viewModel.ATechSupport,
                ATotCosts = viewModel.ATotCosts,
                AdminFee = viewModel.AdminFee,
                Trasportation = viewModel.Trasportation,
                JobTraining = viewModel.JobTraining,
                TuitionAssistance = viewModel.TuitionAssistance,
                ContractedResidential = viewModel.ContractedResidential,
                UtilityAssistance = viewModel.UtilityAssistance,
                EmergencyShelter = viewModel.EmergencyShelter,
                HousingAssistance = viewModel.HousingAssistance,
                Childcare = viewModel.Childcare,
                Clothing = viewModel.Clothing,
                Food = viewModel.Food,
                Supplies = viewModel.Supplies,
                RFO = viewModel.RFO,
                BTotal = viewModel.BTotal,
                Maxtot = viewModel.Maxtot,
            };

            _context.BudgetCosts.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(BudgetCostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Regions = _context.Regions.ToList();
                viewModel.Months = _context.Months.ToList();
                return View("BudgetCostForm", viewModel);
            }

            var invoice = new BudgetCosts
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
                Other = viewModel.Other,
                BackgrounCheck = viewModel.BackgrounCheck,
                SubConPayCost = viewModel.SubConPayCost,
                AJanitorServices = viewModel.AJanitorServices,
                AOfficeCommunications = viewModel.AOfficeCommunications,
                AOfficeMaint = viewModel.AOfficeMaint,
                AOfficeRent = viewModel.AOfficeRent,
                AOfficeSupplies = viewModel.AOfficeSupplies,
                AOfficeUtilities = viewModel.AOfficeUtilities,
                ASecurityServices = viewModel.ASecurityServices,
                ATechSupport = viewModel.ATechSupport,
                ATotCosts = viewModel.ATotCosts,
                AdminFee = viewModel.AdminFee,
                Trasportation = viewModel.Trasportation,
                JobTraining = viewModel.JobTraining,
                TuitionAssistance = viewModel.TuitionAssistance,
                ContractedResidential = viewModel.ContractedResidential,
                UtilityAssistance = viewModel.UtilityAssistance,
                EmergencyShelter = viewModel.EmergencyShelter,
                HousingAssistance = viewModel.HousingAssistance,
                Childcare = viewModel.Childcare,
                Clothing = viewModel.Clothing,
                Food = viewModel.Food,
                Supplies = viewModel.Supplies,
                RFO = viewModel.RFO,
                BTotal = viewModel.BTotal,
                Maxtot = viewModel.Maxtot,
            };

            _context.BudgetCosts.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            var org = _context.BudgetCosts.Single(s => s.MonthId == id);
            var viewModel = new BudgetCostViewModel
            {
                Heading = "Edit Region Budget Analysis",
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
                Other = org.Other,
                BackgrounCheck = org.BackgrounCheck,
                SubConPayCost = org.SubConPayCost,
                AJanitorServices = org.AJanitorServices,
                AOfficeCommunications = org.AOfficeCommunications,
                AOfficeMaint = org.AOfficeMaint,
                AOfficeRent = org.AOfficeRent,
                AOfficeSupplies = org.AOfficeSupplies,
                AOfficeUtilities = org.AOfficeUtilities,
                ASecurityServices = org.ASecurityServices,
                ATechSupport = org.ATechSupport,
                Trasportation = org.Trasportation,
                JobTraining = org.JobTraining,
                TuitionAssistance = org.TuitionAssistance,
                ContractedResidential = org.ContractedResidential,
                UtilityAssistance = org.UtilityAssistance,
                EmergencyShelter = org.EmergencyShelter,
                HousingAssistance = org.HousingAssistance,
                Childcare = org.Childcare,
                Clothing = org.Clothing,
                Food = org.Food,
                Supplies = org.Supplies,
                RFO = org.RFO,
                BTotal = org.BTotal,
                Maxtot = org.Maxtot,
            };
            return View("BudgetCostForm", viewModel);
        }
    }      
}