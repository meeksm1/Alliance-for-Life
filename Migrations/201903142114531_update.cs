namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "AdminCosts_AdminCostId", "dbo.AdminCosts");
            DropForeignKey("dbo.Invoices", "ParticipationService_PSId", "dbo.ParticipationServices");
            DropIndex("dbo.Invoices", new[] { "AdminCosts_AdminCostId" });
            DropIndex("dbo.Invoices", new[] { "ParticipationService_PSId" });
            RenameColumn(table: "dbo.Invoices", name: "AdminCosts_AdminCostId", newName: "AdminCostId");
            RenameColumn(table: "dbo.Invoices", name: "ParticipationService_PSId", newName: "PSId");
            AlterColumn("dbo.Invoices", "AdminCostId", c => c.Guid(nullable: true));
            AlterColumn("dbo.Invoices", "PSId", c => c.Guid(nullable: true));
            CreateIndex("dbo.Invoices", "AdminCostId");
            CreateIndex("dbo.Invoices", "PSId");
            AddForeignKey("dbo.Invoices", "AdminCostId", "dbo.AdminCosts", "AdminCostId", cascadeDelete: false);
            AddForeignKey("dbo.Invoices", "PSId", "dbo.ParticipationServices", "PSId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "PSId", "dbo.ParticipationServices");
            DropForeignKey("dbo.Invoices", "AdminCostId", "dbo.AdminCosts");
            DropIndex("dbo.Invoices", new[] { "PSId" });
            DropIndex("dbo.Invoices", new[] { "AdminCostId" });
            AlterColumn("dbo.Invoices", "PSId", c => c.Guid());
            AlterColumn("dbo.Invoices", "AdminCostId", c => c.Guid());
            RenameColumn(table: "dbo.Invoices", name: "PSId", newName: "ParticipationService_PSId");
            RenameColumn(table: "dbo.Invoices", name: "AdminCostId", newName: "AdminCosts_AdminCostId");
            CreateIndex("dbo.Invoices", "ParticipationService_PSId");
            CreateIndex("dbo.Invoices", "AdminCosts_AdminCostId");
            AddForeignKey("dbo.Invoices", "ParticipationService_PSId", "dbo.ParticipationServices", "PSId");
            AddForeignKey("dbo.Invoices", "AdminCosts_AdminCostId", "dbo.AdminCosts", "AdminCostId");
        }
    }
}
