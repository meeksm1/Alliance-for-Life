namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdateAdminAndPartClasseswithRegionProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminCosts", "Region", c => c.Int());
            AddColumn("dbo.ParticipationServices", "Region", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParticipationServices", "Region");
            DropColumn("dbo.AdminCosts", "Region");
        }
    }
}
