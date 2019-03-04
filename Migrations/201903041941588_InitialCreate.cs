namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminCosts",
                c => new
                    {
                        AdminCostId = c.Guid(nullable: false),
                        ASalandWages = c.Double(nullable: false),
                        ABackgroundCheck = c.Double(nullable: false),
                        EFTFees = c.Double(nullable: false),
                        AEmpBenefits = c.Double(nullable: false),
                        AEmpTravel = c.Double(nullable: false),
                        AEmpTraining = c.Double(nullable: false),
                        AOfficeRent = c.Double(nullable: false),
                        AOfficeUtilities = c.Double(nullable: false),
                        AFacilityIns = c.Double(nullable: false),
                        AOfficeSupplies = c.Double(nullable: false),
                        AEquipment = c.Double(nullable: false),
                        AOfficeCommunications = c.Double(nullable: false),
                        AOfficeMaint = c.Double(nullable: false),
                        AConsulting = c.Double(nullable: false),
                        AJanitorServices = c.Double(nullable: false),
                        ADepreciation = c.Double(nullable: false),
                        ATechSupport = c.Double(nullable: false),
                        ASecurityServices = c.Double(nullable: false),
                        AOther = c.Double(nullable: false),
                        AOther2 = c.Double(nullable: false),
                        AOther3 = c.Double(nullable: false),
                        StateAdminFee = c.Double(nullable: false),
                        ATotCosts = c.Double(nullable: false),
                        SubmittedDate = c.DateTime(nullable: false),
                        Region = c.Int(),
                        Month = c.Int(),
                        SubcontractorId = c.Guid(nullable: false),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdminCostId)
                .ForeignKey("dbo.SubContractors", t => t.SubcontractorId, cascadeDelete: true)
                .Index(t => t.SubcontractorId);
            
            CreateTable(
                "dbo.SubContractors",
                c => new
                    {
                        SubcontractorId = c.Guid(nullable: false),
                        AdministratorId = c.String(nullable: false, maxLength: 128),
                        OrgName = c.String(nullable: false, maxLength: 255),
                        Director = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 255),
                        County = c.String(nullable: false, maxLength: 255),
                        Region = c.Int(nullable: false),
                        EIN = c.Int(nullable: false),
                        State = c.String(nullable: false, maxLength: 25),
                        ZipCode = c.Int(nullable: false),
                        AllocatedContractAmount = c.Double(nullable: false),
                        AllocatedAdjustments = c.Double(nullable: false),
                        Address1 = c.String(),
                        PoBox = c.String(),
                        Active = c.Boolean(nullable: false),
                        SubmittedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SubcontractorId)
                .ForeignKey("dbo.AspNetUsers", t => t.AdministratorId, cascadeDelete: true)
                .Index(t => t.AdministratorId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        SubcontractorId = c.Guid(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BudgetCosts",
                c => new
                    {
                        BudgetInvoiceId = c.Guid(nullable: false),
                        ASalandWages = c.Double(nullable: false),
                        AEmpBenefits = c.Double(nullable: false),
                        AEmpTravel = c.Double(nullable: false),
                        AEmpTraining = c.Double(nullable: false),
                        AOfficeRent = c.Double(nullable: false),
                        AOfficeUtilities = c.Double(nullable: false),
                        AFacilityIns = c.Double(nullable: false),
                        AOfficeSupplies = c.Double(nullable: false),
                        AEquipment = c.Double(nullable: false),
                        AOfficeCommunications = c.Double(nullable: false),
                        AOfficeMaint = c.Double(nullable: false),
                        AConsulting = c.Double(nullable: false),
                        SubConPayCost = c.Double(nullable: false),
                        BackgrounCheck = c.Double(nullable: false),
                        Other = c.Double(nullable: false),
                        AJanitorServices = c.Double(nullable: false),
                        ADepreciation = c.Double(nullable: false),
                        ATechSupport = c.Double(nullable: false),
                        ASecurityServices = c.Double(nullable: false),
                        ATotCosts = c.Double(nullable: false),
                        AdminFee = c.Double(nullable: false),
                        Trasportation = c.Double(nullable: false),
                        JobTraining = c.Double(nullable: false),
                        TuitionAssistance = c.Double(nullable: false),
                        ContractedResidential = c.Double(nullable: false),
                        UtilityAssistance = c.Double(nullable: false),
                        EmergencyShelter = c.Double(nullable: false),
                        HousingAssistance = c.Double(nullable: false),
                        Childcare = c.Double(nullable: false),
                        Clothing = c.Double(nullable: false),
                        Food = c.Double(nullable: false),
                        Supplies = c.Double(nullable: false),
                        RFO = c.Double(nullable: false),
                        BTotal = c.Double(nullable: false),
                        Maxtot = c.Double(nullable: false),
                        SubmittedDate = c.DateTime(nullable: false),
                        Month = c.Int(),
                        Region = c.Int(),
                        Year = c.Int(nullable: false),
                        AdminCost_AdminCostId = c.Guid(),
                        ParticipationCost_PSId = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BudgetInvoiceId)
                .ForeignKey("dbo.AdminCosts", t => t.AdminCost_AdminCostId)
                .ForeignKey("dbo.ParticipationServices", t => t.ParticipationCost_PSId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.AdminCost_AdminCostId)
                .Index(t => t.ParticipationCost_PSId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ParticipationServices",
                c => new
                    {
                        PSId = c.Guid(nullable: false),
                        BackgroudCheck = c.Double(nullable: false),
                        PTranspotation = c.Double(nullable: false),
                        PJobTrain = c.Double(nullable: false),
                        PEducationAssistance = c.Double(nullable: false),
                        PBirthCerts = c.Double(nullable: false),
                        PResidentialCare = c.Double(nullable: false),
                        PUtilities = c.Double(nullable: false),
                        PHousingEmergency = c.Double(nullable: false),
                        PHousingAssistance = c.Double(nullable: false),
                        PChildCare = c.Double(nullable: false),
                        PClothing = c.Double(nullable: false),
                        PFood = c.Double(nullable: false),
                        PSupplies = c.Double(nullable: false),
                        POther = c.Double(nullable: false),
                        POther2 = c.Double(nullable: false),
                        POther3 = c.Double(nullable: false),
                        PTotals = c.Double(nullable: false),
                        SubmittedDate = c.DateTime(nullable: false),
                        Year = c.Int(nullable: false),
                        Region = c.Int(),
                        Month = c.Int(),
                        SubcontractorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PSId)
                .ForeignKey("dbo.SubContractors", t => t.SubcontractorId, cascadeDelete: true)
                .Index(t => t.SubcontractorId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Guid(nullable: false),
                        OrgName = c.String(),
                        DirectAdminCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ParticipantServices = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrandTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LessManagementFee = c.Double(nullable: false),
                        DepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BeginningAllocation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdjustedAllocation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillingDate = c.DateTime(nullable: false),
                        BalanceRemaining = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubmittedDate = c.DateTime(nullable: false),
                        Region = c.Int(),
                        Month = c.Int(),
                        Year = c.Int(nullable: false),
                        SubcontractorId = c.Guid(nullable: false),
                        AdminCostId = c.Guid(nullable: false),
                        PartServId = c.Int(nullable: false),
                        ParticipationService_PSId = c.Guid(),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.AdminCosts", t => t.AdminCostId, cascadeDelete: true)
                .ForeignKey("dbo.ParticipationServices", t => t.ParticipationService_PSId)
                .ForeignKey("dbo.SubContractors", t => t.SubcontractorId, cascadeDelete: false)
                .Index(t => t.SubcontractorId)
                .Index(t => t.AdminCostId)
                .Index(t => t.ParticipationService_PSId);
            
            CreateTable(
                "dbo.NonResidentialMIRs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SubcontractorId = c.Guid(nullable: false),
                        TotBedNights = c.Double(nullable: false),
                        TotA2AEnrollment = c.Double(nullable: false),
                        TotA2ABedNights = c.Double(nullable: false),
                        SubmittedDate = c.DateTime(nullable: false),
                        MA2Apercent = c.Double(nullable: false),
                        ClientsJobEduServ = c.Double(nullable: false),
                        ParticipatingFathers = c.Double(nullable: false),
                        TotEduClasses = c.Double(nullable: false),
                        TotClientsinEduClasses = c.Double(nullable: false),
                        TotCaseHrs = c.Double(nullable: false),
                        TotClientsCaseHrs = c.Double(nullable: false),
                        TotOtherClasses = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Months = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuarterlyStates",
                c => new
                    {
                        QuraterlyStateId = c.Guid(nullable: false),
                        TotPSforQuarter = c.Double(nullable: false),
                        TotDAforQuarter = c.Double(nullable: false),
                        StateFee = c.Double(nullable: false),
                        StateFeeQuarter = c.Double(nullable: false),
                        TotDACandPSMonthly = c.Double(nullable: false),
                        TotDAandPSQuarter = c.Double(nullable: false),
                        Month = c.Int(),
                        SubcontractorId = c.Guid(nullable: false),
                        AdminCostId = c.Guid(nullable: false),
                        ParticipationCostId = c.Guid(nullable: false),
                        ParticipationCost_PSId = c.Guid(),
                    })
                .PrimaryKey(t => t.QuraterlyStateId)
                .ForeignKey("dbo.AdminCosts", t => t.AdminCostId, cascadeDelete: true)
                .ForeignKey("dbo.ParticipationServices", t => t.ParticipationCost_PSId)
                .ForeignKey("dbo.SubContractors", t => t.SubcontractorId, cascadeDelete: false)
                .Index(t => t.SubcontractorId)
                .Index(t => t.AdminCostId)
                .Index(t => t.ParticipationCost_PSId);
            
            CreateTable(
                "dbo.ResidentialMIRs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SubmittedDate = c.DateTime(nullable: false),
                        SubcontractorId = c.Guid(nullable: false),
                        TotBedNights = c.Double(nullable: false),
                        TotA2AEnrollment = c.Double(nullable: false),
                        TotA2ABedNights = c.Double(nullable: false),
                        MA2Apercent = c.Double(nullable: false),
                        ClientsJobEduServ = c.Double(nullable: false),
                        ParticipatingFathers = c.Double(nullable: false),
                        TotEduClasses = c.Double(nullable: false),
                        TotClientsinEduClasses = c.Double(nullable: false),
                        TotCaseHrs = c.Double(nullable: false),
                        TotClientsCaseHrs = c.Double(nullable: false),
                        TotOtherClasses = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Months = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        SurveyId = c.Int(nullable: false, identity: true),
                        SubmittedDate = c.DateTime(nullable: false),
                        SurveysCompleted = c.Int(nullable: false),
                        Month = c.Int(),
                        SubcontractorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyId)
                .ForeignKey("dbo.SubContractors", t => t.SubcontractorId, cascadeDelete: true)
                .Index(t => t.SubcontractorId);
            
            CreateTable(
                "dbo.ClientLists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        DOB = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        SubmittedDate = c.DateTime(nullable: false),
                        SubcontractorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubContractors", t => t.SubcontractorId, cascadeDelete: true)
                .Index(t => t.SubcontractorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.Surveys", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.QuarterlyStates", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.QuarterlyStates", "ParticipationCost_PSId", "dbo.ParticipationServices");
            DropForeignKey("dbo.QuarterlyStates", "AdminCostId", "dbo.AdminCosts");
            DropForeignKey("dbo.Invoices", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.Invoices", "ParticipationService_PSId", "dbo.ParticipationServices");
            DropForeignKey("dbo.Invoices", "AdminCostId", "dbo.AdminCosts");
            DropForeignKey("dbo.BudgetCosts", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetCosts", "ParticipationCost_PSId", "dbo.ParticipationServices");
            DropForeignKey("dbo.ParticipationServices", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.BudgetCosts", "AdminCost_AdminCostId", "dbo.AdminCosts");
            DropForeignKey("dbo.AdminCosts", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.SubContractors", "AdministratorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ClientLists", new[] { "SubcontractorId" });
            DropIndex("dbo.Surveys", new[] { "SubcontractorId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.QuarterlyStates", new[] { "ParticipationCost_PSId" });
            DropIndex("dbo.QuarterlyStates", new[] { "AdminCostId" });
            DropIndex("dbo.QuarterlyStates", new[] { "SubcontractorId" });
            DropIndex("dbo.Invoices", new[] { "ParticipationService_PSId" });
            DropIndex("dbo.Invoices", new[] { "AdminCostId" });
            DropIndex("dbo.Invoices", new[] { "SubcontractorId" });
            DropIndex("dbo.ParticipationServices", new[] { "SubcontractorId" });
            DropIndex("dbo.BudgetCosts", new[] { "User_Id" });
            DropIndex("dbo.BudgetCosts", new[] { "ParticipationCost_PSId" });
            DropIndex("dbo.BudgetCosts", new[] { "AdminCost_AdminCostId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SubContractors", new[] { "AdministratorId" });
            DropIndex("dbo.AdminCosts", new[] { "SubcontractorId" });
            DropTable("dbo.ClientLists");
            DropTable("dbo.Surveys");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ResidentialMIRs");
            DropTable("dbo.QuarterlyStates");
            DropTable("dbo.NonResidentialMIRs");
            DropTable("dbo.Invoices");
            DropTable("dbo.ParticipationServices");
            DropTable("dbo.BudgetCosts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SubContractors");
            DropTable("dbo.AdminCosts");
        }
    }
}
