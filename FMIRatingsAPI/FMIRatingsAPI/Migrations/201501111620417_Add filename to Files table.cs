namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddfilenametoFilestable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Filename", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "Filename");
        }
    }
}
