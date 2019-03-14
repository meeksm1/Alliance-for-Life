namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRESMIR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResidentialMIRs", "Month", c => c.Int());
            CreateIndex("dbo.ResidentialMIRs", "SubcontractorId");
            AddForeignKey("dbo.ResidentialMIRs", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
            DropColumn("dbo.ResidentialMIRs", "Months");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResidentialMIRs", "Months", c => c.Int());
            DropForeignKey("dbo.ResidentialMIRs", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.ResidentialMIRs", new[] { "SubcontractorId" });
            DropColumn("dbo.ResidentialMIRs", "Month");
        }
    }
}
