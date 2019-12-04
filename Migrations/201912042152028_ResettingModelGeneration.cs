namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResettingModelGeneration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 25),
                        icon = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        price = c.Double(nullable: false),
                        Description = c.String(unicode: false, storeType: "text"),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        UnitID = c.Int(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                        Author_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id1)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Unit", t => t.UnitID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.UnitID)
                .Index(t => t.Author_Id1);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.String(),
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                        offer_cash_id = c.Int(nullable: false),
                        cash_value = c.Double(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        Unit_id = c.Int(),
                        offer_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.offer_cash_id)
                .ForeignKey("dbo.Offer", t => t.offer_cash_id)
                .Index(t => t.offer_cash_id);
            
            CreateTable(
                "dbo.Offer",
                c => new
                    {
                        offer_id = c.Int(nullable: false, identity: true),
                        item_id = c.Int(nullable: false),
                        offeredby_author_id = c.String(nullable: false, maxLength: 128),
                        qty = c.Double(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.offer_id);
            
            CreateTable(
                "dbo.Offer_Exchange",
                c => new
                    {
                        offer_exchange_id = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        exchange_item_id = c.Int(nullable: false),
                        exchange_item_qty = c.Double(nullable: false),
                        exchange_item_unit_id = c.Int(nullable: false),
                        offer_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.offer_exchange_id)
                .ForeignKey("dbo.Offer", t => t.offer_exchange_id)
                .Index(t => t.offer_exchange_id);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        transaction_id = c.Int(nullable: false),
                        offer_id = c.Int(nullable: false),
                        accepted = c.Boolean(nullable: false),
                        rejection_reason = c.String(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.transaction_id)
                .ForeignKey("dbo.Offer", t => t.offer_id, cascadeDelete: true)
                .Index(t => t.offer_id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Offer_Cash", "offer_cash_id", "dbo.Offer");
            DropForeignKey("dbo.Transaction", "offer_id", "dbo.Offer");
            DropForeignKey("dbo.Offer_Exchange", "offer_exchange_id", "dbo.Offer");
            DropForeignKey("dbo.Item", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.Item", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Item", "Author_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Transaction", new[] { "offer_id" });
            DropIndex("dbo.Offer_Exchange", new[] { "offer_exchange_id" });
            DropIndex("dbo.Offer_Cash", new[] { "offer_cash_id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Item", new[] { "Author_Id1" });
            DropIndex("dbo.Item", new[] { "UnitID" });
            DropIndex("dbo.Item", new[] { "CategoryID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Transaction");
            DropTable("dbo.Offer_Exchange");
            DropTable("dbo.Offer");
            DropTable("dbo.Offer_Cash");
            DropTable("dbo.Unit");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Item");
            DropTable("dbo.Category");
        }
    }
}
