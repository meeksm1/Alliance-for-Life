using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class ParticipationServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipationServicesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new ParticipationServicesViewModel
            {
                Months = _context.Months.ToList(),
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
        public ActionResult Create(ParticipationServicesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Months = _context.Months.ToList();

                return View("Create", viewModel);
            }

            var invoice = new ParticipationService
            {
                MonthId=viewModel.Month,
                PChildCare=viewModel.PChildCare,
                PClothing=viewModel.PClothing,
                PEducationAssistance=viewModel.PEducationAssistance,
                PFood=viewModel.PFood,
                PHousingAssistance=viewModel.PHousingAssistance,
                PHousingEmergency=viewModel.PHousingEmergency,
                PJobTrain=viewModel.PJobTrain,
                POther=viewModel.POther,
                POther2=viewModel.POther2,
                POther3=viewModel.POther3,
                PResidentialCare=viewModel.PResidentialCare,
                PSupplies=viewModel.PSupplies,
                PTotals=viewModel.PTotals
            };

            _context.ParticipationServices.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}