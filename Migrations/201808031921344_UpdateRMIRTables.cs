namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRMIRTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResidentialMIRs", "ma2apercent", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResidentialMIRs", "ma2apercent");
        }
    }
}
