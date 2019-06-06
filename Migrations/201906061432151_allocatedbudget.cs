namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allocatedbudget : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllocatedBudgets",
                c => new
                    {
                        AllocatedBudgetID = c.Guid(nullable: false),
                        SubcontractorId = c.Guid(nullable: false),
                        Region = c.Int(),
                        Month = c.Int(),
                        Year = c.Int(nullable: false),
                        AllocatedNewBudget = c.Double(nullable: false),
                        AllocatedOldBudget = c.Double(nullable: false),
                        AllocationAdjusted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AllocatedBudgetID)
                .ForeignKey("dbo.SubContractors", t => t.SubcontractorId, cascadeDelete: true)
                .Index(t => t.SubcontractorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AllocatedBudgets", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.AllocatedBudgets", new[] { "SubcontractorId" });
            DropTable("dbo.AllocatedBudgets");
        }
    }
}
