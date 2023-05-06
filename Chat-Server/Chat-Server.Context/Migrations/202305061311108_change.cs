namespace Chat_Server.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChannelMessages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.UserMessages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserMessages", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ChannelMessages", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
