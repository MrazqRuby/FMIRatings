namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Category_Id", "dbo.CourseCategories");
            DropIndex("dbo.Courses", new[] { "Category_Id" });
			//AddColumn("dbo.VoteForTeachers", "Assessment", c => c.Int(nullable: false));
            DropColumn("dbo.Courses", "Category_Id");
			//DropColumn("dbo.VoteForTeachers", "Assesment");
            DropTable("dbo.CourseCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
			//AddColumn("dbo.VoteForTeachers", "Assesment", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "Category_Id", c => c.Int());
			//DropColumn("dbo.VoteForTeachers", "Assessment");
            CreateIndex("dbo.Courses", "Category_Id");
            AddForeignKey("dbo.Courses", "Category_Id", "dbo.CourseCategories", "Id");
        }
    }
}
