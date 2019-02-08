using System;


namespace Alliance_for_Life.Models
{
    public class ClientListReport
    {
        public int Id { get; set; }

        public int SubcontractorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public DateTime DueDate { get; set; }

        public bool Active { get; set; }

        public string Orgname { get; set; }

        public DateTime SubmittedDate { get; set; }
    }
}