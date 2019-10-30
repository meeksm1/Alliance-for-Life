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
    public class DirectDepositsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DirectDeposits
        public ActionResult Index()
        {



            return View(db.DirectDeposit.ToList());
        }

        // GET: DirectDeposits/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectDeposits directDeposits = db.DirectDeposit.Find(id);
            if (directDeposits == null)
            {
                return HttpNotFound();
            }
            return View(directDeposits);
        }

        // GET: DirectDeposits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DirectDeposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DirectDepositsId,IsCheked")] DirectDeposits directDeposits)
        {
            if (ModelState.IsValid)
            {
                directDeposits.DirectDepositsId = Guid.NewGuid();
                db.DirectDeposit.Add(directDeposits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(directDeposits);
        }

        // GET: DirectDeposits/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectDeposits directDeposits = db.DirectDeposit.Find(id);
            if (directDeposits == null)
            {
                return HttpNotFound();
            }
            return View(directDeposits);
        }

        // POST: DirectDeposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DirectDepositsId,IsCheked")] DirectDeposits directDeposits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directDeposits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(directDeposits);
        }

        // GET: DirectDeposits/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectDeposits directDeposits = db.DirectDeposit.Find(id);
            if (directDeposits == null)
            {
                return HttpNotFound();
            }
            return View(directDeposits);
        }

        // POST: DirectDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DirectDeposits directDeposits = db.DirectDeposit.Find(id);
            db.DirectDeposit.Remove(directDeposits);
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
