namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSurveyProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Surveys", "MonthId", "dbo.Months");
            DropForeignKey("dbo.Surveys", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.Surveys", new[] { "SubcontractorId" });
            DropIndex("dbo.Surveys", new[] { "MonthId" });
            AddColumn("dbo.Surveys", "OrgName", c => c.String());
            AddColumn("dbo.Surveys", "Month", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surveys", "Month");
            DropColumn("dbo.Surveys", "OrgName");
            CreateIndex("dbo.Surveys", "MonthId");
            CreateIndex("dbo.Surveys", "SubcontractorId");
            AddForeignKey("dbo.Surveys", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
            AddForeignKey("dbo.Surveys", "MonthId", "dbo.Months", "Id", cascadeDelete: true);
        }
    }
}
