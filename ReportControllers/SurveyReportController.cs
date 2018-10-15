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
        // GET: SurveyReport
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return View(from surveys in db.Surveys.Take(24) join sc in db.SubContractors.Take(24)
                        on surveys.SubcontractorId equals sc.SubcontractorId
                        select new { surveys.Months, surveys.OrgName, surveys.SurveyId, surveys.SurveysCompleted });
        }

        [HttpPost]
        public FileResult Export()
        {
           ApplicationDbContext db = new ApplicationDbContext();
           DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5]
            {
                new DataColumn ("Survey ID"),
                new DataColumn ("Month"),
                new DataColumn ("Organization"),
                new DataColumn ("Date"),
                new DataColumn ("Surveys Returned")
            });

            var query = from s in db.Surveys.Take(24)
                          select new { s.Months.Months, s.OrgName, s.SurveyId, s.SurveysCompleted, s.Date };           

            foreach (var item in query)
            {
                dt.Rows.Add(item.Date, item.Months, item.OrgName, item.SurveysCompleted, item.SurveyId);
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