namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateActiveUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubContractors", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubContractors", "Active");
        }
    }
}
