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
    public class NonResidentialMIRController : Controller
    {
        private readonly ApplicationDbContext db;

        public NonResidentialMIRController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);

            var viewModel = new NonResidentialMIRFormViewModel
            {
                Subcontractors = db.SubContractors.ToList()
            };
            return View(viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NonResidentialMIRFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.SubmittedDate = DateTime.UtcNow;
                viewModel.Subcontractors = db.SubContractors.ToList();
                return View("Create", viewModel);
            }

            var datelist = Enumerable.Range(System.DateTime.Now.Year - 4, 10).ToList();
            ViewBag.Year = new SelectList(datelist);

            var invoice = new NonResidentialMIR
            {
                Id = Guid.NewGuid(),
                SubcontractorId = viewModel.SubcontractorId,
                Months = viewModel.Month,
                Year = viewModel.Year,
                TotBedNights = viewModel.TotBedNights,
                TotA2AEnrollment = viewModel.TotA2AEnrollment,
                TotA2ABedNights = viewModel.TotA2ABedNights,
                MA2Apercent = viewModel.MA2Apercent,
                ClientsJobEduServ = viewModel.ClientsJobEduServ,
                ParticipatingFathers = viewModel.ParticipatingFathers,
                TotEduClasses = viewModel.TotEduClasses,
                TotClientsinEduClasses = viewModel.TotClientsinEduClasses,
                TotCaseHrs = viewModel.TotCaseHrs,
                TotClientsCaseHrs = viewModel.TotClientsCaseHrs,
                TotOtherClasses = viewModel.TotOtherClasses,
                SubmittedDate = DateTime.Now
            };

            db.NonResidentialMIRs.Add(invoice);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
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
                             Month = res.Months.ToString(),
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
                             Month = res.Months.ToString(),
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
                dt.Rows.Add(item.Id,item.SubmittedDate, item.OrgName, item.Month, item.YearName, item.TotBedNights, item.TotA2AEnrollment, item.TotA2ABedNights,
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