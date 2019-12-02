namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 25),
                        icon = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 50),
                        price = c.Double(nullable: false),
                        unit = c.Int(nullable: false),
                        author_id = c.Int(nullable: false),
                        description = c.String(unicode: false, storeType: "text"),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        category = c.Int(nullable: false),
                        Category1_id = c.Int(),
                        Unit1_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Category", t => t.Category1_id)
                .ForeignKey("dbo.Unit", t => t.Unit1_id)
                .Index(t => t.Category1_id)
                .Index(t => t.Unit1_id);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Offer_Cash",
                c => new
                    {
                        id = c.Int(nullable: false),
                        buyer_id = c.Int(nullable: false),
                        seller_id = c.Int(nullable: false),
                        seller_item_name = c.String(nullable: false, maxLength: 50),
                        seller_item_id = c.Int(nullable: false),
                        seller_item_qty = c.String(nullable: false, maxLength: 10),
                        seller_item_unit = c.Int(nullable: false),
                        buyer_cash_value = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        Unit_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Unit", t => t.Unit_id)
                .Index(t => t.Unit_id);
            
            CreateTable(
                "dbo.Offer_Exchange",
                c => new
                    {
                        id = c.Int(nullable: false),
                        buyer_id = c.Int(nullable: false),
                        seller_id = c.Int(nullable: false),
                        buyer_item_name = c.String(nullable: false, maxLength: 50),
                        buyer_item_id = c.Int(nullable: false),
                        buyer_item_qty = c.String(nullable: false, maxLength: 10),
                        buyer_item_unit = c.Int(nullable: false),
                        seller_item_name = c.String(nullable: false, maxLength: 50),
                        seller_item_id = c.Int(nullable: false),
                        seller_item_qty = c.String(nullable: false, maxLength: 10),
                        seller_item_unit = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        Unit_id = c.Int(),
                        Unit1_id = c.Int(),
                        Unit_id1 = c.Int(),
                        Unit_id2 = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Unit", t => t.Unit_id)
                .ForeignKey("dbo.Unit", t => t.Unit1_id)
                .ForeignKey("dbo.Unit", t => t.Unit_id1)
                .ForeignKey("dbo.Unit", t => t.Unit_id2)
                .Index(t => t.Unit_id)
                .Index(t => t.Unit1_id)
                .Index(t => t.Unit_id1)
                .Index(t => t.Unit_id2);
            
            CreateTable(
                "dbo.Transaction_Cash",
                c => new
                    {
                        id = c.Int(nullable: false),
                        buyer_id = c.Int(nullable: false),
                        seller_id = c.Int(nullable: false),
                        seller_item_name = c.String(nullable: false, maxLength: 50),
                        seller_item_id1 = c.Int(nullable: false),
                        seller_item_qty = c.String(nullable: false, maxLength: 10),
                        seller_item_unit = c.Int(nullable: false),
                        buyer_cash_value = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        Unit_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Unit", t => t.Unit_id)
                .Index(t => t.Unit_id);
            
            CreateTable(
                "dbo.Transaction_Exchange",
                c => new
                    {
                        id = c.Int(nullable: false),
                        buyer_id = c.Int(nullable: false),
                        seller_id = c.Int(nullable: false),
                        buyer_item_name = c.String(nullable: false, maxLength: 50),
                        buyer_item_id = c.Int(nullable: false),
                        buyer_item_qty = c.String(nullable: false, maxLength: 10),
                        buyer_item_unit = c.Int(nullable: false),
                        seller_item_name = c.String(nullable: false, maxLength: 50),
                        seller_item_id = c.Int(nullable: false),
                        seller_item_qty = c.String(nullable: false, maxLength: 10),
                        seller_item_unit = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        Unit_id = c.Int(),
                        Unit1_id = c.Int(),
                        Unit_id1 = c.Int(),
                        Unit_id2 = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Unit", t => t.Unit_id)
                .ForeignKey("dbo.Unit", t => t.Unit1_id)
                .ForeignKey("dbo.Unit", t => t.Unit_id1)
                .ForeignKey("dbo.Unit", t => t.Unit_id2)
                .Index(t => t.Unit_id)
                .Index(t => t.Unit1_id)
                .Index(t => t.Unit_id1)
                .Index(t => t.Unit_id2);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Transaction_Exchange", "Unit_id2", "dbo.Unit");
            DropForeignKey("dbo.Transaction_Exchange", "Unit_id1", "dbo.Unit");
            DropForeignKey("dbo.Transaction_Exchange", "Unit1_id", "dbo.Unit");
            DropForeignKey("dbo.Transaction_Exchange", "Unit_id", "dbo.Unit");
            DropForeignKey("dbo.Transaction_Cash", "Unit_id", "dbo.Unit");
            DropForeignKey("dbo.Offer_Exchange", "Unit_id2", "dbo.Unit");
            DropForeignKey("dbo.Offer_Exchange", "Unit_id1", "dbo.Unit");
            DropForeignKey("dbo.Offer_Exchange", "Unit1_id", "dbo.Unit");
            DropForeignKey("dbo.Offer_Exchange", "Unit_id", "dbo.Unit");
            DropForeignKey("dbo.Offer_Cash", "Unit_id", "dbo.Unit");
            DropForeignKey("dbo.Item", "Unit1_id", "dbo.Unit");
            DropForeignKey("dbo.Item", "Category1_id", "dbo.Category");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Transaction_Exchange", new[] { "Unit_id2" });
            DropIndex("dbo.Transaction_Exchange", new[] { "Unit_id1" });
            DropIndex("dbo.Transaction_Exchange", new[] { "Unit1_id" });
            DropIndex("dbo.Transaction_Exchange", new[] { "Unit_id" });
            DropIndex("dbo.Transaction_Cash", new[] { "Unit_id" });
            DropIndex("dbo.Offer_Exchange", new[] { "Unit_id2" });
            DropIndex("dbo.Offer_Exchange", new[] { "Unit_id1" });
            DropIndex("dbo.Offer_Exchange", new[] { "Unit1_id" });
            DropIndex("dbo.Offer_Exchange", new[] { "Unit_id" });
            DropIndex("dbo.Offer_Cash", new[] { "Unit_id" });
            DropIndex("dbo.Item", new[] { "Unit1_id" });
            DropIndex("dbo.Item", new[] { "Category1_id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Transaction_Exchange");
            DropTable("dbo.Transaction_Cash");
            DropTable("dbo.Offer_Exchange");
            DropTable("dbo.Offer_Cash");
            DropTable("dbo.Unit");
            DropTable("dbo.Item");
            DropTable("dbo.Category");
        }
    }
}
