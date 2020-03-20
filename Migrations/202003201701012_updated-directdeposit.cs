namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateddirectdeposit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DirectDeposits", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.DirectDeposits", "Month", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DirectDeposits", "Month");
            DropColumn("dbo.DirectDeposits", "Year");
        }
    }
}
