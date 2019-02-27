namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Createcascadepaths : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdminCosts", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors");
            AddForeignKey("dbo.AdminCosts", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: false);
            AddForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.AdminCosts", "SubcontractorId", "dbo.SubContractors");
            AddForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors", "SubcontractorId");
            AddForeignKey("dbo.AdminCosts", "SubcontractorId", "dbo.SubContractors", "SubcontractorId");
        }
    }
}
