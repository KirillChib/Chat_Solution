namespace Chat_Server.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class del_PasHash : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "PasswordHash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "PasswordHash", c => c.String(nullable: false));
        }
    }
}
