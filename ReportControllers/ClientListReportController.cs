using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.ReportControllers
{
    public class ClientListReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClientListReportController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            //var query = from c in _context.ClientLists
            //            join s in _context.SubContractors
            //            on c.SubcontractorId equals s.SubcontractorId
            //            select new
            //            {
            //                FirstName = c.FirstName,
            //                LastName = c.LastName,
            //                DOB = c.DOB,
            //                DueDate = c.DueDate,
            //                OrgName = s.OrgName,
            //                isActive = c.Active
            //            };
            //foreach (var item in query)
            //{
            //    var clientList = new ClientList
            //    {
            //        FirstName = item.FirstName,
            //        LastName = item.LastName,
            //        DOB = item.DOB,
            //        DueDate = item.DueDate,
            //        OrgName = item.OrgName,
            //        Active = item.isActive
            //    };

            //}
            return View();
        }

    }
}