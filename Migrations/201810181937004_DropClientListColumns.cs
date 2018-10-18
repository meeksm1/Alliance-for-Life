namespace Alliance_for_Life.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropClientListColumns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientLists", "AspNetUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClientLists", new[] { "AspNetUser_Id" });

        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientLists", "AspNetUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ClientLists", "AspNetUser_Id");
            AddForeignKey("dbo.ClientLists", "AspNetUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
