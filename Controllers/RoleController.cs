using Alliance_for_Life.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext _context;

        public RoleController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var Roles = _context.Roles.ToList();
            return View(Roles);
        }

        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            _context.Roles.Add(Role);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}