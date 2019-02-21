using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.IO;
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
                Director = viewModel.Director,
                City = viewModel.City,
                State = viewModel.State,
                County = viewModel.County,
                ZipCode = viewModel.ZipCode,
                EIN = viewModel.EIN,
                Address1 = viewModel.Address1,
                PoBox = viewModel.PoBox,
                AllocatedContractAmount = viewModel.AllocatedContractAmount,
                AllocatedAdjustments = viewModel.AllocatedAdjustments,
                Active = viewModel.Active,
                SubmittedDate = DateTime.Now
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
            var contractor = _context.SubContractors.Single(s => s.SubcontractorId == viewModel.Id && s.AdministratorId == userId);
            contractor.RegionId = viewModel.Region;
            contractor.OrgName = viewModel.OrgName;
            contractor.Director = viewModel.Director;
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

            return RedirectToAction("Reports", "Subcontractor");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var org = _context.SubContractors.Single(s => s.SubcontractorId == id);
            var viewModel = new SubContractorFormViewModel
            {
                Heading = "Edit Subcontractor Information",
                Id = org.SubcontractorId,
                Regions = _context.Regions.ToList(),
                Director = org.Director,
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
                Region = org.RegionId,
                SubmittedDate = DateTime.Now
            };
            return View("SubContractorForm", viewModel);
        }

        // GET: BudgetReports
        public ActionResult Index()
        {
            return View(_context.SubContractors.ToList());
        }

        public ActionResult Reports()
        {
            var subcontractors = from s in _context.SubContractors
                          join r in _context.Regions on s.Regions.Id equals r.Id
                          where s.SubcontractorId > 0
                          select new SubcontractorReport
                          {
                              SubcontractorId = s.SubcontractorId,
                              EIN = s.EIN,
                              OrgName = s.OrgName,
                              Director = s.Director,
                              Region = r.Regions,
                              Active = s.Active,
                              SubmittedDate = DateTime.Now
                          };

            return View(subcontractors);
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn ("EIN"),
                new DataColumn ("Organization"),
                new DataColumn ("Director"),
                new DataColumn ("Region"),
                new DataColumn ("Active"),
                new DataColumn ("Date Submitted")
            });

            var subcontractors = from s in _context.SubContractors
                                 join r in _context.Regions on s.Regions.Id equals r.Id
                                 where s.SubcontractorId > 0
                                 select new SubcontractorReport
                                 {
                                     EIN = s.EIN,
                                     OrgName = s.OrgName,
                                     Director = s.Director,
                                     Region = r.Regions,
                                     Active = s.Active,
                                     SubmittedDate = DateTime.Now
                                 };


            foreach (var item in subcontractors)
            {
                dt.Rows.Add(item.EIN, item.OrgName, item.Director, item.Region, item.Active, item.SubmittedDate);
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