namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAddIdentity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Item");
            AlterColumn("dbo.Item", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Item", "id");
            DropColumn("dbo.Item", "author_UserLockoutEnabledByDefault");
            DropColumn("dbo.Item", "author_MaxFailedAccessAttemptsBeforeLockout");
            DropColumn("dbo.Item", "author_DefaultAccountLockoutTimeSpan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "author_DefaultAccountLockoutTimeSpan", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Item", "author_MaxFailedAccessAttemptsBeforeLockout", c => c.Int(nullable: false));
            AddColumn("dbo.Item", "author_UserLockoutEnabledByDefault", c => c.Boolean(nullable: false));
            DropPrimaryKey("dbo.Item");
            AlterColumn("dbo.Item", "id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Item", "id");
        }
    }
}
