namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedResMIRviewModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ResidentialMIRs", name: "Subcontractors_ID", newName: "OrgName_ID");
            RenameIndex(table: "dbo.ResidentialMIRs", name: "IX_Subcontractors_ID", newName: "IX_OrgName_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ResidentialMIRs", name: "IX_OrgName_ID", newName: "IX_Subcontractors_ID");
            RenameColumn(table: "dbo.ResidentialMIRs", name: "OrgName_ID", newName: "Subcontractors_ID");
        }
    }
}
