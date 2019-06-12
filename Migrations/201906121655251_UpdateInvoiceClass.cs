namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvoiceClass : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AllocatedBudgets", new[] { "InvoiceID" });
            RenameColumn(table: "dbo.Invoices", name: "InvoiceID", newName: "AllocatedBudgetID");
            CreateIndex("dbo.Invoices", "AllocatedBudgetID");
            DropColumn("dbo.Invoices", "BeginningAllocation");
            DropColumn("dbo.Invoices", "AdjustedAllocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "AdjustedAllocation", c => c.Double(nullable: false));
            AddColumn("dbo.Invoices", "BeginningAllocation", c => c.Double(nullable: false));
            DropIndex("dbo.Invoices", new[] { "AllocatedBudgetID" });
            RenameColumn(table: "dbo.Invoices", name: "AllocatedBudgetID", newName: "InvoiceID");
            CreateIndex("dbo.AllocatedBudgets", "InvoiceID");
        }
    }
}
