using System;

namespace Alliance_for_Life.Models
{
    public class SubcontractorReport
    {
        public int SubcontractorId { get; set; }
        public int EIN { get; set; }
        public string OrgName { get; set; }
        public string Director { get; set; }
        public string Region { get; set; }
        public bool Active { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}