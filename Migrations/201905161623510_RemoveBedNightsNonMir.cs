namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBedNightsNonMir : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.NonResidentialMIRs", "TotBedNights");
            DropColumn("dbo.NonResidentialMIRs", "TotA2ABedNights");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NonResidentialMIRs", "TotA2ABedNights", c => c.Double(nullable: false));
            AddColumn("dbo.NonResidentialMIRs", "TotBedNights", c => c.Double(nullable: false));
        }
    }
}
