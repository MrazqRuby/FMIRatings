namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addrealnametousertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RealName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RealName");
        }
    }
}
