using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{

    public class DirectDepositController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DirectDeposit
        public ActionResult Index(string sortOrder, Guid? searchString, string Month, int? Year, string currentFilter, int? page, int? pgSize)
        {
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

            //assign default values if Month is empty
            if (String.IsNullOrEmpty(Month))
            {
                Month = DateTime.Now.Month.ToString();
            }

            //assign default values if Year is empty
            if (String.IsNullOrEmpty(Year.ToString()))
            {
                Year = DateTime.Now.Year;
            }
            //calculating the direct deposit per month 

            var directdeposit = from a in db.AdminCosts
                                join p in db.ParticipationServices on a.SubcontractorId equals p.SubcontractorId
                               where (a.Year == Year && p.Year == Year) && (a.Month.ToString() == Month && p.Month.ToString() == Month)
                                select new DirectDeposit
                                {
                                    AdminCost = a,
                                    ParticipationService = p
                                };

            if (!String.IsNullOrEmpty(Month) || !String.IsNullOrEmpty(Year.ToString()) || !String.IsNullOrEmpty(searchString.ToString()))
            {
                var yearSearch = (Year);

                // if Org is empty
               if (String.IsNullOrEmpty(searchString.ToString()))
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    directdeposit = directdeposit.Where(r => r.AdminCost.Month == (Months)regionSearch && r.AdminCost.Year == yearSearch);
                }
                // if none are empty
                else
                {
                    var regionSearch = Enum.Parse(typeof(Months), Month);
                    directdeposit = directdeposit.Where(r => r.AdminCost.Month == (Months)regionSearch && r.AdminCost.Year == yearSearch && r.AdminCost.Subcontractor.SubcontractorId == searchString);
                }
            }
            else{
                directdeposit = directdeposit.Where(r => r.AdminCost.Month == (Months)DateTime.Now.Month && r.AdminCost.Year == DateTime.Now.Year );

            }

            //sorting
            switch (sortOrder)
            {
                case "name_desc":
                    directdeposit = directdeposit.OrderByDescending(s => s.AdminCost.Subcontractor.OrgName);
                    break;
                default:
                    directdeposit = directdeposit.OrderBy(s => s.AdminCost.Subcontractor.OrgName);
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
                
            return View(directdeposit.ToPagedList(pageNumber, defaSize));
        }


        //export excel sheets
        public FileResult Export(string Month, int? Year)
        {
            //assign default values if Month is empty
            if (String.IsNullOrEmpty(Month))
            {
                Month = DateTime.Now.Month.ToString();
            }

            //assign default values if Year is empty
            if (String.IsNullOrEmpty(Year.ToString()))
            {
                Year = DateTime.Now.Year;
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
                                where (a.Year == Year && p.Year == Year) && (a.Month.ToString() == Month && p.Month.ToString() == Month)
                                select new DirectDeposit
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