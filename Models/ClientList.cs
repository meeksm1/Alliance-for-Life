using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class ClientList
    {
        public int Id { get; set; }

        public int Subcontractor { get; set; }

        public SubContractor Subcontractors { get; set; }

        public string OrgName { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public DateTime DueDate { get; set; }

        public bool Active { get; set; }

        public string AdministratorId { get; set; }

        public ApplicationUser Administrator { get; set; }

    }
}