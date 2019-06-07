﻿
using System;

namespace Alliance_for_Life.Models
{
    public class AllocatedBudget
    {
        public Guid AllocatedBudgetID { get; set; }
        public Guid SubcontractorId { get; set; }
        public double CycleEndAdjustments { get; set; }


        public int Year { get; set; }
        public double AllocatedNewBudget { get; set; }
        public double AllocatedOldBudget { get; set; }
        public DateTime AllocationAdjustedDate { get; set; }
        public SubContractor Subcontractor { get; set; }
        public Invoice Invoice { get; set; }
        public Months? Month { get; set; }


    }
}