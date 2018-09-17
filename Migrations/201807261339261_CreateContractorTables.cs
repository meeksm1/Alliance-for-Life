namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateContractorTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        DOB = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Contractor_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Contractor_Id, cascadeDelete: true)
                .Index(t => t.Contractor_Id);
            
            CreateTable(
                "dbo.SubContractors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrgName = c.String(nullable: false, maxLength: 255),
                        City = c.String(nullable: false, maxLength: 255),
                        County = c.String(nullable: false, maxLength: 255),
                        Region = c.String(nullable: false, maxLength: 255),
                        EIN = c.Int(nullable: false),
                        State = c.String(nullable: false, maxLength: 25),
                        ZipCode = c.Int(nullable: false),
                        AllocatedContractAmount = c.Int(nullable: false),
                        AllocationAdjustments = c.Int(nullable: false),
                        Administrator_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Administrator_Id, cascadeDelete: true)
                .Index(t => t.Administrator_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubContractors", "Administrator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClientLists", "Contractor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SubContractors", new[] { "Administrator_Id" });
            DropIndex("dbo.ClientLists", new[] { "Contractor_Id" });
            DropTable("dbo.SubContractors");
            DropTable("dbo.ClientLists");
        }
    }
}
