namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserNavProptoClientList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientLists", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ClientLists", "User_Id");
            AddForeignKey("dbo.ClientLists", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientLists", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClientLists", new[] { "User_Id" });
            DropColumn("dbo.ClientLists", "User_Id");
        }
    }
}
