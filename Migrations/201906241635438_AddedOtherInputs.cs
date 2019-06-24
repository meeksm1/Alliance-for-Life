namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOtherInputs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuarterlyStates", "AdminCostId", "dbo.AdminCosts");
            DropForeignKey("dbo.QuarterlyStates", "ParticipationCost_PSId", "dbo.ParticipationServices");
            DropForeignKey("dbo.QuarterlyStates", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.QuarterlyStates", new[] { "SubcontractorId" });
            DropIndex("dbo.QuarterlyStates", new[] { "AdminCostId" });
            DropIndex("dbo.QuarterlyStates", new[] { "ParticipationCost_PSId" });
            AddColumn("dbo.AdminCosts", "AOtherInput", c => c.String());
            AddColumn("dbo.AdminCosts", "AOtherInput2", c => c.String());
            AddColumn("dbo.AdminCosts", "AOtherInput3", c => c.String());
            AddColumn("dbo.ParticipationServices", "POtherInput", c => c.String());
            AddColumn("dbo.ParticipationServices", "POtherInput2", c => c.String());
            AddColumn("dbo.ParticipationServices", "POtherInput3", c => c.String());
            DropColumn("dbo.AdminCosts", "ABackgroundCheck");
            DropColumn("dbo.AdminCosts", "EFTFees");
            DropColumn("dbo.AdminCosts", "StateAdminFee");
            DropTable("dbo.QuarterlyStates");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.QuraterlyStateId);
            
            AddColumn("dbo.AdminCosts", "StateAdminFee", c => c.Double(nullable: false));
            AddColumn("dbo.AdminCosts", "EFTFees", c => c.Double(nullable: false));
            AddColumn("dbo.AdminCosts", "ABackgroundCheck", c => c.Double(nullable: false));
            DropColumn("dbo.ParticipationServices", "POtherInput3");
            DropColumn("dbo.ParticipationServices", "POtherInput2");
            DropColumn("dbo.ParticipationServices", "POtherInput");
            DropColumn("dbo.AdminCosts", "AOtherInput3");
            DropColumn("dbo.AdminCosts", "AOtherInput2");
            DropColumn("dbo.AdminCosts", "AOtherInput");
            CreateIndex("dbo.QuarterlyStates", "ParticipationCost_PSId");
            CreateIndex("dbo.QuarterlyStates", "AdminCostId");
            CreateIndex("dbo.QuarterlyStates", "SubcontractorId");
            AddForeignKey("dbo.QuarterlyStates", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
            AddForeignKey("dbo.QuarterlyStates", "ParticipationCost_PSId", "dbo.ParticipationServices", "PSId");
            AddForeignKey("dbo.QuarterlyStates", "AdminCostId", "dbo.AdminCosts", "AdminCostId", cascadeDelete: true);
        }
    }
}
