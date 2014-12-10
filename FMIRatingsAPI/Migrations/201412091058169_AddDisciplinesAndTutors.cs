namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisciplinesAndTutors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DisciplineTeachers",
                c => new
                    {
                        Discipline_Id = c.Int(nullable: false),
                        Teacher_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Discipline_Id, t.Teacher_Id })
                .ForeignKey("dbo.Disciplines", t => t.Discipline_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .Index(t => t.Discipline_Id)
                .Index(t => t.Teacher_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DisciplineTeachers", new[] { "Teacher_Id" });
            DropIndex("dbo.DisciplineTeachers", new[] { "Discipline_Id" });
            DropForeignKey("dbo.DisciplineTeachers", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.DisciplineTeachers", "Discipline_Id", "dbo.Disciplines");
            DropTable("dbo.DisciplineTeachers");
            DropTable("dbo.Teachers");
            DropTable("dbo.Disciplines");
        }
    }
}
