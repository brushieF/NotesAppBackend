namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Really : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoteModels", "UserID_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.NoteModels", "UserID_Id");
            AddForeignKey("dbo.NoteModels", "UserID_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoteModels", "UserID_Id", "dbo.AspNetUsers");
            DropIndex("dbo.NoteModels", new[] { "UserID_Id" });
            DropColumn("dbo.NoteModels", "UserID_Id");
        }
    }
}
