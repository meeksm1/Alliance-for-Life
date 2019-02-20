using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class ResidentialMIRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidentialMIRController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.YearId = new SelectList(datelist);

            var viewModel = new ResidentialMIRFormViewModel
            {
                Months = _context.Months.ToList(),
                Subcontractors = _context.SubContractors.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResidentialMIRFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.SubmittedDate = DateTime.Now;
                viewModel.Months = _context.Months.ToList();
                viewModel.Subcontractors = _context.SubContractors.ToList();
                return View("Create", viewModel);
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.YearId = new SelectList(datelist);

            var invoice = new ResidentialMIR
            {
                Subcontractor = viewModel.Subcontractor,
                MonthId = viewModel.Month,
                YearId = viewModel.YearId,
                TotBedNights = viewModel.TotBedNights,
                TotA2AEnrollment = viewModel.TotA2AEnrollment,
                TotA2ABedNights = viewModel.TotA2ABedNights,
                MA2Apercent = viewModel.MA2Apercent
            }; 

            _context.ResidentialMIRs.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        //Get Report Data
        public ActionResult Reports()
        {
            var report = from res in _context.ResidentialMIRs
                          join s in _context.SubContractors on res.Subcontractor equals s.SubcontractorId  
                          join m in _context.Months on res.Months.Id equals m.Id
                          where res.Id > 0
                          select new MIRReport
                          {
                                Id = res.Id,
                                OrgName = s.OrgName,
                                SubmittedDate = res.SubmittedDate,
                                Month = m.Months,
                                YearName = res.YearId,
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
                new DataColumn ("Organization"),
                new DataColumn ("Date Submitted"),
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

            var report = from res in _context.ResidentialMIRs
                         join s in _context.SubContractors on res.Subcontractor equals s.SubcontractorId
                         join m in _context.Months on res.Months.Id equals m.Id
                         where res.Id > 0
                         select new MIRReport
                         {
                             Id = res.Id,
                             SubmittedDate = res.SubmittedDate,
                             OrgName = s.OrgName,
                             Month = m.Months,
                             YearName = res.YearId,
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