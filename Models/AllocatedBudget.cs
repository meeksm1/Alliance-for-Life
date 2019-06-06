
using System;

namespace Alliance_for_Life.Models
{
    public class AllocatedBudget
    {
        public System.Guid AllocatedBudgetID { get; set; }
        public System.Guid SubcontractorId { get; set; }

        public GeoRegion? Region { get; set; }
        public Months? Month { get; set; }
        public int Year { get; set; }
        public double AllocatedNewBudget { get; set; }
        public double AllocatedOldBudget { get; set; }
        public DateTime AllocationAdjustedDate { get; set; }
        public SubContractor Subcontractor { get; set; }
    }
}