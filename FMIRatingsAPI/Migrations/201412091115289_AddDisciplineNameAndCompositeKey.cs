namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisciplineNameAndCompositeKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disciplines", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Users", new[] { "Id" });
            AddPrimaryKey("dbo.Users", new[] { "Id", "Name" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Users", new[] { "Id", "Name" });
            AddPrimaryKey("dbo.Users", "Id");
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Disciplines", "Name");
        }
    }
}
