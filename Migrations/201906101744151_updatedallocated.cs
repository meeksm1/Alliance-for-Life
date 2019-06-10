namespace Alliance_for_Life.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updatedallocated : DbMigration
    {
        public override void Up()
        {

            AlterColumn("dbo.Invoices", "PSId", c => c.Guid(nullable: true));
            AlterColumn("dbo.Invoices", "AdminCostId", c => c.Guid(nullable: true));

        }

        public override void Down()
        {

            AlterColumn("dbo.Invoices", "PSId", c => c.Guid());
            AlterColumn("dbo.Invoices", "AdminCostId", c => c.Guid());

        }
    }
}
