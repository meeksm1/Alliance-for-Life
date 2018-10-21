namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropertiestoCL : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientLists", "Subcontractors_SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.ClientLists", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClientLists", new[] { "Subcontractors_SubcontractorId" });
            DropIndex("dbo.ClientLists", new[] { "User_Id" });
            RenameColumn(table: "dbo.ClientLists", name: "Subcontractors_SubcontractorId", newName: "SubcontractorId");
            RenameColumn(table: "dbo.ClientLists", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.ClientLists", "SubcontractorId", c => c.Int(nullable: true));
            AlterColumn("dbo.ClientLists", "UserId", c => c.String(nullable: true, maxLength: 128));
            CreateIndex("dbo.ClientLists", "SubcontractorId");
            CreateIndex("dbo.ClientLists", "UserId");
            AddForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: false);
            AddForeignKey("dbo.ClientLists", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientLists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.ClientLists", new[] { "UserId" });
            DropIndex("dbo.ClientLists", new[] { "SubcontractorId" });
            AlterColumn("dbo.ClientLists", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.ClientLists", "SubcontractorId", c => c.Int());
            RenameColumn(table: "dbo.ClientLists", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.ClientLists", name: "SubcontractorId", newName: "Subcontractors_SubcontractorId");
            CreateIndex("dbo.ClientLists", "User_Id");
            CreateIndex("dbo.ClientLists", "Subcontractors_SubcontractorId");
            AddForeignKey("dbo.ClientLists", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ClientLists", "Subcontractors_SubcontractorId", "dbo.SubContractors", "SubcontractorId");
        }
    }
}
