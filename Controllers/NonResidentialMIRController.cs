using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class NonResidentialMIRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NonResidentialMIRController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new NonResidentialMIRFormViewModel
            {
                Years = _context.Years.ToList(),
                Months = _context.Months.ToList(),
                Subcontractors = _context.SubContractors.ToList()
            };
            return View(viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NonResidentialMIRFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Years = _context.Years.ToList();
                viewModel.Months = _context.Months.ToList();
                viewModel.Subcontractors = _context.SubContractors.ToList();
                return View("Create", viewModel);
            }


            var invoice = new NonResidentialMIR
            {
                Subcontractor = viewModel.Subcontractor,
                MonthId = viewModel.Month,
                YearId = viewModel.Year,
                TotBedNights = viewModel.TotBedNights,
                TotA2AEnrollment = viewModel.TotA2AEnrollment,
                TotA2ABedNights = viewModel.TotA2ABedNights,
                MA2Apercent = viewModel.MA2Apercent
            };

            _context.NonResidentialMIRs.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        //Get Report Data
        public ActionResult Reports()
        {
            var report = from res in _context.NonResidentialMIRs
                         join s in _context.SubContractors on res.Subcontractor equals s.SubcontractorId
                         join m in _context.Months on res.Months.Id equals m.Id
                         join y in _context.Years on res.YearId equals y.Id
                         where res.Id > 0
                         select new MIRReport
                         {
                             Id = res.Id,
                             OrgName = s.OrgName,
                             Month = m.Months,
                             YearName = y.Years,
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
            dt.Columns.AddRange(new DataColumn[15]
            {
                new DataColumn ("Report Id"),
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

            var report = from res in _context.NonResidentialMIRs
                         join s in _context.SubContractors on res.Subcontractor equals s.SubcontractorId
                         join m in _context.Months on res.Months.Id equals m.Id
                         join y in _context.Years on res.YearId equals y.Id
                         where res.Id > 0
                         select new MIRReport
                         {
                             Id = res.Id,
                             OrgName = s.OrgName,
                             Month = m.Months,
                             YearName = y.Years,
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
                dt.Rows.Add(item.Id, item.OrgName, item.Month, item.YearName, item.TotBedNights, item.TotA2AEnrollment, item.TotA2ABedNights,
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