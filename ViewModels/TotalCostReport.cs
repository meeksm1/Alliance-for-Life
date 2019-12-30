using Alliance_for_Life.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.ViewModels
{

    public class TotalCostReport
    {
        public IEnumerable<AdminCosts> AdminCosts { get; set; }
        public IEnumerable<ParticipationService> ParticipationCost { get; set; }

    }
}