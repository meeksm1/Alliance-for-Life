using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.Models
{
    public class A2AAllocatedYearlyBudget
    {
        public Guid A2AAllocatedYearlyBudgetId { get; set; }
        public int Year { get; set; }
        public double A2AFundBalance { get; set; }

    }
}