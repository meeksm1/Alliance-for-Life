namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSurveyProperties1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Surveys", "SubcontractorId");
            CreateIndex("dbo.Surveys", "MonthId");
            AddForeignKey("dbo.Surveys", "MonthId", "dbo.Months", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Surveys", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surveys", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.Surveys", "MonthId", "dbo.Months");
            DropIndex("dbo.Surveys", new[] { "MonthId" });
            DropIndex("dbo.Surveys", new[] { "SubcontractorId" });
        }
    }
}
