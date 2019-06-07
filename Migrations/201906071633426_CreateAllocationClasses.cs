namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAllocationClasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllocatedBudgets", "CycleEndAdjustments", c => c.Double(nullable: false));
            DropColumn("dbo.SubContractors", "AllocatedContractAmount");
            DropColumn("dbo.SubContractors", "AllocatedAdjustments");
            DropColumn("dbo.AllocatedBudgets", "Region");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AllocatedBudgets", "Region", c => c.Int());
            AddColumn("dbo.SubContractors", "AllocatedAdjustments", c => c.Double(nullable: false));
            AddColumn("dbo.SubContractors", "AllocatedContractAmount", c => c.Double(nullable: false));
            DropColumn("dbo.AllocatedBudgets", "CycleEndAdjustments");
        }
    }
}
