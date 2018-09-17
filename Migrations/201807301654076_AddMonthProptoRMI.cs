namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMonthProptoRMI : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResidentialMIRs", "Months_Id", c => c.Int());
            CreateIndex("dbo.ResidentialMIRs", "Months_Id");
            AddForeignKey("dbo.ResidentialMIRs", "Months_Id", "dbo.Months", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResidentialMIRs", "Months_Id", "dbo.Months");
            DropIndex("dbo.ResidentialMIRs", new[] { "Months_Id" });
            DropColumn("dbo.ResidentialMIRs", "Months_Id");
        }
    }
}
