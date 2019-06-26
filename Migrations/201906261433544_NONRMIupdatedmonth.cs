namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NONRMIupdatedmonth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NonResidentialMIRs", "Months", c => c.Int());
            DropColumn("dbo.NonResidentialMIRs", "Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NonResidentialMIRs", "Month", c => c.Int());
            DropColumn("dbo.NonResidentialMIRs", "Months");
        }
    }
}
