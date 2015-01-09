namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VoteForTeachers", "Assessment", c => c.Int(nullable: false));
            DropColumn("dbo.VoteForTeachers", "Assesment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VoteForTeachers", "Assesment", c => c.Int(nullable: false));
            DropColumn("dbo.VoteForTeachers", "Assessment");
        }
    }
}
