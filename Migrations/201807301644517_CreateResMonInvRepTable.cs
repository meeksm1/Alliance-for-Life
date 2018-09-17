namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateResMonInvRepTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResidentialMIRs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotBedNights = c.Int(nullable: false),
                        TotA2AEnrollment = c.Int(nullable: false),
                        TotA2ABedNights = c.Int(nullable: false),
                        Month = c.String(nullable: false, maxLength: 50),
                        Subcontractor_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubContractors", t => t.Subcontractor_ID, cascadeDelete: true)
                .Index(t => t.Subcontractor_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResidentialMIRs", "Subcontractor_ID", "dbo.SubContractors");
            DropIndex("dbo.ResidentialMIRs", new[] { "Subcontractor_ID" });
            DropTable("dbo.ResidentialMIRs");
        }
    }
}
