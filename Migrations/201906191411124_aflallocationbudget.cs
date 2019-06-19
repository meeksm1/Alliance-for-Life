namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aflallocationbudget : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.A2AAllocatedBudget",
                c => new
                    {
                        A2AAllocatedBudgetId = c.Guid(nullable: false),
                        Year = c.Int(nullable: false),
                        BeginingBalance = c.Double(nullable: false),
                        StateDeposits = c.Double(nullable: false),
                        Month = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.A2AAllocatedBudgetId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.A2AAllocatedBudget");
        }
    }
}
