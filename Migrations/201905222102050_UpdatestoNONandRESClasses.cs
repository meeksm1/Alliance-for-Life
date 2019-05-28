namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatestoNONandRESClasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NonResidentialMIRs", "TotOverallServed", c => c.Double(nullable: false));
            AddColumn("dbo.ResidentialMIRs", "TotOverallServed", c => c.Double(nullable: false));
            AddColumn("dbo.ResidentialMIRs", "NonResidentialId", c => c.Guid(nullable: false));
        }
            
        
        public override void Down()
        {
           
            DropIndex("dbo.ResidentialMIRs", new[] { "NonResidentialId" });
            DropColumn("dbo.ResidentialMIRs", "NonResidentialId");
            DropColumn("dbo.ResidentialMIRs", "TotOverallServed");
            DropColumn("dbo.NonResidentialMIRs", "TotOverallServed");
        }
    }
}
