using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    [Authorize]
    public class ContractorFormController : Controller
    {
        private readonly ApplicationDbContext db;

        public ContractorFormController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new ContractorFormViewModel
            {
               
            };
            return View(viewModel);
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        

       
    }
}