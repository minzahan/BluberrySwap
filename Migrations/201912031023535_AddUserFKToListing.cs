namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFKToListing : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Item", name: "Category1_id", newName: "Category_id");
            RenameColumn(table: "dbo.Item", name: "Unit1_id", newName: "Unit_id");
            RenameIndex(table: "dbo.Item", name: "IX_Category1_id", newName: "IX_Category_id");
            RenameIndex(table: "dbo.Item", name: "IX_Unit1_id", newName: "IX_Unit_id");
            AddColumn("dbo.Item", "author", c => c.Int(nullable: false));
            AlterColumn("dbo.Item", "Author_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Item", "Author_Id");
            AddForeignKey("dbo.Item", "Author_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Item", new[] { "Author_Id" });
            AlterColumn("dbo.Item", "Author_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Item", "author");
            RenameIndex(table: "dbo.Item", name: "IX_Unit_id", newName: "IX_Unit1_id");
            RenameIndex(table: "dbo.Item", name: "IX_Category_id", newName: "IX_Category1_id");
            RenameColumn(table: "dbo.Item", name: "Unit_id", newName: "Unit1_id");
            RenameColumn(table: "dbo.Item", name: "Category_id", newName: "Category1_id");
        }
    }
}
