using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class ClientListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientListController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new ClientListFormViewModel
            {
                SubContractors = _context.SubContractors.ToList(),
                Heading = "Add Client Information"
            };
            return View("ClientListForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientListFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.SubContractors = _context.SubContractors.ToList();
                return View("ClientListForm", viewModel);
            }
                

            var client = new ClientList
            {
                Subcontractor = viewModel.Subcontractor,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                DOB = viewModel.GetDateTimeDOB(),
                DueDate = viewModel.GetDateTimeDueDate(),
                Active = viewModel.Active
            };

            _context.ClientLists.Add(client);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ClientListFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.SubContractors = _context.SubContractors.ToList();
                return View("ClientListForm", viewModel);
            }

            var client = _context.ClientLists.Single(s => s.Id == viewModel.Id);
            {
                client.Subcontractor = viewModel.Subcontractor;
                client.FirstName = viewModel.FirstName;
                client.LastName = viewModel.LastName;
                client.DOB = viewModel.GetDateTimeDOB();
                client.DueDate = viewModel.GetDateTimeDueDate();
                client.Active = viewModel.Active;
            };

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var client = _context.ClientLists.Single(s => s.Id == id);
            var viewModel = new ClientListFormViewModel
            {
                Heading = "Edit Client Information",
                Id = client.Id,
                SubContractors = _context.SubContractors.ToList(),
                FirstName = client.FirstName,
                LastName = client.LastName,
                DOB = client.DOB.ToString("mm/dd/yyyy"),
                DueDate = client.DueDate.ToString("mm/dd/yyyy"),
                Active = client.Active
            };
            return View("ClientListForm", viewModel);
        }

        public ActionResult NonActiveClients()
        {
            var nonactiveclients = _context.ClientLists
                .Where(c => !c.Active);
            
            return View(nonactiveclients);
        }

        public ActionResult ActiveClients()
        {
            var activeclients = _context.ClientLists
                .Where(c => c.Active);

            return View(activeclients);
        }

        public ActionResult Reports()
        {
            return View();
        }
    }
}