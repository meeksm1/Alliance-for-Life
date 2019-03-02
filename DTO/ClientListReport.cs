using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class ClientListReport
    {
        public System.Guid Id { get; set; }

        public System.Guid SubcontractorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public bool Active { get; set; }

        public string Orgname { get; set; }

        public DateTime SubmittedDate { get; set; }
    }
}