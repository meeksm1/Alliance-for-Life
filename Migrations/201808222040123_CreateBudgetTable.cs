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
                    })
                .PrimaryKey(t => t.MonthId)
                .ForeignKey("dbo.Months", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.SubContractors", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BudgetCosts", "RegionId", "dbo.SubContractors");
            DropForeignKey("dbo.BudgetCosts", "Id", "dbo.Months");
            DropIndex("dbo.BudgetCosts", new[] { "RegionId" });
            DropIndex("dbo.BudgetCosts", new[] { "Id" });
            DropTable("dbo.BudgetCosts");
        }
    }
}
