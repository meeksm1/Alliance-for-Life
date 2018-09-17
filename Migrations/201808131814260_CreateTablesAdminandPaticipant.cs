namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTablesAdminandPaticipant : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdminCosts", "Months_Id", "dbo.Months");
            CreateTable(
                "dbo.AdminCosts",
                c => new
                    {
                        MonthId = c.Int(nullable: false, identity: true),
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
                        AJanitorServices = c.Int(nullable: false),
                        ADepreciation = c.Int(nullable: false),
                        ATechSupport = c.Int(nullable: false),
                        ASecurityServices = c.Int(nullable: false),
                        AOther = c.Int(nullable: false),
                        AOther2 = c.Int(nullable: false),
                        AOther3 = c.Int(nullable: false),
                        ATotCosts = c.Int(nullable: false),
                        Months_Id = c.Int(),
                    })
                .PrimaryKey(t => t.MonthId)
                .ForeignKey("dbo.Months", t => t.Months_Id);
            
            CreateTable(
                "dbo.ParticipationServices",
                c => new
                    {
                        MonthId = c.Int(nullable: false, identity: true),
                        PTranspotation = c.Int(nullable: false),
                        PJobTrain = c.Int(nullable: false),
                        PEducationAssistance = c.Int(nullable: false),
                        PResidentialCare = c.Int(nullable: false),
                        PUtilities = c.Int(nullable: false),
                        PHousingEmergency = c.Int(nullable: false),
                        PHousingAssistance = c.Int(nullable: false),
                        PChildCare = c.Int(nullable: false),
                        PClothing = c.Int(nullable: false),
                        PFood = c.Int(nullable: false),
                        PSupplies = c.Int(nullable: false),
                        POther = c.Int(nullable: false),
                        POther2 = c.Int(nullable: false),
                        POther3 = c.Int(nullable: false),
                        PTotals = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MonthId);
            
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AdminCosts",
                c => new
                    {
                        MonthId = c.Int(nullable: false, identity: true),
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
                        AJanitorServices = c.Int(nullable: false),
                        ADepreciation = c.Int(nullable: false),
                        ATechSupport = c.Int(nullable: false),
                        ASecurityServices = c.Int(nullable: false),
                        AOther = c.Int(nullable: false),
                        AOther2 = c.Int(nullable: false),
                        AOther3 = c.Int(nullable: false),
                        ATotCosts = c.Int(nullable: false),
                        PTranspotation = c.Int(nullable: false),
                        PJobTrain = c.Int(nullable: false),
                        PEducationAssistance = c.Int(nullable: false),
                        PResidentialCare = c.Int(nullable: false),
                        PUtilities = c.Int(nullable: false),
                        PHousingEmergency = c.Int(nullable: false),
                        PHousingAssistance = c.Int(nullable: false),
                        PChildCare = c.Int(nullable: false),
                        PClothing = c.Int(nullable: false),
                        PFood = c.Int(nullable: false),
                        PSupplies = c.Int(nullable: false),
                        POther = c.Int(nullable: false),
                        POther2 = c.Int(nullable: false),
                        POther3 = c.Int(nullable: false),
                        PTotals = c.Int(nullable: false),
                        Months_Id = c.Int(),
                    })
                .PrimaryKey(t => t.MonthId);
            
            DropForeignKey("dbo.AdminCosts", "Months_Id", "dbo.Months");
            DropTable("dbo.ParticipationServices");
            DropTable("dbo.AdminCosts");
            AddForeignKey("dbo.AdminCosts", "Months_Id", "dbo.Months", "Id");
        }
    }
}
