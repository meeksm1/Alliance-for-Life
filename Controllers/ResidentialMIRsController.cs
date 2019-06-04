using Alliance_for_Life.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace Alliance_for_Life.Controllers
{
    [Authorize]
    public class ResidentialMIRsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ResidentialMIRs
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, string pgSize)
        {

            int pageSize = Convert.ToInt16(pgSize);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //looking for the searchstring
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var ressearch = db.ResidentialMIRs.Include(a => a.Subcontractor);

            var nonresidental = db.NonResidentialMIRs.Include(n => n.Subcontractor);

            if (!User.IsInRole("Admin"))
            {
                var id = User.Identity.GetUserId();
                var usersubid = db.Users.Find(id).SubcontractorId;

                ressearch = from s in db.ResidentialMIRs
                            where usersubid == s.SubcontractorId
                            select s;

                nonresidental = from t in db.NonResidentialMIRs
                                where usersubid == t.SubcontractorId
                                select t;
            }
            

            if (!String.IsNullOrEmpty(searchString))
            {
                ressearch = ressearch.Where(a => a.Subcontractor.OrgName.Contains(searchString)
                || a.Subcontractor.SubmittedDate.ToString().Contains(searchString));


                nonresidental = nonresidental.Where(a => a.Subcontractor.OrgName.Contains(searchString)
                 || a.Subcontractor.SubmittedDate.ToString().Contains(searchString));
            }

            ViewBag.nonResidentialMIRs = nonresidental.ToList();

            switch (sortOrder)
            {
                case "name_desc":
                    ressearch = ressearch.OrderByDescending(s => s.Subcontractor.OrgName);
                    nonresidental = nonresidental.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    ressearch = ressearch.OrderBy(s => s.SubmittedDate);
                    nonresidental = nonresidental.OrderBy(s => s.SubmittedDate);
                    break;
                case "date_desc":
                    ressearch = ressearch.OrderByDescending(s => s.SubmittedDate);
                    nonresidental = nonresidental.OrderByDescending(s => s.SubmittedDate);
                    break;
                default:
                    ressearch = ressearch.OrderBy(s => s.Subcontractor.OrgName);
                    nonresidental = nonresidental.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(ressearch.ToPagedList(pageNumber, pageSize));
        }

        // GET: ResidentialMIRs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ResidentialMIR residentialMIR = db.ResidentialMIRs
                 .Include(s => s.Subcontractor)
                 .SingleOrDefault();

            if (residentialMIR == null)
            {
                return HttpNotFound();
            }
            return View(residentialMIR);
        }

        // GET: ResidentialMIRs/Create
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

        // POST: ResidentialMIRs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SubmittedDate,SubcontractorId,TotOverallServed,TotBedNights,TotA2AEnrollment,TotA2ABedNights,MA2Apercent,ClientsJobEduServ,ParticipatingFathers,TotEduClasses,TotClientsinEduClasses,TotCaseHrs,TotClientsCaseHrs,TotOtherClasses,Year,Month")] ResidentialMIR residentialMIR)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.ResidentialMIRs
                                where s.SubcontractorId == residentialMIR.SubcontractorId &&
                                s.Year == residentialMIR.Year &&
                                s.Month == residentialMIR.Month
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    residentialMIR.SubmittedDate = DateTime.Now;
                    residentialMIR.Id = Guid.NewGuid();
                    db.ResidentialMIRs.Add(residentialMIR);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", residentialMIR.SubcontractorId);

            return View(residentialMIR);
        }

        // GET: ResidentialMIRs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResidentialMIR residentialMIR = db.ResidentialMIRs.Find(id);
            if (residentialMIR == null)
            {
                return HttpNotFound();
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");

            return View(residentialMIR);
        }

        // POST: ResidentialMIRs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubmittedDate,SubcontractorId,TotBedNights,TotOverallServed,TotA2AEnrollment,TotA2ABedNights,MA2Apercent,ClientsJobEduServ,ParticipatingFathers,TotEduClasses,TotClientsinEduClasses,TotCaseHrs,TotClientsCaseHrs,TotOtherClasses,Year,Month")] ResidentialMIR residentialMIR)
        {
            if (ModelState.IsValid)
            {
                residentialMIR.SubmittedDate = DateTime.Now;
                db.Entry(residentialMIR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");

            return View(residentialMIR);
        }

        // GET: ResidentialMIRs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResidentialMIR residentialMIR = db.ResidentialMIRs
                .Include(s => s.Subcontractor)
                .SingleOrDefault();

            if (residentialMIR == null)
            {
                return HttpNotFound();
            }
            return View(residentialMIR);
        }

        // POST: ResidentialMIRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ResidentialMIR residentialMIR = db.ResidentialMIRs
                .Include(s => s.Subcontractor)
                .SingleOrDefault();

            db.ResidentialMIRs.Remove(residentialMIR);
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
