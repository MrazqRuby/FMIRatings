namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherDepartmentMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Teachers", "Department_Id", c => c.Int());
            CreateIndex("dbo.Teachers", "Department_Id");
            AddForeignKey("dbo.Teachers", "Department_Id", "dbo.TeacherDepartments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Department_Id", "dbo.TeacherDepartments");
            DropIndex("dbo.Teachers", new[] { "Department_Id" });
            DropColumn("dbo.Teachers", "Department_Id");
            DropTable("dbo.TeacherDepartments");
        }
    }
}
