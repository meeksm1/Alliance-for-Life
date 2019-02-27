using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class ClientList
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public DateTime DueDate { get; set; }

        public bool Active { get; set; }

        public DateTime SubmittedDate { get; set; }

        //Navigation Properties

        public SubContractor Subcontractors { get; set; }

        public int SubcontractorId { get; set; }
    }
}