using Alliance_for_Life.Models;
using Alliance_for_Life.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

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
            var viewModel = new ClientListFormViewModel
            {
                Subcontractors = db.SubContractors.ToList(),
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
                    viewModel.Subcontractors = db.SubContractors.ToList();
                    return View("ClientListForm", viewModel);
            }

            var client = new ClientList
            {
                SubcontractorId = viewModel.SubcontractorId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                DOB = viewModel.GetDateTimeDOB(),
                DueDate = viewModel.GetDateTimeDueDate(),
                Active = viewModel.Active,
                SubmittedDate = DateTime.Now
            };

            db.User.Add(client);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
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

            var client = db.User.Single(s => s.Id == viewModel.Id);
            {
                client.SubcontractorId = viewModel.SubcontractorId;
                client.FirstName = viewModel.FirstName;
                client.LastName = viewModel.LastName;
                client.DOB = viewModel.GetDateTimeDOB();
                client.DueDate = viewModel.GetDateTimeDueDate();
                client.Active = viewModel.Active;
            };

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var client = db.User.Single(s => s.Id == id);
            var viewModel = new ClientListFormViewModel
            {
                Heading = "Edit Client Information",
                Id = client.Id,
                Subcontractors = db.SubContractors.ToList(),
                FirstName = client.FirstName,
                LastName = client.LastName,
                DOB = client.DOB.ToString("mm/dd/yyyy"),
                DueDate = client.DueDate.ToString("mm/dd/yyyy"),
                Active = client.Active,
                SubmittedDate = DateTime.Now
            };
            return View("ClientListForm", viewModel);
        }

        public ActionResult AllActiveClients()
        {
            var allactive = from cl in db.User
                            join su in db.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                            where cl.Active == true
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
            return View(allactive);
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
            var query = from cl in db.User
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

        public ActionResult ActiveClients()
        {
            var user1 = User.Identity.GetUserId();
            var activeclients = from cl in db.User
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

            return View(activeclients);
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
            var query = from cl in db.User
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

        public ActionResult AllNonActiveClients()
        {
            var allnonactive = from cl in db.User
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
            return View(allnonactive);
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
            var query = from cl in db.User
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

        public ActionResult NonActiveClients()
        {
            var user1 = User.Identity.GetUserId();
            var nonactiveclients = from cl in db.User
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

            return View(nonactiveclients);
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
            var query = from cl in db.User
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