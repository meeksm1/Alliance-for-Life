namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedNonResProp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResidentialMIRs", "NonResidentialId", "dbo.NonResidentialMIRs");
            DropIndex("dbo.ResidentialMIRs", new[] { "NonResidentialId" });
            RenameColumn(table: "dbo.ResidentialMIRs", name: "NonResidentialId", newName: "NonResidential_Id");
            AlterColumn("dbo.ResidentialMIRs", "NonResidential_Id", c => c.Guid());
            CreateIndex("dbo.ResidentialMIRs", "NonResidential_Id");
            AddForeignKey("dbo.ResidentialMIRs", "NonResidential_Id", "dbo.NonResidentialMIRs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResidentialMIRs", "NonResidential_Id", "dbo.NonResidentialMIRs");
            DropIndex("dbo.ResidentialMIRs", new[] { "NonResidential_Id" });
            AlterColumn("dbo.ResidentialMIRs", "NonResidential_Id", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.ResidentialMIRs", name: "NonResidential_Id", newName: "NonResidentialId");
            CreateIndex("dbo.ResidentialMIRs", "NonResidentialId");
            AddForeignKey("dbo.ResidentialMIRs", "NonResidentialId", "dbo.NonResidentialMIRs", "Id", cascadeDelete: true);
        }
    }
}
