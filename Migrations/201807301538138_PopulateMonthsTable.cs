namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMonthsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Months (Months) VALUES ('January')");
            Sql("INSERT INTO Months (Months) VALUES ('February')");
            Sql("INSERT INTO Months (Months) VALUES ('March')");
            Sql("INSERT INTO Months (Months) VALUES ('April')");
            Sql("INSERT INTO Months (Months) VALUES ('May')");
            Sql("INSERT INTO Months (Months) VALUES ('June')");
            Sql("INSERT INTO Months (Months) VALUES ('July')");
            Sql("INSERT INTO Months (Months) VALUES ('August')");
            Sql("INSERT INTO Months (Months) VALUES ('September')");
            Sql("INSERT INTO Months (Months) VALUES ('October')");
            Sql("INSERT INTO Months (Months) VALUES ('November')");
            Sql("INSERT INTO Months (Months) VALUES ('December')");

        }
        
        public override void Down()
        {
            Sql("DELETE FROM Regions WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)");
        }
    }
}
