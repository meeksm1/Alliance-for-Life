namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aflstatedeposite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.A2AStateDeposits",
                c => new
                    {
                        A2AStateDepositsId = c.Guid(nullable: false),
                        StateDeposits = c.Double(nullable: false),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        A2AAllocatedBudget_A2AAllocatedBudgetId = c.Guid(),
                    })
                .PrimaryKey(t => t.A2AStateDepositsId)
                .ForeignKey("dbo.A2AAllocatedBudget", t => t.A2AAllocatedBudget_A2AAllocatedBudgetId)
                .Index(t => t.A2AAllocatedBudget_A2AAllocatedBudgetId);
            
            DropColumn("dbo.A2AAllocatedBudget", "StateDeposits");
            DropColumn("dbo.A2AAllocatedBudget", "Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.A2AAllocatedBudget", "Month", c => c.Int(nullable: false));
            AddColumn("dbo.A2AAllocatedBudget", "StateDeposits", c => c.Double(nullable: false));
            DropForeignKey("dbo.A2AStateDeposits", "A2AAllocatedBudget_A2AAllocatedBudgetId", "dbo.A2AAllocatedBudget");
            DropIndex("dbo.A2AStateDeposits", new[] { "A2AAllocatedBudget_A2AAllocatedBudgetId" });
            DropTable("dbo.A2AStateDeposits");
        }
    }
}
