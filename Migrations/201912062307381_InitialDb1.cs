namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Item", "Author_Id", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Item", "Author_Id", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
