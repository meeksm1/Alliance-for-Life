using Alliance_for_Life.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.ViewModels
{
    public class DirectDeposit
    {
        public AdminCosts AdminCost { get; set; }
        public ParticipationService ParticipationService { get; set; }
    }
}