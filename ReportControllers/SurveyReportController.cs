using Alliance_for_Life.Models;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.ReportControllers
{
    public class SurveyReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SurveyReportController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: SurveyReport
        public ActionResult Index()
        {
            var query = from s in _context.Surveys
                        join sc in _context.SubContractors on s.SubcontractorId equals sc.SubcontractorId
                        join m in _context.Months on s.MonthId equals m.Id
                        select new SurveyReport
                        {
                            SurveyId = s.SurveyId,
                            Month = m.Months,
                            Orgname = sc.OrgName,
                            Date = s.Date,
                            SurveysCompleted = s.SurveysCompleted,
                        };

            return View(query);
        }

        [HttpPost]
        public FileResult Export()
        {
           DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5]
            {
                new DataColumn ("Survey ID"),
                new DataColumn ("Month"),
                new DataColumn ("Organization"),
                new DataColumn ("Date"),
                new DataColumn ("Surveys Returned")
            });

            var query = from s in _context.Surveys
                        join sc in _context.SubContractors on s.SubcontractorId equals sc.SubcontractorId
                        join m in _context.Months on s.MonthId equals m.Id
                          select new SurveyReport
                          {
                              SurveyId = s.SurveyId,
                              Month = m.Months,
                              Orgname = sc.OrgName,
                              Date = s.Date,
                              SurveysCompleted = s.SurveysCompleted,
                          };           

            foreach (var item in query)
            {
                dt.Rows.Add(item.SurveyId, item.Month, item.Orgname, item.Date, item.SurveysCompleted);
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