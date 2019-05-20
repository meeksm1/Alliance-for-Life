using Alliance_for_Life.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Alliance_for_Life.Controllers
{
    [Authorize]
    public class QuarterlyStatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuarterlyStates
        public ActionResult Index()
        {
            return View(db.QuarterlyStates.ToList());
        }

        public ActionResult FirstQuarterReport()
        {
            var montot1 = from x in db.AdminCosts where (int)x.Month == 1 select x.ATotCosts;

            var firstquarter = from qs in db.QuarterlyStates
                               join s in db.SubContractors on qs.SubcontractorId equals s.SubcontractorId
                               join a in db.AdminCosts on qs.AdminCostId equals a.AdminCostId
                               join p in db.ParticipationServices on qs.ParticipationCostId equals p.PSId
                               where (int)qs.Month <= 7 && (int)qs.Month >= 9

                               select new QuarterlyStateReport
                               {
                                   OrgName = s.OrgName,
                                   MonthName = qs.Month.ToString(),
                                   ASalandWages = a.ASalandWages,
                                   AEmpBenefits = a.AEmpBenefits,
                                   AEmpTravel = a.AEmpTravel,
                                   AEmpTraining = a.AEmpTraining,
                                   AOfficeRent = a.AOfficeRent,
                                   AOfficeUtilities = a.AOfficeUtilities,
                                   AFacilityIns = a.AFacilityIns,
                                   AOfficeSupplies = a.AOfficeSupplies,
                                   AEquipment = a.AEquipment,
                                   AOfficeCommunications = a.AOfficeCommunications,
                                   AOfficeMaint = a.AOfficeMaint,
                                   AConsulting = a.AConsulting,
                                   EFTFees = a.EFTFees,
                                   AJanitorServices = a.AJanitorServices,
                                   ADepreciation = a.ADepreciation,
                                   ATechSupport = a.ATechSupport,
                                   ASecurityServices = a.ASecurityServices,
                                   ABackgroundCheck = a.ABackgroundCheck,
                                   ATotCosts = a.ATotCosts,
                                   StateAdminFee = qs.StateFee,
                                   TotDAforQuarter = qs.TotDAforQuarter,
                                   StateFeeQuarter = qs.StateFeeQuarter,
                                   PTranspotation = p.PTranspotation,
                                   PJobTrain = p.PJobTrain,
                                   PEducationAssistance = p.PEducationAssistance,
                                   PResidentialCare = p.PResidentialCare,
                                   PUtilities = p.PUtilities,
                                   PHousingEmergency = p.PHousingEmergency,
                                   PHousingAssistance = p.PHousingAssistance,
                                   PChildCare = p.PChildCare,
                                   PClothing = p.PClothing,
                                   PFood = p.PFood,
                                   PSupplies = p.PSupplies,
                                   RFOCarRepairs = p.POther,
                                   RFOCarPayments = p.POther2,
                                   RFOCarIns = p.POther3,
                                   PTotals = p.PTotals,
                                   TotDAandPSMonth = qs.TotDACandPSMonthly,
                                   TotDAandPSQuarter = qs.TotDAandPSQuarter
                               };

            return View(firstquarter);
        }

        public ActionResult SecondQuarterReport()
        {
            var montot1 = from x in db.AdminCosts where (int)x.Month == 1 select x.ATotCosts;

            var secondquarter = from qs in db.QuarterlyStates
                               join s in db.SubContractors on qs.SubcontractorId equals s.SubcontractorId
                               join a in db.AdminCosts on qs.AdminCostId equals a.AdminCostId
                               join p in db.ParticipationServices on qs.ParticipationCostId equals p.PSId
                               where (int)qs.Month <= 10 && (int)qs.Month >= 12

                               select new QuarterlyStateReport
                               {
                                   OrgName = s.OrgName,
                                   MonthName = qs.Month.ToString(),
                                   ASalandWages = a.ASalandWages,
                                   AEmpBenefits = a.AEmpBenefits,
                                   AEmpTravel = a.AEmpTravel,
                                   AEmpTraining = a.AEmpTraining,
                                   AOfficeRent = a.AOfficeRent,
                                   AOfficeUtilities = a.AOfficeUtilities,
                                   AFacilityIns = a.AFacilityIns,
                                   AOfficeSupplies = a.AOfficeSupplies,
                                   AEquipment = a.AEquipment,
                                   AOfficeCommunications = a.AOfficeCommunications,
                                   AOfficeMaint = a.AOfficeMaint,
                                   AConsulting = a.AConsulting,
                                   EFTFees = a.EFTFees,
                                   AJanitorServices = a.AJanitorServices,
                                   ADepreciation = a.ADepreciation,
                                   ATechSupport = a.ATechSupport,
                                   ASecurityServices = a.ASecurityServices,
                                   ABackgroundCheck = a.ABackgroundCheck,
                                   ATotCosts = a.ATotCosts,
                                   StateAdminFee = qs.StateFee,
                                   TotDAforQuarter = qs.TotDAforQuarter,
                                   StateFeeQuarter = qs.StateFeeQuarter,
                                   PTranspotation = p.PTranspotation,
                                   PJobTrain = p.PJobTrain,
                                   PEducationAssistance = p.PEducationAssistance,
                                   PResidentialCare = p.PResidentialCare,
                                   PUtilities = p.PUtilities,
                                   PHousingEmergency = p.PHousingEmergency,
                                   PHousingAssistance = p.PHousingAssistance,
                                   PChildCare = p.PChildCare,
                                   PClothing = p.PClothing,
                                   PFood = p.PFood,
                                   PSupplies = p.PSupplies,
                                   RFOCarRepairs = p.POther,
                                   RFOCarPayments = p.POther2,
                                   RFOCarIns = p.POther3,
                                   PTotals = p.PTotals,
                                   TotDAandPSMonth = qs.TotDACandPSMonthly,
                                   TotDAandPSQuarter = qs.TotDAandPSQuarter
                               };

            return View(secondquarter);
        }

        public ActionResult ThirdQuarterReport()
        {
            var montot1 = from x in db.AdminCosts where (int)x.Month == 1 select x.ATotCosts;

            var thirdquarter = from qs in db.QuarterlyStates
                               join s in db.SubContractors on qs.SubcontractorId equals s.SubcontractorId
                               join a in db.AdminCosts on qs.AdminCostId equals a.AdminCostId
                               join p in db.ParticipationServices on qs.ParticipationCostId equals p.PSId
                               where (int) qs.Month <= 1 && (int)qs.Month >= 3

                               select new QuarterlyStateReport
                               {
                                   OrgName = s.OrgName,
                                   MonthName = qs.Month.ToString(),
                                   ASalandWages = a.ASalandWages,
                                   AEmpBenefits = a.AEmpBenefits,
                                   AEmpTravel = a.AEmpTravel,
                                   AEmpTraining = a.AEmpTraining,
                                   AOfficeRent = a.AOfficeRent,
                                   AOfficeUtilities = a.AOfficeUtilities,
                                   AFacilityIns = a.AFacilityIns,
                                   AOfficeSupplies = a.AOfficeSupplies,
                                   AEquipment = a.AEquipment,
                                   AOfficeCommunications = a.AOfficeCommunications,
                                   AOfficeMaint = a.AOfficeMaint,
                                   AConsulting = a.AConsulting,
                                   EFTFees = a.EFTFees,
                                   AJanitorServices = a.AJanitorServices,
                                   ADepreciation = a.ADepreciation,
                                   ATechSupport = a.ATechSupport,
                                   ASecurityServices = a.ASecurityServices,
                                   ABackgroundCheck = a.ABackgroundCheck,
                                   ATotCosts = a.ATotCosts,
                                   StateAdminFee = qs.StateFee,
                                   TotDAforQuarter = qs.TotDAforQuarter,
                                   StateFeeQuarter = qs.StateFeeQuarter,
                                   PTranspotation = p.PTranspotation,
                                   PJobTrain = p.PJobTrain,
                                   PEducationAssistance = p.PEducationAssistance,
                                   PResidentialCare = p.PResidentialCare,
                                   PUtilities = p.PUtilities,
                                   PHousingEmergency = p.PHousingEmergency,
                                   PHousingAssistance = p.PHousingAssistance,
                                   PChildCare = p.PChildCare,
                                   PClothing = p.PClothing,
                                   PFood = p.PFood,
                                   PSupplies = p.PSupplies,
                                   RFOCarRepairs = p.POther,
                                   RFOCarPayments = p.POther2,
                                   RFOCarIns = p.POther3,
                                   PTotals = p.PTotals,
                                   TotDAandPSMonth = qs.TotDACandPSMonthly,
                                   TotDAandPSQuarter = qs.TotDAandPSQuarter
                               };

            return View(thirdquarter);
        }

        public ActionResult FourthQuarterReport()
        {
            var montot1 = from x in db.AdminCosts where (int)x.Month == 1 select x.ATotCosts;

            var fourthquarter = from qs in db.QuarterlyStates
                               join s in db.SubContractors on qs.SubcontractorId equals s.SubcontractorId
                               join a in db.AdminCosts on qs.AdminCostId equals a.AdminCostId
                               join p in db.ParticipationServices on qs.ParticipationCostId equals p.PSId
                               where (int)qs.Month >= 4 && (int)qs.Month >= 6

                               select new QuarterlyStateReport
                               {
                                   OrgName = s.OrgName,
                                   MonthName = qs.Month.ToString(),
                                   ASalandWages = a.ASalandWages,
                                   AEmpBenefits = a.AEmpBenefits,
                                   AEmpTravel = a.AEmpTravel,
                                   AEmpTraining = a.AEmpTraining,
                                   AOfficeRent = a.AOfficeRent,
                                   AOfficeUtilities = a.AOfficeUtilities,
                                   AFacilityIns = a.AFacilityIns,
                                   AOfficeSupplies = a.AOfficeSupplies,
                                   AEquipment = a.AEquipment,
                                   AOfficeCommunications = a.AOfficeCommunications,
                                   AOfficeMaint = a.AOfficeMaint,
                                   AConsulting = a.AConsulting,
                                   EFTFees = a.EFTFees,
                                   AJanitorServices = a.AJanitorServices,
                                   ADepreciation = a.ADepreciation,
                                   ATechSupport = a.ATechSupport,
                                   ASecurityServices = a.ASecurityServices,
                                   ABackgroundCheck = a.ABackgroundCheck,
                                   ATotCosts = a.ATotCosts,
                                   StateAdminFee = qs.StateFee,
                                   TotDAforQuarter = qs.TotDAforQuarter,
                                   StateFeeQuarter = qs.StateFeeQuarter,
                                   PTranspotation = p.PTranspotation,
                                   PJobTrain = p.PJobTrain,
                                   PEducationAssistance = p.PEducationAssistance,
                                   PResidentialCare = p.PResidentialCare,
                                   PUtilities = p.PUtilities,
                                   PHousingEmergency = p.PHousingEmergency,
                                   PHousingAssistance = p.PHousingAssistance,
                                   PChildCare = p.PChildCare,
                                   PClothing = p.PClothing,
                                   PFood = p.PFood,
                                   PSupplies = p.PSupplies,
                                   RFOCarRepairs = p.POther,
                                   RFOCarPayments = p.POther2,
                                   RFOCarIns = p.POther3,
                                   PTotals = p.PTotals,
                                   TotDAandPSMonth = qs.TotDACandPSMonthly,
                                   TotDAandPSQuarter = qs.TotDAandPSQuarter
                               };

            return View(fourthquarter);
        }

        // GET: QuarterlyStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuarterlyState quarterlyState = db.QuarterlyStates.Find(id);
            if (quarterlyState == null)
            {
                return HttpNotFound();
            }
            return View(quarterlyState);
        }

        // GET: QuarterlyStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuarterlyStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuraterlyStateId,TotPSforQuarter,TotDAforQuarter,StateFeeQuarter,TotDAandPSQuarter,AdminCostId,Month,SubcontractorId,ParticipationCostId,TotDAandPSMonthly,StateFee")] QuarterlyState quarterlyState)
        {
            if (ModelState.IsValid)
            {
                var dataexist = from s in db.QuarterlyStates
                                where
                                s.SubcontractorId == quarterlyState.SubcontractorId &&
                                s.AdminCostId == quarterlyState.AdminCostId &&
                                s.Month == quarterlyState.Month
                                select s;
                if (dataexist.Count() >= 1)
                {
                    ViewBag.error = "Data already exists. Please change the params or search in the Reports tab for the current Record.";
                }
                else
                {
                    db.QuarterlyStates.Add(quarterlyState);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(quarterlyState);
        }

        // GET: QuarterlyStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuarterlyState quarterlyState = db.QuarterlyStates.Find(id);
            if (quarterlyState == null)
            {
                return HttpNotFound();
            }
            return View(quarterlyState);
        }

        // POST: QuarterlyStates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuraterlyStateId,TotPSforQuarter,TotDAforQuarter,StateFeeQuarter,TotDAandPSQuarter,AdminCostId,Month,SubcontractorId,ParticipationCostId,TotDAandPSMonthly,StateFee")] QuarterlyState quarterlyState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quarterlyState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quarterlyState);
        }

        // GET: QuarterlyStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuarterlyState quarterlyState = db.QuarterlyStates.Find(id);
            if (quarterlyState == null)
            {
                return HttpNotFound();
            }
            return View(quarterlyState);
        }

        // POST: QuarterlyStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuarterlyState quarterlyState = db.QuarterlyStates.Find(id);
            db.QuarterlyStates.Remove(quarterlyState);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
