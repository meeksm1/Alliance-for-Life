namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMonthCol : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ClientLists", name: "Contractor_Id", newName: "ContractorID");
            RenameColumn(table: "dbo.SubContractors", name: "Administrator_Id", newName: "AdministratorId");
            RenameColumn(table: "dbo.SubContractors", name: "Regions_Id", newName: "RegionId");
            RenameIndex(table: "dbo.ClientLists", name: "IX_Contractor_Id", newName: "IX_ContractorID");
            RenameIndex(table: "dbo.SubContractors", name: "IX_Administrator_Id", newName: "IX_AdministratorId");
            RenameIndex(table: "dbo.SubContractors", name: "IX_Regions_Id", newName: "IX_RegionId");
            DropColumn("dbo.SubContractors", "Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubContractors", "Month", c => c.String(nullable: false, maxLength: 255));
            RenameIndex(table: "dbo.SubContractors", name: "IX_RegionId", newName: "IX_Regions_Id");
            RenameIndex(table: "dbo.SubContractors", name: "IX_AdministratorId", newName: "IX_Administrator_Id");
            RenameIndex(table: "dbo.ClientLists", name: "IX_ContractorID", newName: "IX_Contractor_Id");
            RenameColumn(table: "dbo.SubContractors", name: "RegionId", newName: "Regions_Id");
            RenameColumn(table: "dbo.SubContractors", name: "AdministratorId", newName: "Administrator_Id");
            RenameColumn(table: "dbo.ClientLists", name: "ContractorID", newName: "Contractor_Id");
        }
    }
}
