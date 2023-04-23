namespace Chat_Server.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PassHash_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordHash", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PasswordHash");
        }
    }
}
