namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAffiliateEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubContractors", "AffiliateRegion", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubContractors", "AffiliateRegion");
        }
    }
}
