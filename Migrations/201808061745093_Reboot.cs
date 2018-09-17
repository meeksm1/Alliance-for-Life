namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reboot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientLists", "Subcontractor", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientLists", "Subcontractor");
        }
    }
}
