using System;
using System.ComponentModel.DataAnnotations;

namespace Alliance_for_Life.Models
{
    public class SubContractor
    {
        public System.Guid SubcontractorId { get; set; }

        public Role Administrator { get; set; }

        [Required]
        public string AdministratorId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Organization")]
        public string OrgName { get; set; }

        [Required]
        [StringLength(100)]
        public string Director { get; set; }


        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        public string County { get; set; }

        [Required]
        public GeoRegion? Region { get; set; }

        public int EIN { get; set; }

        [Required]
        [StringLength(25)]
        public string State { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public double AllocatedContractAmount { get; set; }

        public double AllocatedAdjustments { get; set; }

        public string Address1 { get; set; }

        public string PoBox { get; set; }

        public bool Active { get; set; }

        public DateTime SubmittedDate { get; set; }

        public AffiliatesRegion? AffiliateRegion { get; set; }

    }
}