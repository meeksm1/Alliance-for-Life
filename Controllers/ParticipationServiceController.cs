using Alliance_for_Life.Models;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    [Authorize]
    public class ParticipationServiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ParticipationCost
        public ActionResult Index(string sortOrder, Guid? searchString, string Month, int? Year, string currentFilter, int? page, int? pgSize)
        {
            ViewBag.CurrentSort = sortOrder;
            var datelist = Enumerable.Range(System.DateTime.Now.Year, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.Subcontractor = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName");
            ViewBag.Month = new SelectList(Enum.GetValues(typeof(Months)).Cast<Months>());

            //looking for the searchstring
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                currentFilter = searchString.ToString();
            }

            ViewBag.CurrentFilter = searchString;

            var participationServices = db.ParticipationServices.Include(p => p.Subcontractor);

            var adminSearch = db.AdminCosts
            .Include(a => a.Subcontractor)
            .Where(a => a.SubcontractorId == a.Subcontractor.SubcontractorId);

            if (!User.IsInRole("Admin"))
            {
                var organization = "";
                var id = User.Identity.GetUserId();
                var usr = db.Users.Find(id);
                organization = db.SubContractors.Find(usr.SubcontractorId).OrgName;
                var usersubid = db.Users.Find(id).SubcontractorId;

                participationServices = from s in participationServices
                                        where usersubid == s.SubcontractorId
                                        select s;

                adminSearch = from t in adminSearch
                              where usersubid == t.SubcontractorId
                              select t;

                ViewBag.Subcontractor = organization;
            }

            if (!String.IsNullOrEmpty(Month) || !String.IsNullOrEmpty(Year.ToString()) || !String.IsNullOrEmpty(searchString.ToString()))
            {
                var yearSearch = (Year);

                // if fields Month and Year are empty
                if (String.IsNullOrEmpty(Month) && String.IsNullOrEmpty(Year.ToString()))
                {
                    participationServices = participationServices.Where(r => r.Subcontractor.SubcontractorId == searchString);
                }
                // if fields Year and Org are empty
                else if (String.IsNullOrEmpty(Year.ToString()) && String.IsNullOrEmpty(searchString.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    participationServices = participationServices.Where(r => r.Month == (Months)regionSearch);
                }
                // if fields Org and Month are empty
                else if (String.IsNullOrEmpty(searchString.ToString()) && String.IsNullOrEmpty(Month))
                {
                    participationServices = participationServices.Where(r => r.Year == yearSearch);
                }

                // if Month field is empty
                else if (String.IsNullOrEmpty(Month))
                {
                    participationServices = participationServices.Where(r => r.Year == yearSearch && r.Subcontractor.SubcontractorId == searchString);
                }
                //if Year is empty
                else if (String.IsNullOrEmpty(Year.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    participationServices = participationServices.Where(r => r.Month == (Months)regionSearch && r.Subcontractor.SubcontractorId == searchString);
                }
                // if Org is empty
                else if (String.IsNullOrEmpty(searchString.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    participationServices = participationServices.Where(r => r.Month == (Months)regionSearch && r.Year == yearSearch);
                }
                // if none are empty
                else
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    participationServices = participationServices.Where(r => r.Month == (Months)regionSearch && r.Year == yearSearch && r.Subcontractor.SubcontractorId == searchString);
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    participationServices = participationServices.OrderByDescending(s => s.Subcontractor.OrgName);
                    adminSearch = adminSearch.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    participationServices = participationServices.OrderBy(s => s.SubmittedDate);
                    adminSearch = adminSearch.OrderBy(s => s.SubmittedDate);
                    break;
                case "date_desc":
                    participationServices = participationServices.OrderByDescending(s => s.SubmittedDate);
                    adminSearch = adminSearch.OrderByDescending(s => s.SubmittedDate);
                    break;
                default:
                    participationServices = participationServices.OrderBy(s => s.Subcontractor.OrgName);
                    adminSearch = adminSearch.OrderBy(s => s.Subcontractor.OrgName);
                    break;
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

            ViewBag.AdminCost = adminSearch.ToList();


            return View(participationServices.ToPagedList(pageNumber, defaSize));
        }

        public ActionResult TotalCostReport(string sortOrder, Guid? searchString, string Month, int? Year, string currentFilter, int? page, int? pgSize)
        {
            ViewBag.CurrentSort = sortOrder;
            var datelist = Enumerable.Range(System.DateTime.Now.Year, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.Subcontractor = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName");
            ViewBag.Month = new SelectList(Enum.GetValues(typeof(Months)).Cast<Months>());

            //looking for the searchstring
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                currentFilter = searchString.ToString();
            }

            var year_search = "";

            if ((Year != null) && Year.ToString().Length > 1)
            {
                year_search = (Year.ToString());
            }

            ViewBag.CurrentFilter = searchString;

            var participationServices = db.ParticipationServices.Include(p => p.Subcontractor);

            var adminSearch = db.AdminCosts
            .Include(a => a.Subcontractor)
            .Where(a => a.SubcontractorId == a.Subcontractor.SubcontractorId);

            if (!User.IsInRole("Admin"))
            {
                var organization = "";
                var id = User.Identity.GetUserId();
                var usr = db.Users.Find(id);
                organization = db.SubContractors.Find(usr.SubcontractorId).OrgName;
                var usersubid = db.Users.Find(id).SubcontractorId;

                participationServices = from s in participationServices
                                        where usersubid == s.SubcontractorId
                                        select s;

                adminSearch = from t in adminSearch
                              where usersubid == t.SubcontractorId
                              select t;

                ViewBag.Subcontractor = organization;
            }
            if (!String.IsNullOrEmpty(Month) || !String.IsNullOrEmpty(Year.ToString()) || !String.IsNullOrEmpty(searchString.ToString()))
            {
                var yearSearch = (Year);

                // if fields Month and Year are empty
                if (String.IsNullOrEmpty(Month) && String.IsNullOrEmpty(Year.ToString()))
                {
                    adminSearch = adminSearch.Where(r => r.Subcontractor.SubcontractorId == searchString);
                    participationServices = participationServices.Where(r => r.Subcontractor.SubcontractorId == searchString);
                }
                // if fields Year and Org are empty
                else if (String.IsNullOrEmpty(Year.ToString()) && String.IsNullOrEmpty(searchString.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    adminSearch = adminSearch.Where(r => r.Month == (Months)regionSearch);
                    participationServices = participationServices.Where(r => r.Month == (Months)regionSearch);
                }
                // if fields Org and Month are empty
                else if (String.IsNullOrEmpty(searchString.ToString()) && String.IsNullOrEmpty(Month))
                {
                    adminSearch = adminSearch.Where(r => r.Year == yearSearch);
                    participationServices = participationServices.Where(r => r.Year == yearSearch);
                }

                // if Month field is empty
                else if (String.IsNullOrEmpty(Month))
                {
                    adminSearch = adminSearch.Where(r => r.Year == yearSearch && r.Subcontractor.SubcontractorId == searchString);
                    participationServices = participationServices.Where(r => r.Year == yearSearch && r.Subcontractor.SubcontractorId == searchString);
                }
                //if Year is empty
                else if (String.IsNullOrEmpty(Year.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    adminSearch = adminSearch.Where(r => r.Month == (Months)regionSearch && r.Subcontractor.SubcontractorId == searchString);
                    participationServices = participationServices.Where(r => r.Month == (Months)regionSearch && r.Subcontractor.SubcontractorId == searchString);
                }
                // if Org is empty
                else if (String.IsNullOrEmpty(searchString.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    adminSearch = adminSearch.Where(r => r.Month == (Months)regionSearch && r.Year == yearSearch);
                    participationServices = participationServices.Where(r => r.Month == (Months)regionSearch && r.Year == yearSearch);
                }
                // if none are empty
                else
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    adminSearch = adminSearch.Where(r => r.Month == (Months)regionSearch && r.Year == yearSearch && r.Subcontractor.SubcontractorId == searchString);
                    participationServices = participationServices.Where(r => r.Month == (Months)regionSearch && r.Year == yearSearch && r.Subcontractor.SubcontractorId == searchString);
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    participationServices = participationServices.OrderByDescending(s => s.Subcontractor.OrgName);
                    adminSearch = adminSearch.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    participationServices = participationServices.OrderBy(s => s.SubmittedDate);
                    adminSearch = adminSearch.OrderBy(s => s.SubmittedDate);
                    break;
                case "date_desc":
                    participationServices = participationServices.OrderByDescending(s => s.SubmittedDate);
                    adminSearch = adminSearch.OrderByDescending(s => s.SubmittedDate);
                    break;
                default:
                    participationServices = participationServices.OrderBy(s => s.Subcontractor.OrgName);
                    adminSearch = adminSearch.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }

            //Getting the admin cost totals

            var admincost = db.AdminCosts.ToList();

            if (year_search != "")
            {
                admincost = admincost.Where(a => a.Year.ToString() == year_search).ToList();
            }

            //calculating Admin Total Cost per month and returning it as viewbag
            ViewBag.SalTot = (admincost.Sum(a => a.ASalandWages));
            ViewBag.AEmpBenefits = (admincost.Sum(a => a.AEmpBenefits));
            ViewBag.AEmpTravel = (admincost.Sum(a => a.AEmpTravel));
            ViewBag.AEmpTraining = (admincost.Sum(a => a.AEmpTraining));
            ViewBag.AOfficeRent = (admincost.Sum(a => a.AOfficeRent));
            ViewBag.AOfficeUtilities = (admincost.Sum(a => a.AOfficeUtilities));
            ViewBag.AFacilityIns = (admincost.Sum(a => a.AFacilityIns));
            ViewBag.AOfficeSupplies = (admincost.Sum(a => a.AOfficeSupplies));
            ViewBag.AEquipment = (admincost.Sum(a => a.AEquipment));
            ViewBag.AOfficeCommunications = (admincost.Sum(a => a.AOfficeCommunications));
            ViewBag.AOfficeMaint = (admincost.Sum(a => a.AOfficeMaint));
            ViewBag.AConsulting = (admincost.Sum(a => a.AConsulting));
            ViewBag.AJanitorServices = (admincost.Sum(a => a.AJanitorServices));
            ViewBag.ADepreciation = (admincost.Sum(a => a.ADepreciation));
            ViewBag.ATechSupport = (admincost.Sum(a => a.ATechSupport));
            ViewBag.ASecurityServices = (admincost.Sum(a => a.ASecurityServices));
            ViewBag.AOther = (admincost.Sum(a => a.AOther));
            ViewBag.AOther2 = (admincost.Sum(a => a.AOther2));
            ViewBag.AOther3 = (admincost.Sum(a => a.AOther3));
            ViewBag.Total = (admincost.Sum(a => a.ATotCosts));

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


            ViewBag.AdminCost = adminSearch.ToList();
            ViewBag.AdminTotal = Math.Round(adminSearch.Select(a => a.ATotCosts).DefaultIfEmpty(0).Sum(), 2);

            return View(participationServices.ToPagedList(pageNumber, defaSize));
        }

        // GET: ParticipationCost/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipationService participationService = db.ParticipationServices

                .Include(a => a.Subcontractor)
                .SingleOrDefault(a => a.PSId == id);





            if (participationService == null)
            {
                return HttpNotFound();
            }
            return View(participationService);
        }

        // GET: ParticipationCost/Create
        public ActionResult Create()
        {
            var list = db.SubContractors.ToList();
            if (!User.IsInRole("Admin"))
            {
                var id = User.Identity.GetUserId();
                var usersubid = db.Users.Find(id).SubcontractorId;

                list = list.Where(s => s.SubcontractorId == usersubid).ToList();

            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(list, "SubcontractorId", "OrgName");



            return View();
        }

        // POST: ParticipationCost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PSId,PTranspotation,POtherInput,POtherInput2,POtherInput3,PJobTrain,PEducationAssistance,PResidentialCare,PUtilities,PHousingEmergency,PHousingAssistance,PChildCare,PClothing,PFood,PSupplies,POther,POther2,POther3,PTotals,Region,Month,SubcontractorId,Year,SubmittedDate")] ParticipationService participationService)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.ParticipationServices
                                where
                                s.SubcontractorId == participationService.SubcontractorId &&
                                s.Year == participationService.Year &&
                                s.Month == participationService.Month
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    participationService.PSId = Guid.NewGuid();
                    participationService.SubmittedDate = DateTime.Now;
                    db.ParticipationServices.Add(participationService);
                    participationService.Region = db.SubContractors.Where(A => A.SubcontractorId == participationService.SubcontractorId).FirstOrDefault().Region;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName", participationService.SubcontractorId);
            return View(participationService);
        }

        // GET: ParticipationCost/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipationService participationService = db.ParticipationServices.Find(id);
            if (participationService == null)
            {
                return HttpNotFound();
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors.OrderBy(a => a.OrgName).Where(a => a.SubcontractorId == participationService.SubcontractorId), "SubcontractorId", "OrgName");
            return View(participationService);
        }

        // POST: ParticipationCost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PSId,PTranspotation,POtherInput,POtherInput2,POtherInput3,PJobTrain,PEducationAssistance,PResidentialCare,PUtilities,PHousingEmergency,PHousingAssistance,PChildCare,PClothing,PFood,PSupplies,POther,POther2,POther3,PTotals,Region,Month,SubcontractorId,Year,SubmittedDate")] ParticipationService participationService)
        {
            if (ModelState.IsValid)
            {
                participationService.SubmittedDate = DateTime.Now;
                db.Entry(participationService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors.OrderBy(a => a.OrgName), "SubcontractorId", "OrgName", participationService.SubcontractorId);
            return View(participationService);
        }

        // GET: ParticipationCost/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipationService participationService = db.ParticipationServices

                .Include(a => a.Subcontractor)
                .SingleOrDefault(a => a.PSId == id);

            if (participationService == null)
            {
                return HttpNotFound();
            }
            return View(participationService);
        }

        // POST: ParticipationCost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ParticipationService participationService = db.ParticipationServices

                .Include(a => a.Subcontractor)
                .SingleOrDefault(a => a.PSId == id);

            db.ParticipationServices.Remove(participationService);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public FileResult Export()
        {
            DataTable dt = new DataTable("Total Cost Report");
            dt.Columns.AddRange(new DataColumn[20]
            {
                new DataColumn ("Organization"),
                new DataColumn ("Month"),
                new DataColumn ("Region"),
                new DataColumn ("Year"),
                new DataColumn ("Transportation"),
                new DataColumn ("Job Training"),
                new DataColumn ("Education"),
                new DataColumn ("Education Assistance"),
                new DataColumn ("Residential Care"),
                new DataColumn ("Utilities"),
                new DataColumn ("Housing Emergencies"),
                new DataColumn ("Housing Assistance"),
                new DataColumn ("Child Care"),
                new DataColumn ("Clothing"),
                new DataColumn ("Food"),
                new DataColumn ("Supplies"),
                new DataColumn ("Other"),
                new DataColumn ("Other 2"),
                new DataColumn ("Other 3"),
                new DataColumn ("Total Costs")
            });

            var costs = from a in db.ParticipationServices
                        join s in db.SubContractors on a.SubcontractorId equals s.SubcontractorId
                        where a.SubcontractorId == s.SubcontractorId
                        select new ParticipationServiceReport
                        {
                            OrgName = a.Subcontractor.OrgName,
                            MonthName = a.Month.ToString(),
                            RegionName = s.Region.ToString(),
                            YearName = a.Year,
                            EIN = s.EIN,
                            PTranspotation = a.PTranspotation,
                            PJobTrain = a.PJobTrain,
                            PEducationAssistance = a.PEducationAssistance,
                            PResidentialCare = a.PResidentialCare,
                            PUtilities = a.PUtilities,
                            PHousingEmergency = a.PHousingEmergency,
                            PHousingAssistance = a.PHousingAssistance,
                            PChildCare = a.PChildCare,
                            PClothing = a.PClothing,
                            PFood = a.PFood,
                            PSupplies = a.PSupplies,
                            POther = a.POther,
                            POther2 = a.POther2,
                            POther3 = a.POther3,
                            PTotals = a.PTotals
                        };

            if (!User.IsInRole("Admin"))
            {
                var id = User.Identity.GetUserId();

                costs = from a in db.ParticipationServices
                        join s in db.SubContractors on a.SubcontractorId equals s.SubcontractorId
                        join us in db.Users on s.SubcontractorId equals us.SubcontractorId
                        where a.SubcontractorId == s.SubcontractorId && us.Id == id
                        select new ParticipationServiceReport
                        {
                            OrgName = a.Subcontractor.OrgName,
                            MonthName = a.Month.ToString(),
                            RegionName = s.Region.ToString(),
                            YearName = a.Year,
                            EIN = s.EIN,
                            PTranspotation = a.PTranspotation,
                            PJobTrain = a.PJobTrain,
                            PEducationAssistance = a.PEducationAssistance,
                            PResidentialCare = a.PResidentialCare,
                            PUtilities = a.PUtilities,
                            PHousingEmergency = a.PHousingEmergency,
                            PHousingAssistance = a.PHousingAssistance,
                            PChildCare = a.PChildCare,
                            PClothing = a.PClothing,
                            PFood = a.PFood,
                            PSupplies = a.PSupplies,
                            POther = a.POther,
                            POther2 = a.POther2,
                            POther3 = a.POther3,
                            PTotals = a.PTotals
                        };

            }

            foreach (var item in costs.OrderBy(a => a.OrgName))
            {
                dt.Rows.Add(item.OrgName, item.MonthName, item.RegionName, item.YearName, item.EIN, item.PTranspotation, item.PJobTrain,
                    item.PJobTrain, item.PResidentialCare, item.PUtilities, item.PHousingEmergency, item.PHousingAssistance, item.PChildCare,
                    item.PClothing, item.PFood, item.PSupplies, item.POther, item.POther2, item.POther3, item.PTotals);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Total Cost Report.xlsx");
                }
            }
        }
      
        //exporting Administration cost
        public FileResult ExportAdmincost()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[27]
            {
                new DataColumn ("Administration Invoice Id"),
                new DataColumn ("Date Submitted"),
                new DataColumn ("Organization"),
                new DataColumn ("Month"),
                new DataColumn ("Region"),
                new DataColumn ("Year"),
                new DataColumn ("AflBillable"),
                new DataColumn ("Salary/Wages"),
                new DataColumn ("Employee Benefits"),
                new DataColumn ("Employee Travel"),
                new DataColumn ("Employee Training"),
                new DataColumn ("Office Rent"),
                new DataColumn ("Office Utilities"),
                new DataColumn ("Facility Insurance"),
                new DataColumn ("Office Supplies"),
                new DataColumn ("Equipment"),
                new DataColumn ("Office Communications"),
                new DataColumn ("Office Maintenance"),
                new DataColumn ("Consulting Fees"),
                new DataColumn ("Janitor Services"),
                new DataColumn ("Depreciation"),
                new DataColumn ("Technical Support"),
                new DataColumn ("Security Services"),
                new DataColumn ("Other"),
                new DataColumn ("Other 2"),
                new DataColumn ("Other 3"),
                new DataColumn ("Total Costs"),
            });

            var costs = from a in db.AdminCosts
                        join s in db.SubContractors on a.SubcontractorId equals s.SubcontractorId
                        join us in db.Users on s.SubcontractorId equals us.SubcontractorId
                        where a.SubcontractorId == s.SubcontractorId
                        select new AdminReport
                        {
                            AdminCostId = a.AdminCostId,
                            SubmittedDate = a.SubmittedDate,
                            OrgName = s.OrgName,
                            MonthName = a.Month.ToString(),
                            YearName = a.Year,
                            AflBillable = a.AflBillable,
                            ASalandWages = a.ASalandWages,
                            AEmpBenefits = a.AEmpBenefits,
                            AEmpTravel = a.AEmpTravel,
                            AEmpTraining = a.AEmpTraining,
                            AOfficeRent = a.AOfficeRent,
                            AOfficeUtilities = a.AOfficeUtilities,
                            AFacilityIns = a.AFacilityIns,
                            AOfficeSupplies = a.AOfficeSupplies,
                            AEquipment = a.AEquipment,
                            AOfficeCommunications = a.AOfficeCommunications,
                            AOfficeMaint = a.AOfficeMaint,
                            AConsulting = a.AConsulting,
                            AJanitorServices = a.AJanitorServices,
                            ADepreciation = a.ADepreciation,
                            ATechSupport = a.ATechSupport,
                            ASecurityServices = a.ASecurityServices,
                            AOther = a.AOther,
                            AOther2 = a.AOther2,
                            AOther3 = a.AOther3,
                            ATotCosts = a.ATotCosts
                        };

            if (!User.IsInRole("Admin"))
            {
                var id = User.Identity.GetUserId();

                costs = from a in db.AdminCosts
                        join s in db.SubContractors on a.SubcontractorId equals s.SubcontractorId
                        join us in db.Users on s.SubcontractorId equals us.SubcontractorId
                        where a.SubcontractorId == s.SubcontractorId && us.Id == id
                        select new AdminReport
                        {
                            AdminCostId = a.AdminCostId,
                            SubmittedDate = a.SubmittedDate,
                            OrgName = s.OrgName,
                            MonthName = a.Month.ToString(),
                            YearName = a.Year,
                            AflBillable = a.AflBillable,
                            ASalandWages = a.ASalandWages,
                            AEmpBenefits = a.AEmpBenefits,
                            AEmpTravel = a.AEmpTravel,
                            AEmpTraining = a.AEmpTraining,
                            AOfficeRent = a.AOfficeRent,
                            AOfficeUtilities = a.AOfficeUtilities,
                            AFacilityIns = a.AFacilityIns,
                            AOfficeSupplies = a.AOfficeSupplies,
                            AEquipment = a.AEquipment,
                            AOfficeCommunications = a.AOfficeCommunications,
                            AOfficeMaint = a.AOfficeMaint,
                            AConsulting = a.AConsulting,
                            AJanitorServices = a.AJanitorServices,
                            ADepreciation = a.ADepreciation,
                            ATechSupport = a.ATechSupport,
                            ASecurityServices = a.ASecurityServices,
                            AOther = a.AOther,
                            AOther2 = a.AOther2,
                            AOther3 = a.AOther3,
                            ATotCosts = a.ATotCosts
                        };

            }
            foreach (var item in costs)
            {
                dt.Rows.Add(item.AdminCostId, item.SubmittedDate, item.OrgName, item.MonthName, item.RegionName, item.YearName, item.AflBillable, item.ASalandWages, item.AEmpBenefits,
                    item.AEmpTravel, item.AEmpTraining, item.AOfficeRent, item.AOfficeUtilities, item.AFacilityIns, item.AOfficeSupplies, item.AEquipment,
                    item.AOfficeCommunications, item.AOfficeMaint, item.AConsulting, item.AJanitorServices, item.ADepreciation,
                    item.ATechSupport, item.ASecurityServices, item.AOther, item.AOther2, item.AOther3, item.ATotCosts);
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
