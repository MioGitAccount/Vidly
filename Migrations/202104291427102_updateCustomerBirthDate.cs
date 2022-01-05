namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCustomerBirthDate : DbMigration
    {
        public override void Up()
        {
            Sql("update Customers set Birthdate=''");

            AlterColumn("dbo.Customers", "Birthdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Birthdate", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
