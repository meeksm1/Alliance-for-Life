namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAllocationClasses1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllocatedBudgets", "Invoice_InvoiceId", c => c.Guid());
            CreateIndex("dbo.AllocatedBudgets", "Invoice_InvoiceId");
            AddForeignKey("dbo.AllocatedBudgets", "Invoice_InvoiceId", "dbo.Invoices", "InvoiceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AllocatedBudgets", "Invoice_InvoiceId", "dbo.Invoices");
            DropIndex("dbo.AllocatedBudgets", new[] { "Invoice_InvoiceId" });
            DropColumn("dbo.AllocatedBudgets", "Invoice_InvoiceId");
        }
    }
}
