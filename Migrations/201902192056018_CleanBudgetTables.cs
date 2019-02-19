namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanBudgetTables : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AdminCosts", new[] { "BudgetCosts_BudgetInvoiceId" });
            DropIndex("dbo.ParticipationServices", new[] { "BudgetCosts_BudgetInvoiceId" });
            RenameColumn(table: "dbo.BudgetCosts", name: "BudgetCosts_BudgetInvoiceId", newName: "AdminCost_AdminCostId");
            RenameColumn(table: "dbo.BudgetCosts", name: "BudgetCosts_BudgetInvoiceId", newName: "ParticipationCost_PSId");
            AlterColumn("dbo.SubContractors", "AllocatedContractAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.SubContractors", "AllocatedAdjustments", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ASalandWages", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AEmpBenefits", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AEmpTravel", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AEmpTraining", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeRent", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeUtilities", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AFacilityIns", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeSupplies", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AEquipment", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeCommunications", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AOfficeMaint", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AConsulting", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "SubConPayCost", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "BackgrounCheck", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Other", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "AJanitorServices", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ADepreciation", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ATechSupport", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ASecurityServices", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Trasportation", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "JobTraining", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "TuitionAssistance", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "ContractedResidential", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "UtilityAssistance", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "EmergencyShelter", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "HousingAssistance", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Childcare", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Clothing", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Food", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "Supplies", c => c.Double(nullable: false));
            AlterColumn("dbo.BudgetCosts", "RFO", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotBedNights", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotA2AEnrollment", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotA2ABedNights", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "ClientsJobEduServ", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "ParticipatingFathers", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotEduClasses", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotClientsinEduClasses", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotCaseHrs", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotClientsCaseHrs", c => c.Double(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotOtherClasses", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotBedNights", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotA2AEnrollment", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotA2ABedNights", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "ClientsJobEduServ", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "ParticipatingFathers", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotEduClasses", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotClientsinEduClasses", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotCaseHrs", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotClientsCaseHrs", c => c.Double(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotOtherClasses", c => c.Double(nullable: false));
            CreateIndex("dbo.BudgetCosts", "MonthId");
            CreateIndex("dbo.BudgetCosts", "AdminCost_AdminCostId");
            CreateIndex("dbo.BudgetCosts", "ParticipationCost_PSId");
            AddForeignKey("dbo.BudgetCosts", "MonthId", "dbo.Months", "Id", cascadeDelete: true);
            DropColumn("dbo.AdminCosts", "BudgetCosts_BudgetInvoiceId");
            DropColumn("dbo.ParticipationServices", "BudgetCosts_BudgetInvoiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParticipationServices", "BudgetCosts_BudgetInvoiceId", c => c.Int());
            AddColumn("dbo.AdminCosts", "BudgetCosts_BudgetInvoiceId", c => c.Int());
            DropForeignKey("dbo.BudgetCosts", "MonthId", "dbo.Months");
            DropIndex("dbo.BudgetCosts", new[] { "ParticipationCost_PSId" });
            DropIndex("dbo.BudgetCosts", new[] { "AdminCost_AdminCostId" });
            DropIndex("dbo.BudgetCosts", new[] { "MonthId" });
            AlterColumn("dbo.ResidentialMIRs", "TotOtherClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotClientsCaseHrs", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotCaseHrs", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotClientsinEduClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotEduClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "ParticipatingFathers", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "ClientsJobEduServ", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotA2ABedNights", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotA2AEnrollment", c => c.Int(nullable: false));
            AlterColumn("dbo.ResidentialMIRs", "TotBedNights", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotOtherClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotClientsCaseHrs", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotCaseHrs", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotClientsinEduClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotEduClasses", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "ParticipatingFathers", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "ClientsJobEduServ", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotA2ABedNights", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotA2AEnrollment", c => c.Int(nullable: false));
            AlterColumn("dbo.NonResidentialMIRs", "TotBedNights", c => c.Int(nullable: false));
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
            RenameColumn(table: "dbo.BudgetCosts", name: "ParticipationCost_PSId", newName: "BudgetCosts_BudgetInvoiceId");
            RenameColumn(table: "dbo.BudgetCosts", name: "AdminCost_AdminCostId", newName: "BudgetCosts_BudgetInvoiceId");
            CreateIndex("dbo.ParticipationServices", "BudgetCosts_BudgetInvoiceId");
            CreateIndex("dbo.AdminCosts", "BudgetCosts_BudgetInvoiceId");
        }
    }
}
