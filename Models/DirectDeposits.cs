using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alliance_for_Life.Models
{
    public class DirectDeposits
    {
        [Key]
        public System.Guid DirectDepositsId { get; set; }

        public bool IsCheked { get; set; }

        public System.Guid AdminCostId { get; set; }
        public System.Guid PSId { get; set; }

        public virtual AdminCosts AdminCost { get; set; }
        public virtual ParticipationService ParticipationService { get; set; }

    }
}