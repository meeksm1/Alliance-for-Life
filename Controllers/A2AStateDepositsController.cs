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
    public class A2AStateDepositsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: A2AStateDeposits
        public ActionResult Index()
        {
            return View(db.StateDeposit.ToList());
        }

        // GET: A2AStateDeposits/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            A2AStateDeposits a2AStateDeposits = db.StateDeposit.Find(id);
            if (a2AStateDeposits == null)
            {
                return HttpNotFound();
            }
            return View(a2AStateDeposits);
        }

        // GET: A2AStateDeposits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: A2AStateDeposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "A2AStateDepositsId,StateDeposits,Month,Year")] A2AStateDeposits a2AStateDeposits)
        {
            if (ModelState.IsValid)
            {
                a2AStateDeposits.A2AStateDepositsId = Guid.NewGuid();
                db.StateDeposit.Add(a2AStateDeposits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(a2AStateDeposits);
        }

        // GET: A2AStateDeposits/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            A2AStateDeposits a2AStateDeposits = db.StateDeposit.Find(id);
            if (a2AStateDeposits == null)
            {
                return HttpNotFound();
            }
            return View(a2AStateDeposits);
        }

        // POST: A2AStateDeposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "A2AStateDepositsId,StateDeposits,Month,Year")] A2AStateDeposits a2AStateDeposits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(a2AStateDeposits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(a2AStateDeposits);
        }

        // GET: A2AStateDeposits/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            A2AStateDeposits a2AStateDeposits = db.StateDeposit.Find(id);
            if (a2AStateDeposits == null)
            {
                return HttpNotFound();
            }
            return View(a2AStateDeposits);
        }

        // POST: A2AStateDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            A2AStateDeposits a2AStateDeposits = db.StateDeposit.Find(id);
            db.StateDeposit.Remove(a2AStateDeposits);
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
