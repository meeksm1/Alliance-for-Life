using Alliance_for_Life.Models;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using System;
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
        public ActionResult Index()
        {
            var participationServices = db.ParticipationServices.Include(p => p.Subcontractor);
            if (!User.IsInRole("Admin"))
            {
                var id = User.Identity.GetUserId();
                var usersubid = db.Users.Find(id).SubcontractorId;

                participationServices = from s in participationServices
                                        where usersubid == s.SubcontractorId
                                        select s;
            }
            return View(participationServices.ToList());
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

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(list, "SubcontractorId", "OrgName");

            return View();
        }

        // POST: ParticipationCost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PSId,PTranspotation,PJobTrain,PEducationAssistance,PResidentialCare,PUtilities,PHousingEmergency,PHousingAssistance,PChildCare,PClothing,PFood,PSupplies,POther,POther2,POther3,PTotals,Region,Month,SubcontractorId,Year,SubmittedDate")] ParticipationService participationService)
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
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", participationService.SubcontractorId);
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

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", participationService.SubcontractorId);
            return View(participationService);
        }

        // POST: ParticipationCost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PSId,PTranspotation,PJobTrain,PEducationAssistance,PResidentialCare,PUtilities,PHousingEmergency,PHousingAssistance,PChildCare,PClothing,PFood,PSupplies,POther,POther2,POther3,PTotals,Region,Month,SubcontractorId,Year,SubmittedDate")] ParticipationService participationService)
        {
            if (ModelState.IsValid)
            {
                participationService.SubmittedDate = DateTime.Now;
                db.Entry(participationService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", participationService.SubcontractorId);
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

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[21]
            {
                new DataColumn ("Participation Invoice Id"),
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
                            PSId = a.PSId,
                            OrgName = s.OrgName,
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

            foreach (var item in costs)
            {
                dt.Rows.Add(item.PSId, item.MonthName, item.RegionName, item.YearName, item.EIN, item.PTranspotation, item.PJobTrain,
                    item.PJobTrain, item.PResidentialCare, item.PUtilities, item.PHousingEmergency, item.PHousingAssistance, item.PChildCare,
                    item.PClothing, item.PFood, item.PSupplies, item.POther, item.POther2, item.POther3, item.PTotals);
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
