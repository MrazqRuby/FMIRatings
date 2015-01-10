namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addgroupcolumntousertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Group", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Group");
        }
    }
}
