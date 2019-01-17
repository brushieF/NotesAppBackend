namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noteChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoteModels", "Content", c => c.String());
            DropColumn("dbo.NoteModels", "Contenet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NoteModels", "Contenet", c => c.String());
            DropColumn("dbo.NoteModels", "Content");
        }
    }
}
