using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var subcontractors = _context.SubContractors
             .Include(s => s.Administrator)
             .Where(s => s.Active);

            if (!User.IsInRole("Admin"))
            {
                var id = User.Identity.GetUserId();
                subcontractors = from s in _context.SubContractors
                                        join a in _context.Users on s.SubcontractorId equals a.SubcontractorId
                                        where id == a.Id
                                        select s;
            }

            return View(subcontractors);
        }

 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [WordDocument]
        public ActionResult Print()
        {
            ViewBag.WordDocumentFilename = "AboutMeDocument";
            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}