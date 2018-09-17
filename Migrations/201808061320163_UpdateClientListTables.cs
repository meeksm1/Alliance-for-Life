namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClientListTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientLists", "Subcontractor", c => c.Int(nullable: false));
            AddColumn("dbo.ClientLists", "OrgName_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.ClientLists", "OrgName_ID");
            AddForeignKey("dbo.ClientLists", "OrgName_ID", "dbo.SubContractors", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientLists", "OrgName_ID", "dbo.SubContractors");
            DropIndex("dbo.ClientLists", new[] { "OrgName_ID" });
            DropColumn("dbo.ClientLists", "OrgName_ID");
            DropColumn("dbo.ClientLists", "Subcontractor");
        }
    }
}
