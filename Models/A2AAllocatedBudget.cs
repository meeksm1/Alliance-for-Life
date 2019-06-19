using System;
using System.Collections.Generic;

namespace Alliance_for_Life.Models
{
    public class A2AAllocatedBudget
    {
        public Guid A2AAllocatedBudgetId { get; set; }
        public int Year { get; set; }
        public double BeginingBalance { get; set; }

        public ICollection<A2AStateDeposits> StateDeposits { get; set; }
    }
}