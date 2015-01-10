namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateusertablewithadditionalcolumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String(maxLength: 450));
            AddColumn("dbo.Users", "Course", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Major", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String(maxLength: 450));
            CreateIndex("dbo.Users", "Name", unique: true);
            CreateIndex("dbo.Users", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "Name" });
            AlterColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Users", "Major");
            DropColumn("dbo.Users", "Course");
            DropColumn("dbo.Users", "Email");
        }
    }
}
