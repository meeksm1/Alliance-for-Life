﻿using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using Microsoft.AspNet.Identity;
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
                Subcontractors = _context.SubContractors.ToList(),
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
                viewModel.Subcontractors = _context.SubContractors.ToList();
                return View("ClientListForm", viewModel);
            }

            var client = new ClientList
            {
                SubcontractorId = viewModel.SubcontractorId,
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
                viewModel.Subcontractors = _context.SubContractors.ToList();
                return View("ClientListForm", viewModel);
            }

            var client = _context.ClientLists.Single(s => s.Id == viewModel.Id);
            {
                client.SubcontractorId = viewModel.SubcontractorId;
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
                Subcontractors = _context.SubContractors.ToList(),
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
            var user1 = User.Identity.GetUserId();
            var nonactiveclients = from cl in _context.ClientLists
                                join su in _context.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                                join us in _context.Users on su.SubcontractorId equals us.SubcontractorId
                                where cl.Active == false && us.Id == user1
                                select new ClientListReport
                                {
                                    Id = cl.Id,
                                    Orgname = su.OrgName,
                                    FirstName = cl.FirstName,
                                    LastName = cl.LastName,
                                    DOB = cl.DOB,
                                    DueDate = cl.DueDate
                                };

            return View(nonactiveclients);
        }

        public ActionResult ActiveClients()
        {
            var user1 = User.Identity.GetUserId();
            var activeclients = from cl in _context.ClientLists
                                join su in _context.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                                join us in _context.Users on su.SubcontractorId equals us.SubcontractorId
                                where cl.Active && us.Id == user1
                                select new ClientListReport { Id = cl.Id, Orgname = su.OrgName,
                                    FirstName = cl.FirstName, LastName = cl.LastName, DOB = cl.DOB, DueDate = cl.DueDate};

            return View(activeclients);
        }

        public ActionResult Reports()
        {
            return View();
        }
    }
}