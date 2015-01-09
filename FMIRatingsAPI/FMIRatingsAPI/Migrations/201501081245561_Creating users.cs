namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creatingusers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Admin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CommentForTeachers", "UserId", c => c.Int(nullable: true));
            AddColumn("dbo.CommentForCourses", "UserId", c => c.Int(nullable: true));
            CreateIndex("dbo.CommentForCourses", "UserId");
            CreateIndex("dbo.CommentForTeachers", "UserId");
            AddForeignKey("dbo.CommentForCourses", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CommentForTeachers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentForTeachers", "UserId", "dbo.Users");
            DropForeignKey("dbo.CommentForCourses", "UserId", "dbo.Users");
            DropIndex("dbo.CommentForTeachers", new[] { "UserId" });
            DropIndex("dbo.CommentForCourses", new[] { "UserId" });
            DropColumn("dbo.CommentForCourses", "UserId");
            DropColumn("dbo.CommentForTeachers", "UserId");
            DropTable("dbo.Users");
        }
    }
}
