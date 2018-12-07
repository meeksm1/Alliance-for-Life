using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using System.Data;
using System.IO;
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

        public ActionResult Index()
        {
            return View(_context.ParticipationServices.ToList());
        }

        public ActionResult Reports()
        {
            var costs = from a in _context.AdminCosts
                        join m in _context.Months on a.MonthId equals m.Id
                        join r in _context.Regions on a.RegionId equals r.Id
                        select new ParticipationService
                        {
                            
                        };

            return View(costs);
        }

        public ActionResult ExpenseReports()
        {
            var costs = from a in _context.AdminCosts
                        join m in _context.Months on a.MonthId equals m.Id
                        join r in _context.Regions on a.RegionId equals r.Id
                        select new ParticipationService
                        {
                            
                        };

            return View(costs);
        }

        public ActionResult Create()
        {
            var viewModel = new ParticipationServicesViewModel
            {
                Subcontractors = _context.SubContractors.ToList(),
                Months = _context.Months.ToList(),
                Regions = _context.Regions.ToList(),
                Heading = "Budgeted Participation Costs"
            };
            return View("ParticipationServicesForm", viewModel);
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

                return View("ParticipationServicesForm", viewModel);
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ParticipationServicesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Regions = _context.Regions.ToList();
                viewModel.Months = _context.Months.ToList();
                return View("AdminCostsForm", viewModel);
            }


            var invoice = new ParticipationService
            {
                SubcontractorId = viewModel.SubcontractorId,
                MonthId = viewModel.Month,
                RegionId = viewModel.Region,
                PChildCare = viewModel.PChildCare,
                PClothing = viewModel.PClothing,
                PEducationAssistance = viewModel.PEducationAssistance,
                PFood = viewModel.PFood,
                PHousingAssistance = viewModel.PHousingAssistance,
                PHousingEmergency = viewModel.PHousingEmergency,
                PJobTrain = viewModel.PJobTrain,
                POther = viewModel.POther,
                POther2 = viewModel.POther2,
                POther3 = viewModel.POther3,
                PResidentialCare = viewModel.PResidentialCare,
                PSupplies = viewModel.PSupplies,
                PTotals = viewModel.PTotals

            };

            _context.ParticipationServices.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            var org = _context.ParticipationServices.Single(s => s.PSId == id);
            var viewModel = new ParticipationServicesViewModel

            {
                Heading = "Edit Region Budget Analysis",
                Id = org.PSId,
                Regions = _context.Regions.ToList(),
                Months = _context.Months.ToList(),
                PChildCare = org.PChildCare,
                PClothing = org.PClothing,
                PEducationAssistance = org.PEducationAssistance,
                PFood = org.PFood,
                PHousingAssistance = org.PHousingAssistance,
                PHousingEmergency = org.PHousingEmergency,
                PJobTrain = org.PJobTrain,
                POther = org.POther,
                POther2 = org.POther2,
                POther3 = org.POther3,
                PResidentialCare = org.PResidentialCare,
                PSupplies = org.PSupplies,
                PTotals = org.PTotals


            };
            return View("AdminCostsForm", viewModel);
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[16]
            {
                new DataColumn ("Participation Services ID"),
                new DataColumn ("Region"),
                new DataColumn ("Month"),
                new DataColumn ("Child Care"),
                new DataColumn ("Clothing"),
                new DataColumn ("Education"),
                new DataColumn ("Food"),
                new DataColumn ("Housing"),
                new DataColumn ("Housing Emergencies"),
                new DataColumn ("Job Training"),
                new DataColumn ("Residential Care"),
                new DataColumn ("Supplies"),
                new DataColumn ("Other"),
                new DataColumn ("Other"),
                new DataColumn ("Other"),
                new DataColumn ("Totals"),
            });

            var costs = from a in _context.ParticipationServices
                        join m in _context.Months on a.MonthId equals m.Id
                        join r in _context.Regions on a.RegionId equals r.Id
                        select new ParticipationServiceReport
                        {
                            PSId =a.PSId,
                            RegionName = r.Regions,
                            MonthName = m.Months,
                            PChildCare = a.PChildCare,
                            PClothing = a.PClothing,
                            PEducationAssistance = a.PEducationAssistance,
                            PFood = a.PFood,
                            PHousingAssistance = a.PHousingAssistance,
                            PHousingEmergency = a.PHousingEmergency,
                            PJobTrain = a.PJobTrain,
                            PResidentialCare = a.PResidentialCare,
                            PSupplies = a.PSupplies,
                            POther = a.POther,
                            POther2 = a.POther2,
                            POther3 = a.POther3,
                            PTotals = a.PTotals
                        };

            foreach (var item in costs)
            {
                dt.Rows.Add(item.PSId, item.RegionName, item.MonthName, item.PChildCare, item.PClothing,
                    item.PEducationAssistance, item.PFood, item.PHousingAssistance, item.PHousingEmergency,
                    item.PJobTrain, item.PResidentialCare, item.PSupplies, item.POther, item.POther2, item.POther3,
                    item.PTotals);
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