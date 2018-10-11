using Alliance_for_Life.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.ViewModels
{
    public class ClientListReportViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        [BirthDate]
        public string DOB { get; set; }
        [FutureDate]
        public string DueDate { get; set; }

        public string OrgName { get; set; }

        public bool Active { get; set; }

        public DateTime GetDateTimeDueDate()
        {
            return DateTime.Parse(string.Format(DueDate));
        }
        public DateTime GetDateTimeDOB()
        {
            return DateTime.Parse(string.Format(DOB));
        }

    }
}