using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    [Authorize]
    public class SubContractorController : Controller
    {
        private readonly ApplicationDbContext db;

        public SubContractorController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new SubContractorFormViewModel
            {
                Heading = "Create New Subcontractor"
            };
            return View("SubContractorForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubContractorFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {

                return View("SubContractorForm", viewModel);
            }

            var contractor = new SubContractor
            {
                SubcontractorId = Guid.NewGuid(),
                AdministratorId = User.Identity.GetUserId(),
                Region = viewModel.Region,
                AffiliateRegion = viewModel.AffiliateRegion,
                OrgName = viewModel.OrgName,
                Director = viewModel.Director,
                City = viewModel.City,
                State = viewModel.State,
                County = viewModel.County,
                ZipCode = viewModel.ZipCode,
                EIN = viewModel.EIN,
                Address1 = viewModel.Address1,
                PoBox = viewModel.PoBox,
                Active = viewModel.Active,
                SubmittedDate = DateTime.Now
            };

            db.SubContractors.Add(contractor);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SubContractorFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                return View("SubContractorForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var contractor = db.SubContractors.SingleOrDefault(s => s.SubcontractorId == viewModel.Id);
            contractor.Region = viewModel.Region;
            contractor.AffiliateRegion = viewModel.AffiliateRegion;
            contractor.OrgName = viewModel.OrgName;
            contractor.Director = viewModel.Director;
            contractor.City = viewModel.City;
            contractor.State = viewModel.State;
            contractor.County = viewModel.County;
            contractor.ZipCode = viewModel.ZipCode;
            contractor.EIN = viewModel.EIN;
            contractor.Address1 = viewModel.Address1;
            contractor.PoBox = viewModel.PoBox;
            contractor.Active = viewModel.Active;


            db.SaveChanges();

            return RedirectToAction("Reports", "Subcontractor");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            var org = db.SubContractors.SingleOrDefault(s => s.SubcontractorId == id);
            var viewModel = new SubContractorFormViewModel
            {
                Heading = "Edit Subcontractor Information",
                Id = org.SubcontractorId,
                Region = org.Region,
                AffiliateRegion = org.AffiliateRegion,
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
                SubmittedDate = DateTime.Now
            };
            return View("SubContractorForm", viewModel);
        }

        public ActionResult Reports(string sortOrder, Guid? searchString, int? yearsearch, string currentFilter, int? page, int? pgSize)
        {
            ViewBag.Sub = searchString;

            ViewBag.Subcontractor = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName");

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                currentFilter = searchString.ToString();
            }

            var subcontractors = from s in db.SubContractors
                                 select new SubcontractorReport
                                 {
                                     SubcontractorId = s.SubcontractorId,
                                     EIN = s.EIN,
                                     OrgName = s.OrgName,
                                     Director = s.Director,
                                     Region = s.Region.ToString(),
                                     Active = s.Active,
                                     SubmittedDate = DateTime.Now
                                 };

            if (!String.IsNullOrEmpty(searchString.ToString()))
            {
                subcontractors = subcontractors.Where(a => a.SubcontractorId == searchString);

            }

            int pageNumber = (page ?? 1);
            int defaSize = (pgSize ?? 10);

            ViewBag.psize = defaSize;

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="20", Text= "20" },
                new SelectListItem() { Value="30", Text= "30" },
                new SelectListItem() { Value="40", Text= "40" },
            };

            return View(subcontractors.OrderBy(r => r.OrgName).ToPagedList(pageNumber, defaSize));
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

            var subcontractors = from s in db.SubContractors
                                 select new SubcontractorReport
                                 {
                                     EIN = s.EIN,
                                     OrgName = s.OrgName,
                                     Director = s.Director,
                                     Region = s.Region.ToString(),
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