namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedInttoDecimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuarterlyStates", "TotPSforQuarter", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.QuarterlyStates", "TotDACandPSMonthly", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "ASalandWages", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AEmpBenefits", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AEmpTravel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AEmpTraining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AOfficeRent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AOfficeUtilities", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AFacilityIns", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AOfficeSupplies", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AEquipment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AOfficeCommunications", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AOfficeMaint", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AConsulting", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AJanitorServices", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "ADepreciation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "ATechSupport", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "ASecurityServices", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AOther", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AOther2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "AOther3", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AdminCosts", "ATotCosts", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SubContractors", "AllocatedContractAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SubContractors", "AllocatedAdjustments", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "ASalandWages", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AEmpBenefits", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AEmpTravel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AEmpTraining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AOfficeRent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AOfficeUtilities", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AFacilityIns", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AOfficeSupplies", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AEquipment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AOfficeCommunications", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AOfficeMaint", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AConsulting", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "SubConPayCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "BackgrounCheck", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "Other", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AJanitorServices", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "ADepreciation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "ATechSupport", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "ASecurityServices", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "ATotCosts", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "AdminFee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "Trasportation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "JobTraining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "TuitionAssistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "ContractedResidential", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "UtilityAssistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "EmergencyShelter", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "HousingAssistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "Childcare", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "Clothing", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "Food", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "Supplies", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "RFO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "BTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BudgetCosts", "Maxtot", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PTranspotation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PJobTrain", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PEducationAssistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PResidentialCare", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PUtilities", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PHousingEmergency", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PHousingAssistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PChildCare", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PClothing", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PFood", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PSupplies", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "POther", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "POther2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "POther3", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServices", "PTotals", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "LessManagementFee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "TotBedNights", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "TotA2AEnrollment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "TotA2ABedNights", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "MA2Apercent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "ClientsJobEduServ", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "ParticipatingFathers", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "TotEduClasses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "TotClientsinEduClasses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "TotCaseHrs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "TotClientsCaseHrs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NonResidentialMIRs", "TotOtherClasses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PTranspotation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PJobTrain", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PEducationAssistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PResidentialCare", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PUtilities", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PHousingEmergency", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PHousingAssistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PChildCare", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PClothing", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PFood", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PSupplies", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "POther", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "POther2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "POther3", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ParticipationServicesViewModels", "PTotals", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.QuarterlyStates", "TotDAforQuarter", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.QuarterlyStates", "StateFeeQuarter", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.QuarterlyStates", "TotDAandPSQuarter", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "TotBedNights", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "TotA2AEnrollment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "TotA2ABedNights", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "MA2Apercent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "ClientsJobEduServ", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "ParticipatingFathers", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "TotEduClasses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "TotClientsinEduClasses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "TotCaseHrs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "TotClientsCaseHrs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ResidentialMIRs", "TotOtherClasses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ResidentialMIRs", "TotOtherClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotClientsCaseHrs", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotCaseHrs", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotClientsinEduClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotEduClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "ParticipatingFathers", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "ClientsJobEduServ", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "MA2Apercent", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotA2ABedNights", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotA2AEnrollment", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotBedNights", c => c.Int(nullable: false));
            AlterColumn("dbo.QuarterlyStates", "TotDAandPSQuarter", c => c.Double(nullable: false));
            AlterColumn("dbo.QuarterlyStates", "StateFeeQuarter", c => c.Double(nullable: false));
            AlterColumn("dbo.QuarterlyStates", "TotDAforQuarter", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PTotals", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "POther3", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "POther2", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "POther", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PSupplies", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PFood", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PClothing", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PChildCare", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PHousingAssistance", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PHousingEmergency", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PUtilities", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PResidentialCare", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PEducationAssistance", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PJobTrain", c => c.Int(nullable: false));
            AlterColumn("dbo.ParticipationServicesViewModels", "PTranspotation", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotOtherClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotClientsCaseHrs", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotCaseHrs", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotClientsinEduClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotEduClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "ParticipatingFathers", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "ClientsJobEduServ", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "MA2Apercent", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotA2ABedNights", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotA2AEnrollment", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotBedNights", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "LessManagementFee", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PTotals", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "POther3", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "POther2", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "POther", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PSupplies", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PFood", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PClothing", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PChildCare", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PHousingAssistance", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PHousingEmergency", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PUtilities", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PResidentialCare", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PEducationAssistance", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PJobTrain", c => c.Double(nullable: false));
            AlterColumn("dbo.ParticipationServices", "PTranspotation", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Maxtot", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "BTotal", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "RFO", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Supplies", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Food", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Clothing", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Childcare", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "HousingAssistance", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "EmergencyShelter", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "UtilityAssistance", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ContractedResidential", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "TuitionAssistance", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "JobTraining", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Trasportation", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AdminFee", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ATotCosts", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ASecurityServices", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ATechSupport", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ADepreciation", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AJanitorServices", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Other", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "BackgrounCheck", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "SubConPayCost", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AConsulting", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeMaint", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeCommunications", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AEquipment", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeSupplies", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AFacilityIns", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeUtilities", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeRent", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AEmpTraining", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AEmpTravel", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AEmpBenefits", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ASalandWages", c => c.Int(nullable: false));
            AlterColumn("dbo.SubContractors", "AllocatedAdjustments", c => c.Int(nullable: false));
            AlterColumn("dbo.SubContractors", "AllocatedContractAmount", c => c.Int(nullable: false));
            AlterColumn("dbo.AdminCosts", "ATotCosts", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AOther3", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AOther2", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AOther", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "ASecurityServices", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "ATechSupport", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "ADepreciation", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AJanitorServices", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AConsulting", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AOfficeMaint", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AOfficeCommunications", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AEquipment", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AOfficeSupplies", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AFacilityIns", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AOfficeUtilities", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AOfficeRent", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AEmpTraining", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AEmpTravel", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "AEmpBenefits", c => c.Double(nullable: false));
            AlterColumn("dbo.AdminCosts", "ASalandWages", c => c.Double(nullable: false));
            DropColumn("dbo.QuarterlyStates", "TotDACandPSMonthly");
            DropColumn("dbo.QuarterlyStates", "TotPSforQuarter");
        }
    }
}