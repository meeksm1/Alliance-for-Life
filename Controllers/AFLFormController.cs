using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class AFLFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AFLFormController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new AFLFormViewModel
            {

            };
            return View(viewModel);
        }

        public ActionResult Reports()
        {
            var viewModel = new AFLFormViewModel
            {

            };
            return View(viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}