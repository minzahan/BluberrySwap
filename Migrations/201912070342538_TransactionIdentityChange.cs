namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionIdentityChange : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Transaction");
            AlterColumn("dbo.Transaction", "transaction_id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Transaction", "transaction_id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Transaction");
            AlterColumn("dbo.Transaction", "transaction_id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Transaction", "transaction_id");
        }
    }
}
