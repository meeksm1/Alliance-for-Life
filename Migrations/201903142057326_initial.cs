namespace Alliance_for_Life.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "AdminCosts_AdminCostId", "dbo.AdminCosts");
            DropForeignKey("dbo.Invoices", "ParticipationService_PSId", "dbo.ParticipationServices");
            DropIndex("dbo.Invoices", new[] { "AdminCosts_AdminCostId" });
            DropIndex("dbo.Invoices", new[] { "ParticipationService_PSId" });

        }

        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "PSId", "dbo.ParticipationServices");
            DropForeignKey("dbo.Invoices", "AdminCostId", "dbo.AdminCosts");
            DropIndex("dbo.Invoices", new[] { "PSId" });
            DropIndex("dbo.Invoices", new[] { "AdminCostId" });

        }
    }
}
