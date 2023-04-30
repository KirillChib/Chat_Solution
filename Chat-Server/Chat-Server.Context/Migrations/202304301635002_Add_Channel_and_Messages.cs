namespace Chat_Server.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Channel_and_Messages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChannelMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserFromId = c.Int(nullable: false),
                        ChannelId = c.Int(nullable: false),
                        Message = c.String(),
                        HasFile = c.Boolean(nullable: false),
                        FilePath = c.String(),
                        CreatedAt = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Channels", t => t.ChannelId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserFromId, cascadeDelete: true)
                .Index(t => t.UserFromId)
                .Index(t => t.ChannelId);
            
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChannelUsers",
                c => new
                    {
                        ChannelId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChannelId, t.UserId })
                .ForeignKey("dbo.Channels", t => t.ChannelId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ChannelId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserFromId = c.Int(nullable: false),
                        UserToId = c.Int(nullable: false),
                        Message = c.String(),
                        HasFile = c.Boolean(nullable: false),
                        FilePath = c.String(),
                        CreatedAt = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserFromId)
                .ForeignKey("dbo.Users", t => t.UserToId)
                .Index(t => t.UserFromId)
                .Index(t => t.UserToId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChannelMessages", "UserFromId", "dbo.Users");
            DropForeignKey("dbo.ChannelMessages", "ChannelId", "dbo.Channels");
            DropForeignKey("dbo.ChannelUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserMessages", "UserToId", "dbo.Users");
            DropForeignKey("dbo.UserMessages", "UserFromId", "dbo.Users");
            DropForeignKey("dbo.ChannelUsers", "ChannelId", "dbo.Channels");
            DropIndex("dbo.UserMessages", new[] { "UserToId" });
            DropIndex("dbo.UserMessages", new[] { "UserFromId" });
            DropIndex("dbo.ChannelUsers", new[] { "UserId" });
            DropIndex("dbo.ChannelUsers", new[] { "ChannelId" });
            DropIndex("dbo.ChannelMessages", new[] { "ChannelId" });
            DropIndex("dbo.ChannelMessages", new[] { "UserFromId" });
            DropTable("dbo.UserMessages");
            DropTable("dbo.ChannelUsers");
            DropTable("dbo.Channels");
            DropTable("dbo.ChannelMessages");
        }
    }
}
