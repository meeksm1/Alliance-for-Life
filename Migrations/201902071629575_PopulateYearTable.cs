namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateYearTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BudgetCosts", "YearId", c => c.Int(nullable: false));
            CreateIndex("dbo.BudgetCosts", "YearId");
            AddForeignKey("dbo.BudgetCosts", "YearId", "dbo.Years", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BudgetCosts", "YearId", "dbo.Years");
            DropIndex("dbo.BudgetCosts", new[] { "YearId" });
            DropColumn("dbo.BudgetCosts", "YearId");
        }
    }
}
