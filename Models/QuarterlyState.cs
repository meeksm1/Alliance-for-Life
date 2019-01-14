using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Alliance_for_Life.Models
{
    public class QuarterlyState
    {
        [Key]
        public int QuraterlyStateId { get; set; }

        public double TotPSforQuarter()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var quarterTotalList = db.ParticipationServices.ToString();
            var quarterTotal = 1;

            return quarterTotal;
        }

        public double TotDAforQuarter { get; set; }

        public double StateFee { get; set; }

        public double StateFeeQuarter { get; set; }

        public double TotDACandPSMonthly()
        {
            var total = AdminCost.ATotCosts + ParticipationService.PTotals;

            return total;
        }

        public double TotDAandPSQuarter { get; set; }

        //Navigation Properties

        public AdminCosts AdminCost { get; set; }
        public ParticipationService ParticipationService { get; set; }
        public SubContractor Subcontractor { get; set; }
        public Month Month { get; set; }

        public int MonthId { get; set; }
        public int SubcontractorId { get; set; }
        public int AdminCostId { get; set; }
        public int ParticipationServiceId { get; set; }


    }
}