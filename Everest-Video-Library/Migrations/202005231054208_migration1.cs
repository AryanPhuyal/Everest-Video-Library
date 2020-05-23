namespace Everest_Video_Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lones", "LoneDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Lones", "ReturnDate", c => c.DateTime());
            AddColumn("dbo.Lones", "ReturnedDate", c => c.DateTime());
            AddColumn("dbo.Lones", "FineAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lones", "FineAmount");
            DropColumn("dbo.Lones", "ReturnedDate");
            DropColumn("dbo.Lones", "ReturnDate");
            DropColumn("dbo.Lones", "LoneDate");
        }
    }
}
