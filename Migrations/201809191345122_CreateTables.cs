namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminCosts",
                c => new
                    {
                        MonthId = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
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
                        Region_SubcontractorId = c.Int(),
                    })
                .PrimaryKey(t => t.MonthId)
                .ForeignKey("dbo.Months", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.SubContractors", t => t.Region_SubcontractorId)
                .Index(t => t.Id)
                .Index(t => t.Region_SubcontractorId);
            
            CreateTable(
                "dbo.Months",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Months = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubContractors",
                c => new
                    {
                        SubcontractorId = c.Int(nullable: false, identity: true),
                        AdministratorId = c.String(nullable: false, maxLength: 128),
                        OrgName = c.String(nullable: false, maxLength: 255),
                        City = c.String(nullable: false, maxLength: 255),
                        County = c.String(nullable: false, maxLength: 255),
                        RegionId = c.Int(nullable: false),
                        EIN = c.Int(nullable: false),
                        State = c.String(nullable: false, maxLength: 25),
                        ZipCode = c.Int(nullable: false),
                        AllocatedContractAmount = c.Int(nullable: false),
                        AllocatedAdjustments = c.Int(nullable: false),
                        Address1 = c.String(),
                        PoBox = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SubcontractorId)
                .ForeignKey("dbo.AspNetUsers", t => t.AdministratorId, cascadeDelete: true)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.AdministratorId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        Subcontractor = c.Int(nullable: false),
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
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Regions = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        AssetID = c.Guid(nullable: false),
                        Barcode = c.String(),
                        SerialNumber = c.String(),
                        FacilitySite = c.String(),
                        PMGuide = c.String(),
                        AstID = c.String(nullable: false),
                        ChildAsset = c.String(),
                        GeneralAssetDescription = c.String(),
                        SecondaryAssetDescription = c.String(),
                        Quantity = c.Int(nullable: false),
                        Manufacturer = c.String(),
                        ModelNumber = c.String(),
                        Building = c.String(),
                        Floor = c.String(),
                        Corridor = c.String(),
                        RoomNo = c.String(),
                        MERNo = c.String(),
                        EquipSystem = c.String(),
                        Comments = c.String(),
                        Issued = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AssetID);
            
            CreateTable(
                "dbo.BudgetCosts",
                c => new
                    {
                        MonthId = c.Int(nullable: false, identity: true),
                        RegionId = c.Int(nullable: false),
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
                        Months_Id = c.Int(),
                    })
                .PrimaryKey(t => t.MonthId)
                .ForeignKey("dbo.Months", t => t.Months_Id)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId)
                .Index(t => t.Months_Id);
            
            CreateTable(
                "dbo.ClientLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subcontractor = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        DOB = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        AdministratorId = c.String(maxLength: 128),
                        OrgName_SubcontractorId = c.Int(),
                        Subcontractors_SubcontractorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AdministratorId)
                .ForeignKey("dbo.SubContractors", t => t.OrgName_SubcontractorId)
                .ForeignKey("dbo.SubContractors", t => t.Subcontractors_SubcontractorId)
                .Index(t => t.AdministratorId)
                .Index(t => t.OrgName_SubcontractorId)
                .Index(t => t.Subcontractors_SubcontractorId);
            
            CreateTable(
                "dbo.NonResidentialMIRs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subcontractor = c.Int(nullable: false),
                        TotBedNights = c.Int(nullable: false),
                        TotA2AEnrollment = c.Int(nullable: false),
                        TotA2ABedNights = c.Int(nullable: false),
                        MonthId = c.Int(nullable: false),
                        MA2Apercent = c.Double(nullable: false),
                        ClientsJobEduServ = c.Int(nullable: false),
                        ParticipatingFathers = c.Int(nullable: false),
                        TotEduClasses = c.Int(nullable: false),
                        TotClientsinEduClasses = c.Int(nullable: false),
                        TotCaseHrs = c.Int(nullable: false),
                        TotClientsCaseHrs = c.Int(nullable: false),
                        TotOtherClasses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Months", t => t.MonthId, cascadeDelete: true)
                .Index(t => t.MonthId);
            
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
            
            CreateTable(
                "dbo.ResidentialMIRs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subcontractor = c.Int(nullable: false),
                        TotBedNights = c.Int(nullable: false),
                        TotA2AEnrollment = c.Int(nullable: false),
                        TotA2ABedNights = c.Int(nullable: false),
                        MonthId = c.Int(nullable: false),
                        MA2Apercent = c.Double(nullable: false),
                        ClientsJobEduServ = c.Int(nullable: false),
                        ParticipatingFathers = c.Int(nullable: false),
                        TotEduClasses = c.Int(nullable: false),
                        TotClientsinEduClasses = c.Int(nullable: false),
                        TotCaseHrs = c.Int(nullable: false),
                        TotClientsCaseHrs = c.Int(nullable: false),
                        TotOtherClasses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Months", t => t.MonthId, cascadeDelete: true)
                .Index(t => t.MonthId);
            
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
                        MonthId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        SurveysCompleted = c.Int(nullable: false),
                        OrgName_SubcontractorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyId)
                .ForeignKey("dbo.Months", t => t.MonthId, cascadeDelete: true)
                .ForeignKey("dbo.SubContractors", t => t.OrgName_SubcontractorId, cascadeDelete: true)
                .Index(t => t.MonthId)
                .Index(t => t.OrgName_SubcontractorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surveys", "OrgName_SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.Surveys", "MonthId", "dbo.Months");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ResidentialMIRs", "MonthId", "dbo.Months");
            DropForeignKey("dbo.NonResidentialMIRs", "MonthId", "dbo.Months");
            DropForeignKey("dbo.ClientLists", "Subcontractors_SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.ClientLists", "OrgName_SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.ClientLists", "AdministratorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetCosts", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.BudgetCosts", "Months_Id", "dbo.Months");
            DropForeignKey("dbo.AdminCosts", "Region_SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.SubContractors", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.SubContractors", "AdministratorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AdminCosts", "Id", "dbo.Months");
            DropIndex("dbo.Surveys", new[] { "OrgName_SubcontractorId" });
            DropIndex("dbo.Surveys", new[] { "MonthId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ResidentialMIRs", new[] { "MonthId" });
            DropIndex("dbo.NonResidentialMIRs", new[] { "MonthId" });
            DropIndex("dbo.ClientLists", new[] { "Subcontractors_SubcontractorId" });
            DropIndex("dbo.ClientLists", new[] { "OrgName_SubcontractorId" });
            DropIndex("dbo.ClientLists", new[] { "AdministratorId" });
            DropIndex("dbo.BudgetCosts", new[] { "Months_Id" });
            DropIndex("dbo.BudgetCosts", new[] { "RegionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SubContractors", new[] { "RegionId" });
            DropIndex("dbo.SubContractors", new[] { "AdministratorId" });
            DropIndex("dbo.AdminCosts", new[] { "Region_SubcontractorId" });
            DropIndex("dbo.AdminCosts", new[] { "Id" });
            DropTable("dbo.Surveys");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ResidentialMIRs");
            DropTable("dbo.ParticipationServices");
            DropTable("dbo.NonResidentialMIRs");
            DropTable("dbo.ClientLists");
            DropTable("dbo.BudgetCosts");
            DropTable("dbo.Assets");
            DropTable("dbo.Regions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SubContractors");
            DropTable("dbo.Months");
            DropTable("dbo.AdminCosts");
        }
    }
}
