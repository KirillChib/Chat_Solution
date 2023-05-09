namespace Chat_Server.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetypedate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChannelMessages", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserMessages", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserMessages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.ChannelMessages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
