namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReconcileDatabase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CommentForCourses", "AuthorId");
            DropColumn("dbo.CommentForTeachers", "AuthorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommentForTeachers", "AuthorId", c => c.Int());
            AddColumn("dbo.CommentForCourses", "AuthorId", c => c.Int());
        }
    }
}
