namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoteModels", "R", c => c.Int(nullable: false));
            AddColumn("dbo.NoteModels", "G", c => c.Int(nullable: false));
            AddColumn("dbo.NoteModels", "B", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NoteModels", "B");
            DropColumn("dbo.NoteModels", "G");
            DropColumn("dbo.NoteModels", "R");
        }
    }
}
