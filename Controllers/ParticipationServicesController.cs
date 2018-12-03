using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using Microsoft.AspNet.Identity;
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
                Subcontractors = _context.SubContractors.ToList(),
                Months = _context.Months.ToList(),
                Regions = _context.Regions.ToList()
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
                var user1 = User.Identity.GetUserId();
                viewModel.Subcontractors = _context.SubContractors.ToList();
                viewModel.Months = _context.Months.ToList();
                viewModel.Regions = _context.Regions.ToList();

                return View("Create", viewModel);
            }

            var invoice = new ParticipationService
            {
                SubcontractorId = viewModel.SubcontractorId,
                MonthId = viewModel.Month,
                RegionId = viewModel.Region,
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