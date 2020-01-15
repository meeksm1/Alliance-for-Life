using Alliance_for_Life.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class A2AStateDepositsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: A2AStateDeposits
        public ActionResult Index(int? Year, string Month)
        {
            var datelist = Enumerable.Range(System.DateTime.Now.Year-1, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
            ViewBag.Month = new SelectList(Enum.GetValues(typeof(Months)).Cast<Months>());


            var statedeposit = db.StateDeposit.ToList();

            var year_search = "";

            if ((Year != null) && Year.ToString().Length > 1)
            {
                year_search = Year.ToString();
            }

            if (!String.IsNullOrEmpty(Month) || !String.IsNullOrEmpty(Year.ToString()))
            {
                var yearSearch = (Year);

                if (String.IsNullOrEmpty(Month))
                {
                    statedeposit = statedeposit.Where(r => r.Year == Year).ToList();
                }
                else if (String.IsNullOrEmpty(Year.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    statedeposit = statedeposit.Where(r => r.Month == (Months)regionSearch).ToList();
                }
                else
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    statedeposit = statedeposit.Where(r => r.Month == (Months)regionSearch && r.Year == Year).ToList();
                }
            }
            return View(statedeposit);
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
            var datelist = Enumerable.Range(System.DateTime.Now.Year-1, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
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
                var dataexist = from s in db.StateDeposit.ToList()
                                where
                                s.Month == a2AStateDeposits.Month &&
                                s.Year == a2AStateDeposits.Year
                                select s;

                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "State Budget for " + a2AStateDeposits.Month
                        + " - " + a2AStateDeposits.Year + " already exists.";

                    ModelState.Clear();
                }
                else
                {
                    a2AStateDeposits.A2AStateDepositsId = Guid.NewGuid();
                    db.StateDeposit.Add(a2AStateDeposits);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year-1, 5).ToList();
            ViewBag.Year = new SelectList(datelist);
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
