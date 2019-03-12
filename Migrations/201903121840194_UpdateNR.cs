namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNR : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.NonResidentialMIRs", "SubcontractorId");
            AddForeignKey("dbo.NonResidentialMIRs", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NonResidentialMIRs", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.NonResidentialMIRs", new[] { "SubcontractorId" });
        }
    }
}
