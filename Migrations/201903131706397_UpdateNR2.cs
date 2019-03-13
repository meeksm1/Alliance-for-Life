namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNR2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NonResidentialMIRs", "Month", c => c.Int());
            DropColumn("dbo.NonResidentialMIRs", "Months");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NonResidentialMIRs", "Months", c => c.Int());
            DropColumn("dbo.NonResidentialMIRs", "Month");
        }
    }
}
