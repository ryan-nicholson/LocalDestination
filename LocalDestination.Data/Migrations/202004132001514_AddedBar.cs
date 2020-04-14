namespace LocalDestination.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bar",
                c => new
                    {
                        BarId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ServesFood = c.Boolean(nullable: false),
                        Comment = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                        DestinationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BarId)
                .ForeignKey("dbo.Destination", t => t.DestinationId, cascadeDelete: false)
                .Index(t => t.DestinationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bar", "DestinationId", "dbo.Destination");
            DropIndex("dbo.Bar", new[] { "DestinationId" });
            DropTable("dbo.Bar");
        }
    }
}
