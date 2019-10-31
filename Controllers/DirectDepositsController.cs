using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using PagedList;

namespace Alliance_for_Life.Controllers
{
    public class DirectDepositsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DirectDeposits
        public ActionResult Index(string sortOrder, Guid? searchString, string Month, int? Year, string currentFilter, int? page, int? pgSize)
        {
            DirectDeposits directdepo = new DirectDeposits();
           
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
            //assign default values if Year is empty
            if (String.IsNullOrEmpty(Year.ToString()))
            {
                Year = DateTime.Now.Year;
            }

            //assign default values if Month is empty
            if (String.IsNullOrEmpty(Month))
            {
                Month = DateTime.Now.Month.ToString();
                ViewBag.Title = "Direct Deposit Summary for " + DateTime.Now.ToString("MMMM") + "-" + Year;
            }
            else
            {
                ViewBag.Title = "Direct Deposit Summary for " + Month + "-" + Year;
            }

           

            //title
           

           //calculating the direct deposit per month 



           var directdeposit = from a in db.AdminCosts
                                join p in db.ParticipationServices on a.SubcontractorId equals p.SubcontractorId
                                where (a.Year == Year && p.Year == Year) && (a.Month.ToString() == Month && p.Month.ToString() == Month)
                                select new DirectDepositView
                                {
                                    AdminCost = a,
                                    ParticipationService = p
                                };

            //check to see if there is data stored on the database

            foreach(var items in directdeposit.ToList())
            {
                if (checkdeposit(items.AdminCost.AdminCostId, items.ParticipationService.PSId) == true)
                {
                    //create if the data does not exists
                    Create(directdepo, items.AdminCost.AdminCostId, items.ParticipationService.PSId);
                }
            }

            //return data from the database
            var depodb = db.DirectDeposit.ToList();

            if (!String.IsNullOrEmpty(Month) || !String.IsNullOrEmpty(Year.ToString()) || !String.IsNullOrEmpty(searchString.ToString()))
            {
                var yearSearch = (Year);

                // if Org is empty
                if (String.IsNullOrEmpty(searchString.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    depodb = depodb.Where(r => r.AdminCost.Month == (Months)regionSearch && r.AdminCost.Year == yearSearch).ToList();

                    //directdeposit = directdeposit.Where(r => r.AdminCost.Month == (Months)regionSearch && r.AdminCost.Year == yearSearch);
                }
                // if none are empty
                else
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    depodb = depodb.Where(r => r.AdminCost.Month == (Months)regionSearch && r.AdminCost.Year == yearSearch && r.AdminCost.Subcontractor.SubcontractorId == searchString).ToList();
                }
            }
            else
            {
                depodb = depodb.Where(r => r.AdminCost.Month == (Months)DateTime.Now.Month && r.AdminCost.Year == DateTime.Now.Year).ToList();

            }

            //sorting
            switch (sortOrder)
            {
                case "name_desc":
                    depodb = depodb.OrderByDescending(s => s.AdminCost.Subcontractor.OrgName).ToList();
                    break;
                default:
                    break;
            }

            ViewBag.CurrentFilter = searchString;
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

            return View(depodb.ToPagedList(pageNumber, defaSize));
        }

        //checking to see if the data exists on the table
        public Boolean checkdeposit(Guid AdminID, Guid PatriID )
        {
            var adddeposit = false;
            var deposit = db.DirectDeposit
                .Where(a => a.AdminCost.AdminCostId == AdminID)
                .Where(a => a.ParticipationService.PSId == PatriID).ToList();

            if(deposit.Count() == 0)
            {
                adddeposit = true;
            }

            return adddeposit;
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
        public ActionResult Create(DirectDeposits directDeposits, Guid AdminId, Guid PartiID)
        {
            directDeposits = new DirectDeposits();

            if (ModelState.IsValid)
            {
                directDeposits.DirectDepositsId = Guid.NewGuid();
                directDeposits.AdminCostId = AdminId;
                directDeposits.PSId = PartiID;
                db.DirectDeposit.Add(directDeposits);
                db.SaveChanges();
                return View();
            }
            return View();
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
                directDeposits.AdminCostId = new Guid(Request["AdminCostId"]);
                directDeposits.PSId = new Guid(Request["PSId"]);
                db.Entry(directDeposits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(directDeposits);
        }

        // GET: DirectDeposits/Delete/5
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //export excel sheets
        [HttpPost]
        public FileResult Export()
        {
            string Month = Request["Month"];
            string Year = Request["Year"];
            //assign default values if Month is empty
            if (String.IsNullOrEmpty(Month))
            {
                Month = DateTime.Now.ToString("MMM");
            }
            //assign default values if Year is empty
            if (String.IsNullOrEmpty(Year))
            {
                Year = DateTime.Now.Year.ToString();
            }
            DataTable dt = new DataTable("Direct Deposit");
            dt.Columns.AddRange(new DataColumn[8]
            {
                new DataColumn ("EIN"),
                new DataColumn ("Organization"),
                new DataColumn ("Region"),
                new DataColumn ("Amount"),
                new DataColumn ("Direct Admin Cost"),
                //admincost
                new DataColumn ("3%"),
                new DataColumn ("Subcontractor Direct Deposit Total"),
                new DataColumn ("√")
            });

            var directdeposit = from a in db.AdminCosts
                                join p in db.ParticipationServices on a.SubcontractorId equals p.SubcontractorId
                                where (a.Year.ToString() == Year && p.Year.ToString() == Year) && (a.Month.ToString() == Month && p.Month.ToString() == Month)
                                select new DirectDepositView
                                {
                                    AdminCost = a,
                                    ParticipationService = p
                                };


            //orderby year / month / orgname
            foreach (var item in directdeposit.OrderBy(a => a.AdminCost.Subcontractor.OrgName))
            {
                dt.Rows.Add(item.AdminCost.Subcontractor.EIN, item.AdminCost.Subcontractor.OrgName, item.AdminCost.Subcontractor.Region, item.AdminCost.ATotCosts + item.ParticipationService.PTotals, item.AdminCost.ATotCosts
                   , item.AdminCost.ATotCosts, item.AdminCost.ATotCosts * 0.03, item.AdminCost.ATotCosts + item.ParticipationService.PTotals - item.AdminCost.ATotCosts * 0.03);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Direct Deposit.xlsx");
                }
            }
        }
    }
}
