namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNonResMIRTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NonResidentialMIRs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subcontractor = c.Int(nullable: false),
                        TotBedNights = c.Int(nullable: false),
                        TotA2AEnrollment = c.Int(nullable: false),
                        TotA2ABedNights = c.Int(nullable: false),
                        MonthId = c.Int(nullable: false),
                        MA2Apercent = c.Double(nullable: false),
                        ClientsJobEduServ = c.Int(nullable: false),
                        ParticipatingFathers = c.Int(nullable: false),
                        TotEduClasses = c.Int(nullable: false),
                        TotClientsinEduClasses = c.Int(nullable: false),
                        TotCaseHrs = c.Int(nullable: false),
                        TotClientsCaseHrs = c.Int(nullable: false),
                        TotOtherClasses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Months", t => t.MonthId, cascadeDelete: true)
                .Index(t => t.MonthId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NonResidentialMIRs", "MonthId", "dbo.Months");
            DropIndex("dbo.NonResidentialMIRs", new[] { "MonthId" });
            DropTable("dbo.NonResidentialMIRs");
        }
    }
}
