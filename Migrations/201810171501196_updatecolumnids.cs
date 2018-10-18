namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecolumnids : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientLists", "Subcontractors_SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.ClientLists", new[] { "Subcontractors_SubcontractorId" });
            RenameColumn(table: "dbo.ClientLists", name: "Subcontractors_SubcontractorId", newName: "SubcontractorId");
            AddColumn("dbo.AspNetUsers", "SubcontractorId", c => c.Int(nullable: true));
            AlterColumn("dbo.ClientLists", "SubcontractorId", c => c.Int(nullable: true));
            CreateIndex("dbo.ClientLists", "SubcontractorId");
            AddForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors", "SubcontractorId", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "Subcontractor");
            DropColumn("dbo.ClientLists", "Subcontractor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientLists", "Subcontractor", c => c.Int(nullable: true));
            AddColumn("dbo.AspNetUsers", "Subcontractor", c => c.Int(nullable: true));
            DropForeignKey("dbo.ClientLists", "SubcontractorId", "dbo.SubContractors");
            DropIndex("dbo.ClientLists", new[] { "SubcontractorId" });
            AlterColumn("dbo.ClientLists", "SubcontractorId", c => c.Int());
            DropColumn("dbo.AspNetUsers", "SubcontractorId");
            RenameColumn(table: "dbo.ClientLists", name: "SubcontractorId", newName: "Subcontractors_SubcontractorId");
            CreateIndex("dbo.ClientLists", "Subcontractors_SubcontractorId");
            AddForeignKey("dbo.ClientLists", "Subcontractors_SubcontractorId", "dbo.SubContractors", "SubcontractorId");
        }
    }
}
