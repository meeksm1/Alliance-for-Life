using Alliance_for_Life.Models;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Alliance_for_Life.ReportControllers
{
    public class ClientListReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientListReportController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult AllActiveClients()
        {
            var allactive = from cl in _context.User
                            join su in _context.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                            where cl.Active == true
                            select new ClientListReport
                            {
                                Id = cl.Id,
                                Orgname = su.OrgName,
                                FirstName = cl.FirstName,
                                LastName = cl.LastName,
                                DOB = cl.DOB,
                                DueDate = cl.DueDate
                            };
            return View(allactive);
        }

        public ActionResult AllNonActiveClients()
        {
            var allnonactive = from cl in _context.User
                               join su in _context.SubContractors on cl.SubcontractorId equals su.SubcontractorId
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

        public ActionResult NonActiveClients()
        {
            var user1 = User.Identity.GetUserId();
            var nonactiveclients = from cl in _context.User
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
            var activeclients = from cl in _context.User
                                join su in _context.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                                join us in _context.Users on su.SubcontractorId equals us.SubcontractorId
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
                new DataColumn ("LastName"),
                new DataColumn ("Due Date"),
                new DataColumn ("Birthday")
            });

            var user1 = User.Identity.GetUserId();
            var query = from cl in _context.User
                        join su in _context.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                        join us in _context.Users on su.SubcontractorId equals us.SubcontractorId
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

        public FileResult ExportAllActive()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn ("Client ID"),
                new DataColumn ("Organization"),
                new DataColumn ("First Name"),
                new DataColumn ("LastName"),
                new DataColumn ("Due Date"),
                new DataColumn ("Birthday")
            });

            var user1 = User.Identity.GetUserId();
            var query = from cl in _context.User
                        join su in _context.SubContractors on cl.SubcontractorId equals su.SubcontractorId
                        where cl.Active 
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

        [HttpPost]
        public FileResult ExportNonActive()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn ("Client ID"),
                new DataColumn ("Organization"),
                new DataColumn ("First Name"),
                new DataColumn ("LastName"),
                new DataColumn ("Due Date"),
                new DataColumn ("Birthday")
            });

            var user1 = User.Identity.GetUserId();
            var query = from cl in _context.User
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

        [HttpPost]
        public FileResult ExportAllNonActive()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn ("Client ID"),
                new DataColumn ("Organization"),
                new DataColumn ("First Name"),
                new DataColumn ("LastName"),
                new DataColumn ("Due Date"),
                new DataColumn ("Birthday")
            });

            var user1 = User.Identity.GetUserId();
            var query = from cl in _context.User
                        join su in _context.SubContractors on cl.SubcontractorId equals su.SubcontractorId
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
    }
}