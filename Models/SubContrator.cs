using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class SubContractor
    {
        public int ID { get; set; }

        public ApplicationUser Administrator { get; set; }

        [Required]
        public string AdministratorId { get; set; }

        [Required]
        [StringLength(255)]
        public string OrgName { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        public string County { get; set; }

        
        [StringLength(25)]
        public Region Regions { get; set; }

        [Required]
        public int RegionId { get; set; }

        public int EIN { get; set; }

        [Required]
        [StringLength(25)]
        public string State { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public int AllocatedContractAmount { get; set; }

        public int AllocatedAdjustments { get; set; }

        public string Address1 { get; set; }

        public string PoBox { get; set; }

        public bool Active { get; set; }

    }
}