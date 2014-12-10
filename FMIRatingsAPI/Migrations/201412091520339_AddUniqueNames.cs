namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Disciplines", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Teachers", "Name", c => c.String(nullable: false, maxLength: 256));
            CreateIndex("dbo.Disciplines", "Name", unique: true, name: "IX_UniqueDisciplineName");
            CreateIndex("dbo.Teachers", "Name", unique: true, name: "IX_UniqueTeacherName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teachers", "IX_UniqueTeacherName");
            DropIndex("dbo.Disciplines", "IX_UniqueDisciplineName");
            AlterColumn("dbo.Teachers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Disciplines", "Name", c => c.String(nullable: false));
        }
    }
}
