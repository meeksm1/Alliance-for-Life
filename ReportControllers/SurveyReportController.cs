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
            return View(from surveys in db.Surveys.Take(10) select surveys);
        }

        [HttpPost]
        public FileResult Export()
        {
           ApplicationDbContext db = new ApplicationDbContext();
           DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4]
            {
                new DataColumn ("Month"),
                new DataColumn ("Organization"),
                new DataColumn ("Date"),
                new DataColumn ("Surveys Returned")
            });

            var surveys = from survey in db.Surveys.Take(10) select survey;

            foreach (var survey in surveys)
            {
                dt.Rows.Add(survey.Months, survey.OrgName, survey.Date, survey.SurveysCompleted);
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