namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeinvoicemodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "DirectAdminCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "ParticipantServices", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "GrandTotal", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "DepositAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "BeginningAllocation", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "AdjustedAllocation", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "BalanceRemaining", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "BalanceRemaining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "AdjustedAllocation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "BeginningAllocation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "DepositAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "GrandTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "ParticipantServices", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "DirectAdminCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
