namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubContractors", "Region", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubContractors", "Region");
        }
    }
}
