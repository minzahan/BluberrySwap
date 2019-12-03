namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategoryKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "Category1_id", "dbo.Category");
            DropPrimaryKey("dbo.Category");
            AlterColumn("dbo.Category", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Category", "id");
            AddForeignKey("dbo.Item", "Category1_id", "dbo.Category", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "Category1_id", "dbo.Category");
            DropPrimaryKey("dbo.Category");
            AlterColumn("dbo.Category", "id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Category", "id");
            AddForeignKey("dbo.Item", "Category1_id", "dbo.Category", "id");
        }
    }
}
