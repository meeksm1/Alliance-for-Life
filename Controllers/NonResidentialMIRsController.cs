﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alliance_for_Life.Models;
using ClosedXML.Excel;
using PagedList;

namespace Alliance_for_Life.Controllers
{
    public class NonResidentialMIRsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NonResidentialMIRs
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

            var nrsearch = db.NonResidentialMIRs
            .Include(a => a.Subcontractor).Where(a => a.SubcontractorId == a.Subcontractor.SubcontractorId);

            if (!String.IsNullOrEmpty(searchString))
            {
                nrsearch = nrsearch.Where(a => a.Subcontractor.OrgName.Contains(searchString)
                || a.Subcontractor.SubmittedDate.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    nrsearch = nrsearch.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    nrsearch = nrsearch.OrderBy(s => s.SubmittedDate);
                    break;
                case "date_desc":
                    nrsearch = nrsearch.OrderByDescending(s => s.SubmittedDate);
                    break;
                default:
                    nrsearch = nrsearch.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(nrsearch.ToPagedList(pageNumber, pageSize));
        }

        // GET: NonResidentialMIRs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonResidentialMIR nonResidentialMIR = db.NonResidentialMIRs
               .Include(s => s.Subcontractor)
               .SingleOrDefault();

            if (nonResidentialMIR == null)
            {
                return HttpNotFound();
            }
            return View(nonResidentialMIR);
        }

        // GET: NonResidentialMIRs/Create
        public ActionResult Create()
        {
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");

            return View();
        }

        // POST: NonResidentialMIRs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SubcontractorId,TotBedNights,TotA2AEnrollment,TotA2ABedNights,SubmittedDate,MA2Apercent,ClientsJobEduServ,ParticipatingFathers,TotEduClasses,TotClientsinEduClasses,TotCaseHrs,TotClientsCaseHrs,TotOtherClasses,Year,Months")] NonResidentialMIR nonResidentialMIR)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.NonResidentialMIRs
                                where s.SubcontractorId == nonResidentialMIR.SubcontractorId &&
                                s.Year == nonResidentialMIR.Year &&
                                s.Month == nonResidentialMIR.Month
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    nonResidentialMIR.SubmittedDate = DateTime.Now;
                    nonResidentialMIR.Id = Guid.NewGuid();
                    db.NonResidentialMIRs.Add(nonResidentialMIR);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName", nonResidentialMIR.SubcontractorId);

            return View(nonResidentialMIR);
        }

        // GET: NonResidentialMIRs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonResidentialMIR nonResidentialMIR = db.NonResidentialMIRs.Find(id);
            if (nonResidentialMIR == null)
            {
                return HttpNotFound();
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");

            return View(nonResidentialMIR);
        }

        // POST: NonResidentialMIRs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubcontractorId,TotBedNights,TotA2AEnrollment,TotA2ABedNights,SubmittedDate,MA2Apercent,ClientsJobEduServ,ParticipatingFathers,TotEduClasses,TotClientsinEduClasses,TotCaseHrs,TotClientsCaseHrs,TotOtherClasses,Year,Month")] NonResidentialMIR nonResidentialMIR)
        {
            if (ModelState.IsValid)
            {
                nonResidentialMIR.SubmittedDate = DateTime.Now;
                db.Entry(nonResidentialMIR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();

            ViewBag.Year = new SelectList(datelist);
            ViewBag.SubcontractorId = new SelectList(db.SubContractors, "SubcontractorId", "OrgName");

            return View(nonResidentialMIR);
        }

        // GET: NonResidentialMIRs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonResidentialMIR nonResidentialMIR = db.NonResidentialMIRs
                .Include(s => s.Subcontractor)
                .SingleOrDefault();

            if (nonResidentialMIR == null)
            {
                return HttpNotFound();
            }
            return View(nonResidentialMIR);
        }

        // POST: NonResidentialMIRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            NonResidentialMIR nonResidentialMIR = db.NonResidentialMIRs
             .Include(s => s.Subcontractor)
             .SingleOrDefault();

            db.NonResidentialMIRs.Remove(nonResidentialMIR);
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

        //Get Report Data
        public ActionResult Reports()
        {
            var report = from res in db.NonResidentialMIRs
                         join s in db.SubContractors on res.SubcontractorId equals s.SubcontractorId
                         select new MIRReport
                         {
                             Id = res.Id,
                             SubmittedDate = res.SubmittedDate,
                             OrgName = s.OrgName,
                             Month = res.Month.ToString(),
                             YearName = res.Year,
                             TotBedNights = res.TotBedNights,
                             TotA2AEnrollment = res.TotA2AEnrollment,
                             TotA2ABedNights = res.TotA2ABedNights,
                             MA2Apercent = res.MA2Apercent,
                             ClientsJobEduServ = res.ClientsJobEduServ,
                             ParticipatingFathers = res.ParticipatingFathers,
                             TotEduClasses = res.TotEduClasses,
                             TotClientsinEduClasses = res.TotClientsinEduClasses,
                             TotCaseHrs = res.TotCaseHrs,
                             TotClientsCaseHrs = res.TotClientsCaseHrs,
                             TotOtherClasses = res.TotOtherClasses
                         };

            return View(report);
        }

        //Export Report Data to Excel
        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[16]
            {
                new DataColumn ("Report Id"),
                new DataColumn ("Date Submitted"),
                new DataColumn ("Organization"),
                new DataColumn ("Month"),
                new DataColumn ("Year"),
                new DataColumn ("Client Bed Nights"),
                new DataColumn ("A2A Enrollment"),
                new DataColumn ("Total A2A Bed Nights"),
                new DataColumn ("Monthly A2A Clients Served"),
                new DataColumn ("Job or Educational Service"),
                new DataColumn ("Participating Fathers"),
                new DataColumn ("Prenatal & Parenting Classes Offered"),
                new DataColumn ("Prenatal & Parenting Classes Attended"),
                new DataColumn ("Case Management Hours"),
                new DataColumn ("Participants in Case Management"),
                new DataColumn ("Other Classes Offered")
            });

            var report = from res in db.NonResidentialMIRs
                         join s in db.SubContractors on res.SubcontractorId equals s.SubcontractorId
                         select new MIRReport
                         {
                             Id = res.Id,
                             SubmittedDate = res.SubmittedDate,
                             OrgName = s.OrgName,
                             Month = res.Month.ToString(),
                             YearName = res.Year,
                             TotBedNights = res.TotBedNights,
                             TotA2AEnrollment = res.TotA2AEnrollment,
                             TotA2ABedNights = res.TotA2ABedNights,
                             MA2Apercent = res.MA2Apercent,
                             ClientsJobEduServ = res.ClientsJobEduServ,
                             ParticipatingFathers = res.ParticipatingFathers,
                             TotEduClasses = res.TotEduClasses,
                             TotClientsinEduClasses = res.TotClientsinEduClasses,
                             TotCaseHrs = res.TotCaseHrs,
                             TotClientsCaseHrs = res.TotClientsCaseHrs,
                             TotOtherClasses = res.TotOtherClasses
                         };

            foreach (var item in report)
            {
                dt.Rows.Add(item.Id, item.SubmittedDate, item.OrgName, item.Month, item.YearName, item.TotBedNights, item.TotA2AEnrollment, item.TotA2ABedNights,
                    item.MA2Apercent, item.ClientsJobEduServ, item.ParticipatingFathers, item.TotEduClasses,
                    item.TotClientsinEduClasses, item.TotCaseHrs, item.TotClientsCaseHrs, item.TotOtherClasses);
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
