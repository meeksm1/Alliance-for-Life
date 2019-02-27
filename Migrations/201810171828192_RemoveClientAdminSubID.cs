namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveClientAdminSubID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientLists", "AdministratorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.ClientLists", new[] { "SubcontractorId" });
            DropIndex("dbo.ClientLists", new[] { "AdministratorId" });
            RenameColumn(table: "dbo.ClientLists", name: "SubcontractorId", newName: "Subcontractors_SubcontractorId");
            AlterColumn("dbo.ClientLists", "Subcontractors_SubcontractorId", c => c.Int());
            CreateIndex("dbo.ClientLists", "Subcontractors_SubcontractorId");
            AddForeignKey("dbo.ClientLists", "Subcontractors_SubcontractorId", "dbo.SubContractors", "SubcontractorId");
            DropColumn("dbo.ClientLists", "AdministratorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientLists", "AdministratorId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ClientLists", "Subcontractors_SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.ClientLists", new[] { "Subcontractors_SubcontractorId" });
            AlterColumn("dbo.ClientLists", "Subcontractors_SubcontractorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ClientLists", name: "Subcontractors_SubcontractorId", newName: "SubcontractorId");
            CreateIndex("dbo.ClientLists", "AdministratorId");
            CreateIndex("dbo.ClientLists", "SubcontractorId");
            AddForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
            AddForeignKey("dbo.ClientLists", "AdministratorId", "dbo.AspNetUsers", "Id");
        }
    }
}
