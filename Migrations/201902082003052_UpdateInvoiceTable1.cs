namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvoiceTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "YearId", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "RegionId", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "MonthId", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "SubcontractorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "YearId");
            CreateIndex("dbo.Invoices", "RegionId");
            CreateIndex("dbo.Invoices", "MonthId");
            CreateIndex("dbo.Invoices", "SubcontractorId");
            AddForeignKey("dbo.Invoices", "MonthId", "dbo.Months", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Invoices", "RegionId", "dbo.Regions", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Invoices", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: false);
            AddForeignKey("dbo.Invoices", "YearId", "dbo.Years", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "YearId", "dbo.Years");
            DropForeignKey("dbo.Invoices", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.Invoices", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Invoices", "MonthId", "dbo.Months");
            DropIndex("dbo.Invoices", new[] { "SubcontractorId" });
            DropIndex("dbo.Invoices", new[] { "MonthId" });
            DropIndex("dbo.Invoices", new[] { "RegionId" });
            DropIndex("dbo.Invoices", new[] { "YearId" });
            DropColumn("dbo.Invoices", "SubcontractorId");
            DropColumn("dbo.Invoices", "MonthId");
            DropColumn("dbo.Invoices", "RegionId");
            DropColumn("dbo.Invoices", "YearId");
        }
    }
}
