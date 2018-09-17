namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRegionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Regions = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SubContractors", "Address1", c => c.String());
            AddColumn("dbo.SubContractors", "Address2", c => c.String());
            AddColumn("dbo.SubContractors", "Month", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.SubContractors", "Regions_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.SubContractors", "Regions_Id");
            AddForeignKey("dbo.SubContractors", "Regions_Id", "dbo.Regions", "Id", cascadeDelete: true);
            DropColumn("dbo.SubContractors", "Region");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubContractors", "Region", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.SubContractors", "Regions_Id", "dbo.Regions");
            DropIndex("dbo.SubContractors", new[] { "Regions_Id" });
            DropColumn("dbo.SubContractors", "Regions_Id");
            DropColumn("dbo.SubContractors", "Month");
            DropColumn("dbo.SubContractors", "Address2");
            DropColumn("dbo.SubContractors", "Address1");
            DropTable("dbo.Regions");
        }
    }
}
