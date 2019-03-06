namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminCostAddedOrgNameProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminCosts", "OrgName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdminCosts", "OrgName");
        }
    }
}
