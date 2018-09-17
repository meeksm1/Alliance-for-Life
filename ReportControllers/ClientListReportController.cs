using Alliance_for_Life.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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

        List<ClientList> client = new List<ClientList>();
        List<SubContractor> sub = new List<SubContractor>();
        public ActionResult Index()
        {
            var clientListReportViewModel = from c in client
                                            join s in sub on c.Subcontractor equals s.ID 
                                            select new ClientListReportViewModel { clientListVm = c, subcontractorVm = s };

            return View(clientListReportViewModel);

        }

    }
}