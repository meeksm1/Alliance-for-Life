using Alliance_for_Life.Models;
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

            if (!User.IsInRole("Admin"))
            {
                var id = User.Identity.GetUserId();
                subcontractors = from s in db.SubContractors
                                 join a in db.Users on s.SubcontractorId equals a.SubcontractorId
                                 where id == a.Id
                                 select s;
            }

            List<Userinformation> userlist = new List<Userinformation>();
            var role = db.Roles.Include(x => x.Users).ToList();
            var organization = "";
            foreach (var r in role)
            {
                foreach (var u in r.Users)
                {
                    var usr = db.Users.Find(u.UserId);

                    var usersub = db.SubContractors.Find(usr.SubcontractorId).OrgName;
                    organization = usersub;
                    if (usersub == null)
                    {
                        usersub = "";

                    }

                    var obj = new Userinformation
                    {
                        Id = new Guid(usr.Id),
                        Firstname = usr.FirstName,
                        Lastname = usr.LastName,
                        Email = usr.Email,
                        Role = r.Name,
                        Organization = usersub
                    };

                    userlist.Add(obj);
                }
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