namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUpdatedAtToDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Item", "updated_at", c => c.DateTime(nullable: false));
            DropColumn("dbo.Item", "AuthorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "AuthorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Item", "updated_at", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"));
        }
    }
}
