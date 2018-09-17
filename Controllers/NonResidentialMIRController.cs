using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class NonResidentialMIRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NonResidentialMIRController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new NonResidentialMIRFormViewModel
            {
                Months = _context.Months.ToList(),
                Subcontractors = _context.SubContractors.ToList()
            };
            return View(viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NonResidentialMIRFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Months = _context.Months.ToList();
                viewModel.Subcontractors = _context.SubContractors.ToList();
                return View("Create", viewModel);
            }


            var invoice = new NonResidentialMIR
            {
                Subcontractor = viewModel.Subcontractor,
                MonthId = viewModel.Month,
                TotBedNights = viewModel.TotBedNights,
                TotA2AEnrollment = viewModel.TotA2AEnrollment,
                TotA2ABedNights = viewModel.TotA2ABedNights,
                MA2Apercent = viewModel.MA2Apercent
            };

            _context.NonResidentialMIRs.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}