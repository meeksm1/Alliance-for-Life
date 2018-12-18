using Alliance_for_Life.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class ParticipationCostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ParticipationCost
        public ActionResult Index()
        {
            var participationServices = db.ParticipationServices.Include(p => p.Month).Include(p => p.Region).Include(p => p.Subcontractor);
            return View(participationServices.ToList());
        }

        // GET: ParticipationCost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipationService participationService = db.ParticipationServices
                .Include(a => a.Month)
                .Include(a => a.Region)
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
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions");
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");
            return View();
        }

        // POST: ParticipationCost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PSId,PTranspotation,PJobTrain,PEducationAssistance,PResidentialCare,PUtilities,PHousingEmergency,PHousingAssistance,PChildCare,PClothing,PFood,PSupplies,POther,POther2,POther3,PTotals,RegionId,MonthId,SubcontractorId")] ParticipationService participationService)
        {
            if (ModelState.IsValid)
            {
                db.ParticipationServices.Add(participationService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", participationService.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", participationService.RegionId);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", participationService.SubcontractorId);
            return View(participationService);
        }

        // GET: ParticipationCost/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", participationService.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", participationService.RegionId);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", participationService.SubcontractorId);
            return View(participationService);
        }

        // POST: ParticipationCost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PSId,PTranspotation,PJobTrain,PEducationAssistance,PResidentialCare,PUtilities,PHousingEmergency,PHousingAssistance,PChildCare,PClothing,PFood,PSupplies,POther,POther2,POther3,PTotals,RegionId,MonthId,SubcontractorId")] ParticipationService participationService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participationService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MonthId = new SelectList(db.Months, "Id", "Months", participationService.MonthId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Regions", participationService.RegionId);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", participationService.SubcontractorId);
            return View(participationService);
        }

        // GET: ParticipationCost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipationService participationService = db.ParticipationServices
                .Include(a => a.Month)
                .Include(a => a.Region)
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
        public ActionResult DeleteConfirmed(int id)
        {
            ParticipationService participationService = db.ParticipationServices
                .Include(a => a.Month)
                .Include(a => a.Region)
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
    }
}
