using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class ClientList
    {
        public System.Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public bool Active { get; set; }

        [DataType(DataType.Date)]
        public DateTime SubmittedDate { get; set; }

        //Navigation Properties

        public SubContractor Subcontractor { get; set; }

        public System.Guid SubcontractorId { get; set; }
    }
}