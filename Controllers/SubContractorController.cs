using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class SubContractorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubContractorController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new SubContractorFormViewModel
            {
                Regions = _context.Regions.ToList(),
                Heading = "Create New Subcontractor"
            };
            return View("SubContractorForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubContractorFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Regions = _context.Regions.ToList();
                return View("SubContractorForm", viewModel);
            }
                

            var contractor = new SubContractor
            {
                AdministratorId = User.Identity.GetUserId(),
                RegionId = viewModel.Region,
                OrgName = viewModel.OrgName,
                City = viewModel.City,
                State = viewModel.State,
                County = viewModel.County,
                ZipCode = viewModel.ZipCode,
                EIN = viewModel.EIN,
                Address1 = viewModel.Address1,
                PoBox = viewModel.PoBox,
                AllocatedContractAmount = viewModel.AllocatedContractAmount,
                AllocatedAdjustments = viewModel.AllocatedAdjustments,
                Active = viewModel.Active
            };

            _context.SubContractors.Add(contractor);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SubContractorFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Regions = _context.Regions.ToList();
                return View("SubContractorForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var contractor = _context.SubContractors.Single(s => s.ID == viewModel.Id && s.AdministratorId == userId);
            contractor.RegionId = viewModel.Region;
            contractor.OrgName = viewModel.OrgName;
            contractor.City = viewModel.City;
            contractor.State = viewModel.State;
            contractor.County = viewModel.County;
            contractor.ZipCode = viewModel.ZipCode;
            contractor.EIN = viewModel.EIN;
            contractor.Address1 = viewModel.Address1;
            contractor.PoBox = viewModel.PoBox;
            contractor.AllocatedContractAmount = viewModel.AllocatedContractAmount;
            contractor.AllocatedAdjustments = viewModel.AllocatedAdjustments;
            contractor.Active = viewModel.Active;


            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var org = _context.SubContractors.Single(s => s.ID == id);
            var viewModel = new SubContractorFormViewModel
            {
                Heading = "Edit Subcontractor Information",
                Id = org.ID,
                Regions = _context.Regions.ToList(),
                City = org.City,
                State = org.State,
                County = org.County,
                ZipCode = org.ZipCode,
                EIN = org.EIN,
                Address1 = org.Address1,
                PoBox = org.PoBox,
                OrgName = org.OrgName,
                Active = org.Active,
                AllocatedContractAmount = org.AllocatedContractAmount,
                AllocatedAdjustments = org.AllocatedAdjustments,
                Region = org.RegionId
            };
            return View("SubContractorForm", viewModel);
        }
    }
}