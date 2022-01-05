namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class justAPrankBroDropColumnPhoneNumber : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Phone_Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Phone_Number", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
