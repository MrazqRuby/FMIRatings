namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentForCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Text = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.CommentForTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Text = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.CriterionForCourses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CriterionForTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VoteForCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        CriterionId = c.Int(nullable: false),
                        Assessment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.CriterionForCourses", t => t.CriterionId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.CriterionId);
            
            CreateTable(
                "dbo.VoteForTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        CriterionId = c.Int(nullable: false),
                        Assesment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CriterionForTeachers", t => t.CriterionId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.CriterionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoteForTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.VoteForTeachers", "CriterionId", "dbo.CriterionForTeachers");
            DropForeignKey("dbo.VoteForCourses", "CriterionId", "dbo.CriterionForCourses");
            DropForeignKey("dbo.VoteForCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TeacherInCourses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.CommentForTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherInCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CommentForCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CommentForTeachers", "UserId", "dbo.Users");
            DropForeignKey("dbo.CommentForCourses", "UserId", "dbo.Users");
            DropIndex("dbo.VoteForTeachers", new[] { "CriterionId" });
            DropIndex("dbo.VoteForTeachers", new[] { "TeacherId" });
            DropIndex("dbo.VoteForCourses", new[] { "CriterionId" });
            DropIndex("dbo.VoteForCourses", new[] { "CourseId" });
            DropIndex("dbo.TeacherInCourses", new[] { "CourseId" });
            DropIndex("dbo.TeacherInCourses", new[] { "TeacherId" });
            DropIndex("dbo.CommentForTeachers", new[] { "UserId" });
            DropIndex("dbo.CommentForTeachers", new[] { "TeacherId" });
            DropIndex("dbo.CommentForCourses", new[] { "UserId" });
            DropIndex("dbo.CommentForCourses", new[] { "CourseId" });
            DropTable("dbo.VoteForTeachers");
            DropTable("dbo.VoteForCourses");
            DropTable("dbo.CriterionForTeachers");
            DropTable("dbo.CriterionForCourses");
            DropTable("dbo.Teachers");
            DropTable("dbo.TeacherInCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.CommentForTeachers");
            DropTable("dbo.Users");
            DropTable("dbo.CommentForCourses");
        }
    }
}
