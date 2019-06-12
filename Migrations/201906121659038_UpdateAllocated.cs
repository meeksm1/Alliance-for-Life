namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAllocated : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Invoices", new[] { "AllocatedBudgetID" });
            CreateIndex("dbo.Invoices", "AllocatedBudgetId");
            DropColumn("dbo.AllocatedBudgets", "Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AllocatedBudgets", "Month", c => c.Int());
            DropIndex("dbo.Invoices", new[] { "AllocatedBudgetId" });
            CreateIndex("dbo.Invoices", "AllocatedBudgetID");
        }
    }
}
