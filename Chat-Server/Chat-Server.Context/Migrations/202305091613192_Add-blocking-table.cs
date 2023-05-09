namespace Chat_Server.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addblockingtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blockings",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        BlockingUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.BlockingUserId })
                .ForeignKey("dbo.Users", t => t.BlockingUserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BlockingUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blockings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Blockings", "BlockingUserId", "dbo.Users");
            DropIndex("dbo.Blockings", new[] { "BlockingUserId" });
            DropIndex("dbo.Blockings", new[] { "UserId" });
            DropTable("dbo.Blockings");
        }
    }
}
