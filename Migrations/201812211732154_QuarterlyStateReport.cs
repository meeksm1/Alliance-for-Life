namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuarterlyStateReport : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuarterlyStates", "AdminCost_AdminCostId", "dbo.AdminCosts");
            DropForeignKey("dbo.QuarterlyStates", "Month_Id", "dbo.Months");
            DropForeignKey("dbo.QuarterlyStates", "Subcontractor_SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.QuarterlyStates", new[] { "AdminCost_AdminCostId" });
            DropIndex("dbo.QuarterlyStates", new[] { "Month_Id" });
            DropIndex("dbo.QuarterlyStates", new[] { "Subcontractor_SubcontractorId" });
            RenameColumn(table: "dbo.QuarterlyStates", name: "AdminCost_AdminCostId", newName: "AdminCostId");
            RenameColumn(table: "dbo.QuarterlyStates", name: "Month_Id", newName: "MonthId");
            RenameColumn(table: "dbo.QuarterlyStates", name: "Subcontractor_SubcontractorId", newName: "SubcontractorId");
            AddColumn("dbo.QuarterlyStates", "ParticipationCostId", c => c.Int(nullable: false));
            AlterColumn("dbo.QuarterlyStates", "AdminCostId", c => c.Int(nullable: false));
            AlterColumn("dbo.QuarterlyStates", "MonthId", c => c.Int(nullable: false));
            AlterColumn("dbo.QuarterlyStates", "SubcontractorId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuarterlyStates", "MonthId");
            CreateIndex("dbo.QuarterlyStates", "SubcontractorId");
            CreateIndex("dbo.QuarterlyStates", "AdminCostId");
            AddForeignKey("dbo.QuarterlyStates", "AdminCostId", "dbo.AdminCosts", "AdminCostId", cascadeDelete: false);
            AddForeignKey("dbo.QuarterlyStates", "MonthId", "dbo.Months", "Id", cascadeDelete: false);
            AddForeignKey("dbo.QuarterlyStates", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuarterlyStates", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.QuarterlyStates", "MonthId", "dbo.Months");
            DropForeignKey("dbo.QuarterlyStates", "AdminCostId", "dbo.AdminCosts");
            DropIndex("dbo.QuarterlyStates", new[] { "AdminCostId" });
            DropIndex("dbo.QuarterlyStates", new[] { "SubcontractorId" });
            DropIndex("dbo.QuarterlyStates", new[] { "MonthId" });
            AlterColumn("dbo.QuarterlyStates", "SubcontractorId", c => c.Int());
            AlterColumn("dbo.QuarterlyStates", "MonthId", c => c.Int());
            AlterColumn("dbo.QuarterlyStates", "AdminCostId", c => c.Int());
            DropColumn("dbo.QuarterlyStates", "ParticipationCostId");
            RenameColumn(table: "dbo.QuarterlyStates", name: "SubcontractorId", newName: "Subcontractor_SubcontractorId");
            RenameColumn(table: "dbo.QuarterlyStates", name: "MonthId", newName: "Month_Id");
            RenameColumn(table: "dbo.QuarterlyStates", name: "AdminCostId", newName: "AdminCost_AdminCostId");
            CreateIndex("dbo.QuarterlyStates", "Subcontractor_SubcontractorId");
            CreateIndex("dbo.QuarterlyStates", "Month_Id");
            CreateIndex("dbo.QuarterlyStates", "AdminCost_AdminCostId");
            AddForeignKey("dbo.QuarterlyStates", "Subcontractor_SubcontractorId", "dbo.SubContractors", "SubcontractorId");
            AddForeignKey("dbo.QuarterlyStates", "Month_Id", "dbo.Months", "Id");
            AddForeignKey("dbo.QuarterlyStates", "AdminCost_AdminCostId", "dbo.AdminCosts", "AdminCostId");
        }
    }
}
