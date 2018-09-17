namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAdd2toPoBox : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubContractors", "PoBox", c => c.String());
            DropColumn("dbo.SubContractors", "AllocationAdjustments");
            DropColumn("dbo.SubContractors", "Address2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubContractors", "Address2", c => c.String());
            AddColumn("dbo.SubContractors", "AllocationAdjustments", c => c.Int(nullable: false));
            DropColumn("dbo.SubContractors", "PoBox");
        }
    }
}
