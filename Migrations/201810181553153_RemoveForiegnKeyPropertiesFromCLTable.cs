namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForiegnKeyPropertiesFromCLTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors");
            DropForeignKey("dbo.ClientLists", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ClientLists", new[] { "SubcontractorId" });
            DropIndex("dbo.ClientLists", new[] { "UserId" });
            RenameColumn(table: "dbo.ClientLists", name: "SubcontractorId", newName: "Subcontractors_SubcontractorId");
            RenameColumn(table: "dbo.ClientLists", name: "UserId", newName: "User_Id");
            AlterColumn("dbo.ClientLists", "Subcontractors_SubcontractorId", c => c.Int());
            AlterColumn("dbo.ClientLists", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ClientLists", "Subcontractors_SubcontractorId");
            CreateIndex("dbo.ClientLists", "User_Id");
            AddForeignKey("dbo.ClientLists", "Subcontractors_SubcontractorId", "dbo.SubContractors", "SubcontractorId");
            AddForeignKey("dbo.ClientLists", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientLists", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClientLists", "Subcontractors_SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.ClientLists", new[] { "User_Id" });
            DropIndex("dbo.ClientLists", new[] { "Subcontractors_SubcontractorId" });
            AlterColumn("dbo.ClientLists", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ClientLists", "Subcontractors_SubcontractorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ClientLists", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.ClientLists", name: "Subcontractors_SubcontractorId", newName: "SubcontractorId");
            CreateIndex("dbo.ClientLists", "UserId");
            CreateIndex("dbo.ClientLists", "SubcontractorId");
            AddForeignKey("dbo.ClientLists", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
        }
    }
}
