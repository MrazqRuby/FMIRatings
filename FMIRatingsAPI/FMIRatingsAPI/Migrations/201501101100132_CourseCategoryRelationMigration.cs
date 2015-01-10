namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseCategoryRelationMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseInCourseCategories", "CategoryId", "dbo.CourseCategories");
            DropForeignKey("dbo.CourseInCourseCategories", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Category_Id", "dbo.CourseCategories");
            DropIndex("dbo.CourseInCourseCategories", new[] { "CategoryId" });
            DropIndex("dbo.CourseInCourseCategories", new[] { "CourseId" });
            RenameColumn(table: "dbo.Courses", name: "Category_Id", newName: "CategoryId");
            RenameIndex(table: "dbo.Courses", name: "IX_Category_Id", newName: "IX_CategoryId");
            DropPrimaryKey("dbo.CourseCategories");
            AddColumn("dbo.CourseCategories", "CategoryId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CourseCategories", "CategoryId");
            AddForeignKey("dbo.Courses", "CategoryId", "dbo.CourseCategories", "CategoryId");
            DropColumn("dbo.CourseCategories", "Id");
            DropTable("dbo.CourseInCourseCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseInCourseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CourseCategories", "Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.CourseCategories");
            DropPrimaryKey("dbo.CourseCategories");
            DropColumn("dbo.CourseCategories", "CategoryId");
            AddPrimaryKey("dbo.CourseCategories", "Id");
            RenameIndex(table: "dbo.Courses", name: "IX_CategoryId", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Courses", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.CourseInCourseCategories", "CourseId");
            CreateIndex("dbo.CourseInCourseCategories", "CategoryId");
            AddForeignKey("dbo.Courses", "Category_Id", "dbo.CourseCategories", "Id");
            AddForeignKey("dbo.CourseInCourseCategories", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CourseInCourseCategories", "CategoryId", "dbo.CourseCategories", "Id", cascadeDelete: true);
        }
    }
}
