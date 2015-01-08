namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
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
                        Assesment = c.Int(nullable: false),
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
            DropIndex("dbo.VoteForTeachers", new[] { "CriterionId" });
            DropIndex("dbo.VoteForTeachers", new[] { "TeacherId" });
            DropIndex("dbo.VoteForCourses", new[] { "CriterionId" });
            DropIndex("dbo.VoteForCourses", new[] { "CourseId" });
            DropTable("dbo.VoteForTeachers");
            DropTable("dbo.VoteForCourses");
            DropTable("dbo.CriterionForTeachers");
            DropTable("dbo.CriterionForCourses");
        }
    }
}
