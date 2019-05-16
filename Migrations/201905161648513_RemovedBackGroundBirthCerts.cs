namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBackGroundBirthCerts : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ParticipationServices", "BackgroudCheck");
            DropColumn("dbo.ParticipationServices", "PBirthCerts");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParticipationServices", "PBirthCerts", c => c.Double(nullable: false));
            AddColumn("dbo.ParticipationServices", "BackgroudCheck", c => c.Double(nullable: false));
        }
    }
}
