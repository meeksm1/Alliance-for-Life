namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAdminCost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminCosts", "AflBillable", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdminCosts", "AflBillable");
        }
    }
}
