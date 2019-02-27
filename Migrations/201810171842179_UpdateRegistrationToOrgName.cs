namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRegistrationToOrgName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "OrgName", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "SubcontractorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "SubcontractorId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "OrgName");
        }
    }
}
