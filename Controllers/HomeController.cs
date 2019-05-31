﻿using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var subcontractors = db.SubContractors
             .Include(s => s.Administrator)
             .Where(s => s.Active);

            var id = User.Identity.GetUserId();

            List<Userinformation> userlist = new List<Userinformation>();
            var role = db.Roles.Include(x => x.Users).ToList();
            var organization = "";

            var usr = db.Users.Find(id);

            var usersub = db.SubContractors.Find(usr.SubcontractorId);
            organization = usersub.OrgName;
            if (organization == null)
            {
                organization = "";

            }
            foreach (var users in db.Users.Where(s => s.SubcontractorId == usersub.SubcontractorId))
            {
                var obj = new Userinformation
                {
                    Id = new Guid(users.Id),
                    Firstname = users.FirstName,
                    Lastname = users.LastName,
                    Email = users.Email,
                };

                userlist.Add(obj);
            }

            ViewBag.Orgname = organization;
            ViewBag.Users = userlist;

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