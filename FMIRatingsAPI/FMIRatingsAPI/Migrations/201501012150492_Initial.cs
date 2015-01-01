namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentForTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        AuthorId = c.Int(),
                        Text = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherInCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherInCourses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.CommentForTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherInCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TeacherInCourses", new[] { "CourseId" });
            DropIndex("dbo.TeacherInCourses", new[] { "TeacherId" });
            DropIndex("dbo.CommentForTeachers", new[] { "TeacherId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.TeacherInCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.CommentForTeachers");
        }
    }
}
