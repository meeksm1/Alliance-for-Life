namespace Alliance_for_Life.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateAdminTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SurveysViewModels", "MonthId", "dbo.Months");
            DropForeignKey("dbo.SurveysViewModels", "OrgName_SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.SurveysViewModels", "SubcontractorId_SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.SurveysViewModels", new[] { "MonthId" });
            DropIndex("dbo.SurveysViewModels", new[] { "OrgName_SubcontractorId" });
            DropIndex("dbo.SurveysViewModels", new[] { "SubcontractorId_SubcontractorId" });
            CreateTable(
                "dbo.AdminCosts",
                c => new
                    {
                        AdminCostId = c.Int(nullable: false, identity: true),
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
                        RegionId = c.Int(nullable: false),
                        MonthId = c.Int(nullable: false),
                        SubcontractorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdminCostId)
                .ForeignKey("dbo.Months", t => t.MonthId, cascadeDelete: true)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .ForeignKey("dbo.SubContractors", t => t.SubcontractorId)
                .Index(t => t.RegionId)
                .Index(t => t.MonthId)
                .Index(t => t.SubcontractorId);
            
            CreateTable(
                "dbo.ParticipationServices",
                c => new
                    {
                        PSId = c.Int(nullable: false, identity: true),
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
                        RegionId = c.Int(nullable: false),
                        MonthId = c.Int(nullable: false),
                        SubcontractorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PSId)
                .ForeignKey("dbo.Months", t => t.MonthId, cascadeDelete: true)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .ForeignKey("dbo.SubContractors", t => t.SubcontractorId, cascadeDelete: false)
                .Index(t => t.RegionId)
                .Index(t => t.MonthId)
                .Index(t => t.SubcontractorId);
            
            AddForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors", "SubcontractorId");
            DropTable("dbo.SurveysViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SurveysViewModels",
                c => new
                    {
                        SurveyId = c.Int(nullable: false, identity: true),
                        MonthId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        SurveysCompleted = c.Int(nullable: false),
                        Heading = c.String(),
                        OrgName_SubcontractorId = c.Int(nullable: false),
                        SubcontractorId_SubcontractorId = c.Int(),
                    })
                .PrimaryKey(t => t.SurveyId);
            
            DropForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.ParticipationServices", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.ParticipationServices", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.ParticipationServices", "MonthId", "dbo.Months");
            DropForeignKey("dbo.AdminCosts", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.AdminCosts", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.AdminCosts", "MonthId", "dbo.Months");
            DropIndex("dbo.ParticipationServices", new[] { "SubcontractorId" });
            DropIndex("dbo.ParticipationServices", new[] { "MonthId" });
            DropIndex("dbo.ParticipationServices", new[] { "RegionId" });
            DropIndex("dbo.AdminCosts", new[] { "SubcontractorId" });
            DropIndex("dbo.AdminCosts", new[] { "MonthId" });
            DropIndex("dbo.AdminCosts", new[] { "RegionId" });
            DropTable("dbo.ParticipationServices");
            DropTable("dbo.AdminCosts");
            CreateIndex("dbo.SurveysViewModels", "SubcontractorId_SubcontractorId");
            CreateIndex("dbo.SurveysViewModels", "OrgName_SubcontractorId");
            CreateIndex("dbo.SurveysViewModels", "MonthId");
            AddForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
            AddForeignKey("dbo.SurveysViewModels", "SubcontractorId_SubcontractorId", "dbo.SubContractors", "SubcontractorId");
            AddForeignKey("dbo.SurveysViewModels", "OrgName_SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: false);
            AddForeignKey("dbo.SurveysViewModels", "MonthId", "dbo.Months", "Id", cascadeDelete: true);
        }
    }
}
