namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surveys", "Month", c => c.Int());
            DropColumn("dbo.Surveys", "Months");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Surveys", "Months", c => c.Int());
            DropColumn("dbo.Surveys", "Month");
        }
    }
}
