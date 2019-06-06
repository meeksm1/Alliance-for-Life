namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allocatedbudget1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllocatedBudgets", "AllocationAdjustedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.AllocatedBudgets", "AllocationAdjusted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AllocatedBudgets", "AllocationAdjusted", c => c.DateTime(nullable: false));
            DropColumn("dbo.AllocatedBudgets", "AllocationAdjustedDate");
        }
    }
}
