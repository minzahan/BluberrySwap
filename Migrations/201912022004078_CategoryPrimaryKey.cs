namespace BlueberrySwap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryPrimaryKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "author_UserLockoutEnabledByDefault", c => c.Boolean(nullable: false));
            AddColumn("dbo.Item", "author_MaxFailedAccessAttemptsBeforeLockout", c => c.Int(nullable: false));
            AddColumn("dbo.Item", "author_DefaultAccountLockoutTimeSpan", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "author_DefaultAccountLockoutTimeSpan");
            DropColumn("dbo.Item", "author_MaxFailedAccessAttemptsBeforeLockout");
            DropColumn("dbo.Item", "author_UserLockoutEnabledByDefault");
        }
    }
}
