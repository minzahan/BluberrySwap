namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "Category_id", "dbo.Category");
            DropForeignKey("dbo.Item", "Unit_id", "dbo.Unit");
            DropIndex("dbo.Item", new[] { "Category_id" });
            DropIndex("dbo.Item", new[] { "Unit_id" });
            RenameColumn(table: "dbo.Item", name: "Category_id", newName: "CategoryID");
            RenameColumn(table: "dbo.Item", name: "Unit_id", newName: "UnitID");
            AddColumn("dbo.Item", "AuthorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Item", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.Item", "UnitID", c => c.Int(nullable: false));
            CreateIndex("dbo.Item", "CategoryID");
            CreateIndex("dbo.Item", "UnitID");
            AddForeignKey("dbo.Item", "CategoryID", "dbo.Category", "id", cascadeDelete: true);
            AddForeignKey("dbo.Item", "UnitID", "dbo.Unit", "id", cascadeDelete: true);
            DropColumn("dbo.Item", "unit");
            DropColumn("dbo.Item", "author");
            DropColumn("dbo.Item", "category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "category", c => c.Int(nullable: false));
            AddColumn("dbo.Item", "author", c => c.Int(nullable: false));
            AddColumn("dbo.Item", "unit", c => c.Int(nullable: false));
            DropForeignKey("dbo.Item", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.Item", "CategoryID", "dbo.Category");
            DropIndex("dbo.Item", new[] { "UnitID" });
            DropIndex("dbo.Item", new[] { "CategoryID" });
            AlterColumn("dbo.Item", "UnitID", c => c.Int());
            AlterColumn("dbo.Item", "CategoryID", c => c.Int());
            DropColumn("dbo.Item", "AuthorID");
            RenameColumn(table: "dbo.Item", name: "UnitID", newName: "Unit_id");
            RenameColumn(table: "dbo.Item", name: "CategoryID", newName: "Category_id");
            CreateIndex("dbo.Item", "Unit_id");
            CreateIndex("dbo.Item", "Category_id");
            AddForeignKey("dbo.Item", "Unit_id", "dbo.Unit", "id");
            AddForeignKey("dbo.Item", "Category_id", "dbo.Category", "id");
        }
    }
}
