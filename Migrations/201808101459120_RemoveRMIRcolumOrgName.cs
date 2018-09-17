namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRMIRcolumOrgName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResidentialMIRs", "OrgName_ID", "dbo.SubContractors");
            DropIndex("dbo.ResidentialMIRs", new[] { "OrgName_ID" });
            DropColumn("dbo.ResidentialMIRs", "OrgName_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResidentialMIRs", "OrgName_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.ResidentialMIRs", "OrgName_ID");
            AddForeignKey("dbo.ResidentialMIRs", "OrgName_ID", "dbo.SubContractors", "ID", cascadeDelete: true);
        }
    }
}
