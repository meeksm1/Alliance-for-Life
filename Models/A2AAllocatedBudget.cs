using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.Models
{
    public class A2AAllocatedBudget
    {
        public Guid A2AAllocatedBudgetId { get; set; }
        public int Year { get; set; }
        public double BeginingBalance { get; set; }
        public double StateDeposits { get; set; }


        public Months Month { get; set; }


    }
}