using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alliance_for_Life.Models;

namespace Alliance_for_Life.Controllers
{
    public class MonthlyServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MonthlyServices
        public ActionResult Index()
        {
            var monthlyServices = db.MonthlyServices.Include(m => m.Subcontractor);
            return View(monthlyServices.ToList());
        }

        // GET: MonthlyServices/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyServices monthlyServices = db.MonthlyServices.Find(id);
            if (monthlyServices == null)
            {
                return HttpNotFound();
            }
            return View(monthlyServices);
        }

        // GET: MonthlyServices/Create
        public ActionResult CreateR()
        {
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");

            return View();
        }

        // POST: MonthlyServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateR([Bind(Include = "Id,SubmittedDate,SubcontractorId,RTotBedNights,RTotA2AEnrollment,RTotA2ABedNights,RMA2Apercent,RClientsJobEduServ,RParticipatingFathers,RTotEduClasses,RTotClientsinEduClasses,RTotCaseHrs,RTotClientsCaseHrs,RTotOtherClasses,NTotA2AEnrollment,NMA2Apercent,NClientsJobEduServ,NParticipatingFathers,NTotEduClasses,NTotClientsinEduClasses,NTotCaseHrs,NTotClientsCaseHrs,NTotOtherClasses,Year,Month")] MonthlyServices monthlyServices)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.MonthlyServices
                                where s.SubcontractorId == monthlyServices.SubcontractorId &&
                                s.Year == monthlyServices.Year &&
                                s.Month == monthlyServices.Month
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    monthlyServices.SubmittedDate = DateTime.Now;
                    monthlyServices.Id = Guid.NewGuid();
                    db.MonthlyServices.Add(monthlyServices);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", monthlyServices.SubcontractorId);

            return View(monthlyServices);
        }


        // GET: MonthlyServices/Create
        public ActionResult CreateN()
        {
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");

            return View();
        }

        // POST: MonthlyServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateN([Bind(Include = "Id,SubmittedDate,SubcontractorId,RTotBedNights,RTotA2AEnrollment,RTotA2ABedNights,RMA2Apercent,RClientsJobEduServ,RParticipatingFathers,RTotEduClasses,RTotClientsinEduClasses,RTotCaseHrs,RTotClientsCaseHrs,RTotOtherClasses,NTotA2AEnrollment,NMA2Apercent,NClientsJobEduServ,NParticipatingFathers,NTotEduClasses,NTotClientsinEduClasses,NTotCaseHrs,NTotClientsCaseHrs,NTotOtherClasses,Year,Month")] MonthlyServices monthlyServices)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.MonthlyServices
                                where s.SubcontractorId == monthlyServices.SubcontractorId &&
                                s.Year == monthlyServices.Year &&
                                s.Month == monthlyServices.Month
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    monthlyServices.SubmittedDate = DateTime.Now;
                    monthlyServices.Id = Guid.NewGuid();
                    db.MonthlyServices.Add(monthlyServices);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", monthlyServices.SubcontractorId);

            return View(monthlyServices);
        }

        // GET: MonthlyServices/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyServices monthlyServices = db.MonthlyServices.Find(id);
            if (monthlyServices == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "AdministratorId", monthlyServices.SubcontractorId);
            return View(monthlyServices);
        }

        // POST: MonthlyServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubmittedDate,SubcontractorId,RTotBedNights,RTotA2AEnrollment,RTotA2ABedNights,RMA2Apercent,RClientsJobEduServ,RParticipatingFathers,RTotEduClasses,RTotClientsinEduClasses,RTotCaseHrs,RTotClientsCaseHrs,RTotOtherClasses,NTotA2AEnrollment,NMA2Apercent,NClientsJobEduServ,NParticipatingFathers,NTotEduClasses,NTotClientsinEduClasses,NTotCaseHrs,NTotClientsCaseHrs,NTotOtherClasses,Year,Month")] MonthlyServices monthlyServices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlyServices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "AdministratorId", monthlyServices.SubcontractorId);
            return View(monthlyServices);
        }

        // GET: MonthlyServices/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyServices monthlyServices = db.MonthlyServices.Find(id);
            if (monthlyServices == null)
            {
                return HttpNotFound();
            }
            return View(monthlyServices);
        }

        // POST: MonthlyServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MonthlyServices monthlyServices = db.MonthlyServices.Find(id);
            db.MonthlyServices.Remove(monthlyServices);
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
