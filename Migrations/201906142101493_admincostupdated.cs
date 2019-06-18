namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admincostupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminCosts", "AllocatedBudget_AllocatedBudgetId", c => c.Guid());
            CreateIndex("dbo.AdminCosts", "AllocatedBudget_AllocatedBudgetId");
            AddForeignKey("dbo.AdminCosts", "AllocatedBudget_AllocatedBudgetId", "dbo.AllocatedBudgets", "AllocatedBudgetId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdminCosts", "AllocatedBudget_AllocatedBudgetId", "dbo.AllocatedBudgets");
            DropIndex("dbo.AdminCosts", new[] { "AllocatedBudget_AllocatedBudgetId" });
            DropColumn("dbo.AdminCosts", "AllocatedBudget_AllocatedBudgetId");
        }
    }
}
