namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMonthlyServiceClassesControlersViews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MonthlyServices", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.MonthlyServices", new[] { "SubcontractorId" });
            DropTable("dbo.MonthlyServices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MonthlyServices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SubmittedDate = c.DateTime(nullable: false),
                        SubcontractorId = c.Guid(nullable: false),
                        RTotBedNights = c.Double(nullable: false),
                        RTotA2AEnrollment = c.Double(nullable: false),
                        RTotA2ABedNights = c.Double(nullable: false),
                        RMA2Apercent = c.Double(nullable: false),
                        RClientsJobEduServ = c.Double(nullable: false),
                        RParticipatingFathers = c.Double(nullable: false),
                        RTotEduClasses = c.Double(nullable: false),
                        RTotClientsinEduClasses = c.Double(nullable: false),
                        RTotCaseHrs = c.Double(nullable: false),
                        RTotClientsCaseHrs = c.Double(nullable: false),
                        RTotOtherClasses = c.Double(nullable: false),
                        NTotA2AEnrollment = c.Double(nullable: false),
                        NMA2Apercent = c.Double(nullable: false),
                        NClientsJobEduServ = c.Double(nullable: false),
                        NParticipatingFathers = c.Double(nullable: false),
                        NTotEduClasses = c.Double(nullable: false),
                        NTotClientsinEduClasses = c.Double(nullable: false),
                        NTotCaseHrs = c.Double(nullable: false),
                        NTotClientsCaseHrs = c.Double(nullable: false),
                        NTotOtherClasses = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.MonthlyServices", "SubcontractorId");
            AddForeignKey("dbo.MonthlyServices", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
        }
    }
}
