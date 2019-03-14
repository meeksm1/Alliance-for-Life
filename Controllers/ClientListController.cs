using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;
using PagedList;

namespace Alliance_for_Life.Controllers
{
    public class ClientListController : Controller
    {
        private readonly ApplicationDbContext db;

        public ClientListController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Reports()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
             var subcontractorlist = new ClientListFormViewModel
             {
                 Subcontractors = from s in db.SubContractors
                                  join a in db.Users on s.AdministratorId equals a.Id
                                  where s.SubcontractorId == a.SubcontractorId
                                  select s,
                 Heading = "Add Client Information"
             }; 

            if (User.IsInRole("Admin"))
            {
                var viewModel = new ClientListFormViewModel
                {
                    Subcontractors = db.SubContractors.ToList(),
                    Heading = "Add Client Information"
                };
                return View("ClientListForm", viewModel);
            }
               

            return View("ClientListForm", subcontractorlist);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientListFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                    viewModel.Subcontractors = db.SubContractors.ToList();
                    return View("ClientListForm", viewModel);
            }

            var client = new ClientList
            {
                Id= Guid.NewGuid(),
                SubcontractorId = viewModel.SubcontractorId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                DOB = viewModel.DOB,
                DueDate = viewModel.DueDate,
                Active = viewModel.Active,
                SubmittedDate = DateTime.Now
            };

            db.ClientLists.Add(client);
            db.SaveChanges();

            return RedirectToAction("Create", "ContractorForm");
        }

        // GET: ClientList/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientList client = db.ClientLists

                .SingleOrDefault(a => a.Id == id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ClientListFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Subcontractors = db.SubContractors.ToList();
                return View("ClientListForm", viewModel);
            }

            var client = db.ClientLists.Single(s => s.Id == viewModel.Id);
            {
                client.SubcontractorId = new Guid(Request["SubcontractorId"]);
                client.FirstName = viewModel.FirstName;
                client.LastName = viewModel.LastName;
                client.DOB = viewModel.DOB;
                client.DueDate = viewModel.DueDate;
                client.Active = viewModel.Active;
            };

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            var client = db.ClientLists.Single(s => s.Id == id);
            var viewModel = new ClientListFormViewModel
            {
                Heading = "Edit Client Information",
                Id = client.Id,
                Subcontractors = db.SubContractors.ToList(),
                FirstName = client.FirstName,
                LastName = client.LastName,
                DOB = client.DOB,
                DueDate = client.DueDate,
                Active = client.Active,
                SubmittedDate = DateTime.Now
            };
            viewModel.SubcontractorId = client.SubcontractorId;
            viewModel.DOB = client.DOB;
            viewModel.DueDate = client.DueDate;

            return View("ClientListForm", viewModel);
        }

        public ViewResult AllActiveClients(string sortOrder, string searchString, string currentFilter, int? page, string pgSize)
        {

            int pageSize = Convert.ToInt16(pgSize);
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "last_desc" : "LastName";

            //looking for the searchstring
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var clients = db.ClientLists.Include(s => s.Subcontractor)
                .Where(s => s.Active);

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(a => a.Subcontractor.OrgName.Contains(searchString)
                || a.LastName.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    clients = clients.OrderBy(s => s.DueDate);
                    break;
                case "date_desc":
                    clients = clients.OrderByDescending(s => s.DueDate);
                    break;
                case "LastName":
                    clients = clients.OrderBy(s => s.LastName);
                    break;
                case "last_desc":
                    clients = clients.OrderByDescending(s => s.LastName);
                    break;
                default:
                    clients = clients.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));
        }

        public FileResult ExportAllActive()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn ("Client ID"),
                new DataColumn ("Organization"),
                new DataColumn ("First Name"),
                new DataColumn ("Last Name"),
                new DataColumn ("Due Date"),
                new DataColumn ("Birthday"),
                new DataColumn ("Date Submitted")
            });

            var user1 = User.Identity.GetUserId();
            var query = from cl in db.ClientLists
                        join su in db.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                        where cl.Active
                        select new ClientListReport
                        {
                            Id = cl.Id,
                            Orgname = su.OrgName,
                            FirstName = cl.FirstName,
                            LastName = cl.LastName,
                            DOB = cl.DOB,
                            DueDate = cl.DueDate,
                            SubmittedDate = cl.SubmittedDate
                        };

            foreach (var item in query)
            {
                dt.Rows.Add(item.Id, item.Orgname, item.FirstName, item.LastName, item.DOB.ToShortDateString(), item.DueDate.ToShortDateString(), item.SubmittedDate);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

        public ActionResult ActiveClients(string sortOrder, string searchString, string currentFilter, int? page, string pgSize)
        {

            int pageSize = Convert.ToInt16(pgSize);
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "last_desc" : "LastName";

            //looking for the searchstring
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var clients = db.ClientLists.Include(s => s.Subcontractor)
                .Where(s => s.SubcontractorId == s.Subcontractor.SubcontractorId)
                .Where(s => s.Active);

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(a => a.Subcontractor.OrgName.Contains(searchString)
                || a.LastName.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    clients = clients.OrderBy(s => s.DueDate);
                    break;
                case "date_desc":
                    clients = clients.OrderByDescending(s => s.DueDate);
                    break;
                case "LastName":
                    clients = clients.OrderBy(s => s.LastName);
                    break;
                case "last_desc":
                    clients = clients.OrderByDescending(s => s.LastName);
                    break;
                default:
                    clients = clients.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public FileResult ExportActive()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn ("Client ID"),
                new DataColumn ("Organization"),
                new DataColumn ("First Name"),
                new DataColumn ("Last Name"),
                new DataColumn ("Due Date"),
                new DataColumn ("Birthday")
            });

            var user1 = User.Identity.GetUserId();
            var query = from cl in db.ClientLists
                        join su in db.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                        join us in db.Users on su.SubcontractorId equals us.SubcontractorId
                        where cl.Active && us.Id == user1
                        select new ClientListReport
                        {
                            Id = cl.Id,
                            Orgname = su.OrgName,
                            FirstName = cl.FirstName,
                            LastName = cl.LastName,
                            DOB = cl.DOB,
                            DueDate = cl.DueDate
                        };

            foreach (var item in query)
            {
                dt.Rows.Add(item.Id, item.Orgname, item.FirstName, item.LastName, item.DOB.ToShortDateString(), item.DueDate.ToShortDateString());
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

        public ActionResult AllNonActiveClients(string sortOrder, string searchString, string currentFilter, int? page, string pgSize)
        {

            int pageSize = Convert.ToInt16(pgSize);
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "last_desc" : "LastName";

            //looking for the searchstring
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var clients = db.ClientLists.Include(s => s.Subcontractor)
                .Where(s => s.Active == false);

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(a => a.Subcontractor.OrgName.Contains(searchString)
                || a.LastName.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    clients = clients.OrderBy(s => s.DueDate);
                    break;
                case "date_desc":
                    clients = clients.OrderByDescending(s => s.DueDate);
                    break;
                case "LastName":
                    clients = clients.OrderBy(s => s.LastName);
                    break;
                case "last_desc":
                    clients = clients.OrderByDescending(s => s.LastName);
                    break;
                default:
                    clients = clients.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public FileResult ExportAllNonActive()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn ("Client ID"),
                new DataColumn ("Organization"),
                new DataColumn ("First Name"),
                new DataColumn ("Last Name"),
                new DataColumn ("Due Date"),
                new DataColumn ("Birthday")
            });

            var user1 = User.Identity.GetUserId();
            var query = from cl in db.ClientLists
                        join su in db.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                        where cl.Active == false
                        select new ClientListReport
                        {
                            Id = cl.Id,
                            Orgname = su.OrgName,
                            FirstName = cl.FirstName,
                            LastName = cl.LastName,
                            DOB = cl.DOB,
                            DueDate = cl.DueDate
                        };

            foreach (var item in query)
            {
                dt.Rows.Add(item.Id, item.Orgname, item.FirstName, item.LastName, item.DOB.ToShortDateString(), item.DueDate.ToShortDateString());
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

        public ActionResult NonActiveClients(string sortOrder, string searchString, string currentFilter, int? page, string pgSize)
        {

            int pageSize = Convert.ToInt16(pgSize);
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "last_desc" : "LastName";

            //looking for the searchstring
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var clients = db.ClientLists.Include(s => s.Subcontractor)
                .Where(s => s.SubcontractorId == s.Subcontractor.SubcontractorId)
                .Where(s => s.Active == false);

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(a => a.Subcontractor.OrgName.Contains(searchString)
                || a.LastName.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.Subcontractor.OrgName);
                    break;
                case "Date":
                    clients = clients.OrderBy(s => s.DueDate);
                    break;
                case "date_desc":
                    clients = clients.OrderByDescending(s => s.DueDate);
                    break;
                case "LastName":
                    clients = clients.OrderBy(s => s.LastName);
                    break;
                case "last_desc":
                    clients = clients.OrderByDescending(s => s.LastName);
                    break;
                default:
                    clients = clients.OrderBy(s => s.Subcontractor.OrgName);
                    break;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public FileResult ExportNonActive()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn ("Client ID"),
                new DataColumn ("Organization"),
                new DataColumn ("First Name"),
                new DataColumn ("Last Name"),
                new DataColumn ("Due Date"),
                new DataColumn ("Birthday")
            });

            var user1 = User.Identity.GetUserId();
            var query = from cl in db.ClientLists
                        join su in db.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                        join us in db.Users on su.SubcontractorId equals us.SubcontractorId
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

            foreach (var item in query)
            {
                dt.Rows.Add(item.Id, item.Orgname, item.FirstName, item.LastName, item.DOB.ToShortDateString(), item.DueDate.ToShortDateString());
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
    }
}