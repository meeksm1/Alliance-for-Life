namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvoiceTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "MonthId", "dbo.Months");
            DropForeignKey("dbo.Invoices", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.Invoices", "YearId", "dbo.Years");
            DropIndex("dbo.Invoices", new[] { "SubcontractorId" });
            DropIndex("dbo.Invoices", new[] { "MonthId" });
            DropIndex("dbo.Invoices", new[] { "YearId" });
            DropIndex("dbo.Invoices", new[] { "AdminCostId" });
            DropIndex("dbo.Invoices", new[] { "PSId" });
            AddColumn("dbo.Invoices", "OrgName", c => c.String());
            DropColumn("dbo.Invoices", "SubcontractorId");
            DropColumn("dbo.Invoices", "MonthId");
            DropColumn("dbo.Invoices", "YearId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "YearId", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "MonthId", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "SubcontractorId", c => c.Int(nullable: false));
            DropColumn("dbo.Invoices", "OrgName");
            CreateIndex("dbo.Invoices", "PSId");
            CreateIndex("dbo.Invoices", "AdminCostId");
            CreateIndex("dbo.Invoices", "YearId");
            CreateIndex("dbo.Invoices", "MonthId");
            CreateIndex("dbo.Invoices", "SubcontractorId");
            AddForeignKey("dbo.Invoices", "YearId", "dbo.Years", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Invoices", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
            AddForeignKey("dbo.Invoices", "MonthId", "dbo.Months", "Id", cascadeDelete: true);
        }
    }
}
