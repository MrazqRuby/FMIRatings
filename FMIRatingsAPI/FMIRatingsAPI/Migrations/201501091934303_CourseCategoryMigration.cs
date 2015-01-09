namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseCategoryMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courses", "Category_Id", c => c.Int());
            CreateIndex("dbo.Courses", "Category_Id");
            AddForeignKey("dbo.Courses", "Category_Id", "dbo.CourseCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Category_Id", "dbo.CourseCategories");
            DropIndex("dbo.Courses", new[] { "Category_Id" });
            DropColumn("dbo.Courses", "Category_Id");
            DropTable("dbo.CourseCategories");
        }
    }
}
