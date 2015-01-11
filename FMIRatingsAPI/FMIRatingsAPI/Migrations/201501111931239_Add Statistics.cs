namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatistics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        FileName = c.String(),
                        InputType = c.String(),
                        ReturnType = c.Int(nullable: false),
                        InputTransform = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Statistics");
        }
    }
}
