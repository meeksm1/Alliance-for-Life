using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class ContractorFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorFormController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new ContractorFormViewModel
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