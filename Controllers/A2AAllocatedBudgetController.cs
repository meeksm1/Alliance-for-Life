using Alliance_for_Life.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class A2AAllocatedBudgetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: A2AAllocatedBudget
        public ActionResult Index()
        {
            return View(db.AFLAllocation.ToList());
        }

        // GET: A2AAllocatedBudget/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            A2AAllocatedBudget a2AAllocatedBudget = db.AFLAllocation.Find(id);
            if (a2AAllocatedBudget == null)
            {
                return HttpNotFound();
            }
            return View(a2AAllocatedBudget);
        }

        // GET: A2AAllocatedBudget/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: A2AAllocatedBudget/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "A2AAllocatedBudgetId,Year,BeginingBalance,StateDeposits,Month")] A2AAllocatedBudget a2AAllocatedBudget)
        {
            if (ModelState.IsValid)
            {
                a2AAllocatedBudget.A2AAllocatedBudgetId = Guid.NewGuid();
                db.AFLAllocation.Add(a2AAllocatedBudget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(a2AAllocatedBudget);
        }

        // GET: A2AAllocatedBudget/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            A2AAllocatedBudget a2AAllocatedBudget = db.AFLAllocation.Find(id);
            if (a2AAllocatedBudget == null)
            {
                return HttpNotFound();
            }
            return View(a2AAllocatedBudget);
        }

        // POST: A2AAllocatedBudget/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "A2AAllocatedBudgetId,Year,BeginingBalance,StateDeposits,Month")] A2AAllocatedBudget a2AAllocatedBudget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(a2AAllocatedBudget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(a2AAllocatedBudget);
        }

        // GET: A2AAllocatedBudget/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            A2AAllocatedBudget a2AAllocatedBudget = db.AFLAllocation.Find(id);
            if (a2AAllocatedBudget == null)
            {
                return HttpNotFound();
            }
            return View(a2AAllocatedBudget);
        }

        // POST: A2AAllocatedBudget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            A2AAllocatedBudget a2AAllocatedBudget = db.AFLAllocation.Find(id);
            db.AFLAllocation.Remove(a2AAllocatedBudget);
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
