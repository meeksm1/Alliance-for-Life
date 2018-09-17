namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRIMTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResidentialMIRs", "Subcontractor", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResidentialMIRs", "Subcontractor");
        }
    }
}
