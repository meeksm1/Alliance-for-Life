using Alliance_for_Life.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.ViewModels
{
    public class MonthlyServices
    {
        public IEnumerable<ResidentialMIR> Residential { get; set; }
        public IEnumerable<NonResidentialMIR> NonResidential { get; set; }
    }
}