using Alliance_for_Life.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    [Authorize]
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var admin = _context.Users.SingleOrDefault(u => u.Id == userId && u.isAdmin == 1);
            var user = _context.Users.SingleOrDefault(u => u.Id == userId && u.isAdmin == 0);
            var x = admin;
            if (admin == null)
            {
                x = user;
            }

            var gig = new Authentication
            {

                AdminNavProp = x

            };
            return View(gig);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
