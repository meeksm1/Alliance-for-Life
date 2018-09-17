namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientTableUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientLists", "Contractor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClientLists", new[] { "Contractor_Id" });
            RenameColumn(table: "dbo.ClientLists", name: "Contractor_Id", newName: "ContractorID");
            AddColumn("dbo.ClientLists", "Subcontractor", c => c.Int(nullable: false));
            AlterColumn("dbo.ClientLists", "ContractorID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ClientLists", "ContractorID");
            AddForeignKey("dbo.ClientLists", "ContractorID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientLists", "ContractorID", "dbo.AspNetUsers");
            DropIndex("dbo.ClientLists", new[] { "ContractorID" });
            AlterColumn("dbo.ClientLists", "ContractorID", c => c.String(maxLength: 128));
            DropColumn("dbo.ClientLists", "Subcontractor");
            RenameColumn(table: "dbo.ClientLists", name: "ContractorID", newName: "Contractor_Id");
            CreateIndex("dbo.ClientLists", "Contractor_Id");
            AddForeignKey("dbo.ClientLists", "Contractor_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
