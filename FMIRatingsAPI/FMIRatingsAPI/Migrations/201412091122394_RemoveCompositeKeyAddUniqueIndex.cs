namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCompositeKeyAddUniqueIndex : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.Users", "Name", unique: true, name: "IX_UniqueUserName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DisciplineTeachers", new[] { "Teacher_Id" });
            DropIndex("dbo.DisciplineTeachers", new[] { "Discipline_Id" });
            DropIndex("dbo.Users", "IX_UniqueUserName");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Users", new[] { "Id", "Name" });
        }
    }
}
