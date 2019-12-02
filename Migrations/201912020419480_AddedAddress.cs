namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "street", c => c.String());
            AddColumn("dbo.AspNetUsers", "city", c => c.String());
            AddColumn("dbo.AspNetUsers", "state", c => c.String());
            AddColumn("dbo.AspNetUsers", "zipcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "zipcode");
            DropColumn("dbo.AspNetUsers", "state");
            DropColumn("dbo.AspNetUsers", "city");
            DropColumn("dbo.AspNetUsers", "street");
        }
    }
}
