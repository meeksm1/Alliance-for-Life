namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRegionTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Regions (Regions) VALUES ('Region 1')");
            Sql("INSERT INTO Regions (Regions) VALUES ('Region 2')");
            Sql("INSERT INTO Regions (Regions) VALUES ('Region 3')");
            Sql("INSERT INTO Regions (Regions) VALUES ('Region 4')");
            Sql("INSERT INTO Regions (Regions) VALUES ('Region 5')");
            Sql("INSERT INTO Regions (Regions) VALUES ('Region 6')");
            Sql("INSERT INTO Regions (Regions) VALUES ('Region 7')");
            Sql("INSERT INTO Regions (Regions) VALUES ('Region 8')");
            Sql("INSERT INTO Regions (Regions) VALUES ('Region 9')");
        }

        public override void Down()
        {
            Sql("DELETE FROM Regions WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9)");
        }
    }
}
