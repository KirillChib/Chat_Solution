namespace Chat_Server.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedateonsmalldatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChannelMessages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AlterColumn("dbo.UserMessages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserMessages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.ChannelMessages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
