namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAdminandPartIdsForInvoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "AdminCostId", "dbo.AdminCosts");
            DropIndex("dbo.Invoices", new[] { "AdminCostId" });
            RenameColumn(table: "dbo.Invoices", name: "AdminCostId", newName: "AdminCosts_AdminCostId");
            AlterColumn("dbo.Invoices", "AdminCosts_AdminCostId", c => c.Guid());
            CreateIndex("dbo.Invoices", "AdminCosts_AdminCostId");
            AddForeignKey("dbo.Invoices", "AdminCosts_AdminCostId", "dbo.AdminCosts", "AdminCostId");
            DropColumn("dbo.Invoices", "PartServId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "PartServId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Invoices", "AdminCosts_AdminCostId", "dbo.AdminCosts");
            DropIndex("dbo.Invoices", new[] { "AdminCosts_AdminCostId" });
            AlterColumn("dbo.Invoices", "AdminCosts_AdminCostId", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.Invoices", name: "AdminCosts_AdminCostId", newName: "AdminCostId");
            CreateIndex("dbo.Invoices", "AdminCostId");
            AddForeignKey("dbo.Invoices", "AdminCostId", "dbo.AdminCosts", "AdminCostId", cascadeDelete: true);
        }
    }
}
