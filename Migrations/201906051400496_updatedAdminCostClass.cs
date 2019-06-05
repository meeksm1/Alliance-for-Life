namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedAdminCostClass : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AdminCosts", "OrgName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminCosts", "OrgName", c => c.String());
        }
    }
}
