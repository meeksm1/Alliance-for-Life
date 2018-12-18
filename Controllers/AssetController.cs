using Alliance_for_Life.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    public class AssetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View(_context.Assets.ToList());
        }
    }
}