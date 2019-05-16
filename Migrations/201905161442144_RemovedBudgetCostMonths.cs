namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBudgetCostMonths : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BudgetCosts", "Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BudgetCosts", "Month", c => c.Int());
        }
    }
}
