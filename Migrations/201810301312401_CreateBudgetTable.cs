namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBudgetTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BudgetCosts",
                c => new
                    {
                        BudgetInvoiceId = c.Int(nullable: false, identity: true),
                        ASalandWages = c.Int(nullable: false),
                        AEmpBenefits = c.Int(nullable: false),
                        AEmpTravel = c.Int(nullable: false),
                        AEmpTraining = c.Int(nullable: false),
                        AOfficeRent = c.Int(nullable: false),
                        AOfficeUtilities = c.Int(nullable: false),
                        AFacilityIns = c.Int(nullable: false),
                        AOfficeSupplies = c.Int(nullable: false),
                        AEquipment = c.Int(nullable: false),
                        AOfficeCommunications = c.Int(nullable: false),
                        AOfficeMaint = c.Int(nullable: false),
                        AConsulting = c.Int(nullable: false),
                        SubConPayCost = c.Int(nullable: false),
                        BackgrounCheck = c.Int(nullable: false),
                        Other = c.Int(nullable: false),
                        AJanitorServices = c.Int(nullable: false),
                        ADepreciation = c.Int(nullable: false),
                        ATechSupport = c.Int(nullable: false),
                        ASecurityServices = c.Int(nullable: false),
                        ATotCosts = c.Double(nullable: false),
                        AdminFee = c.Double(nullable: false),
                        Trasportation = c.Int(nullable: false),
                        JobTraining = c.Int(nullable: false),
                        TuitionAssistance = c.Int(nullable: false),
                        ContractedResidential = c.Int(nullable: false),
                        UtilityAssistance = c.Int(nullable: false),
                        EmergencyShelter = c.Int(nullable: false),
                        HousingAssistance = c.Int(nullable: false),
                        Childcare = c.Int(nullable: false),
                        Clothing = c.Int(nullable: false),
                        Food = c.Int(nullable: false),
                        Supplies = c.Int(nullable: false),
                        RFO = c.Int(nullable: false),
                        BTotal = c.Int(nullable: false),
                        Maxtot = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetInvoiceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BudgetCosts");
        }
    }
}
