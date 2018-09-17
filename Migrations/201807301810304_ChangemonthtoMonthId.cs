namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangemonthtoMonthId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResidentialMIRs", "Months_Id", "dbo.Months");
            DropIndex("dbo.ResidentialMIRs", new[] { "Months_Id" });
            RenameColumn(table: "dbo.ResidentialMIRs", name: "Months_Id", newName: "MonthId");
            RenameColumn(table: "dbo.ResidentialMIRs", name: "Subcontractor_ID", newName: "Subcontractors_ID");
            RenameIndex(table: "dbo.ResidentialMIRs", name: "IX_Subcontractor_ID", newName: "IX_Subcontractors_ID");
            AlterColumn("dbo.ResidentialMIRs", "MonthId", c => c.Int(nullable: false));
            CreateIndex("dbo.ResidentialMIRs", "MonthId");
            AddForeignKey("dbo.ResidentialMIRs", "MonthId", "dbo.Months", "Id", cascadeDelete: true);
            DropColumn("dbo.ResidentialMIRs", "Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResidentialMIRs", "Month", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.ResidentialMIRs", "MonthId", "dbo.Months");
            DropIndex("dbo.ResidentialMIRs", new[] { "MonthId" });
            AlterColumn("dbo.ResidentialMIRs", "MonthId", c => c.Int());
            RenameIndex(table: "dbo.ResidentialMIRs", name: "IX_Subcontractors_ID", newName: "IX_Subcontractor_ID");
            RenameColumn(table: "dbo.ResidentialMIRs", name: "Subcontractors_ID", newName: "Subcontractor_ID");
            RenameColumn(table: "dbo.ResidentialMIRs", name: "MonthId", newName: "Months_Id");
            CreateIndex("dbo.ResidentialMIRs", "Months_Id");
            AddForeignKey("dbo.ResidentialMIRs", "Months_Id", "dbo.Months", "Id");
        }
    }
}
