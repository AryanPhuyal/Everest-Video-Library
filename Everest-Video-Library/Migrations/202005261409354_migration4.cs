namespace Everest_Video_Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "AddLone_Id", "dbo.AddLones");
            DropForeignKey("dbo.Members", "AddLone_Id", "dbo.AddLones");
            DropIndex("dbo.Albums", new[] { "AddLone_Id" });
            DropIndex("dbo.Members", new[] { "AddLone_Id" });
            DropColumn("dbo.Albums", "AddLone_Id");
            DropColumn("dbo.Members", "AddLone_Id");
            DropTable("dbo.AddLones");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AddLones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Members", "AddLone_Id", c => c.Int());
            AddColumn("dbo.Albums", "AddLone_Id", c => c.Int());
            CreateIndex("dbo.Members", "AddLone_Id");
            CreateIndex("dbo.Albums", "AddLone_Id");
            AddForeignKey("dbo.Members", "AddLone_Id", "dbo.AddLones", "Id");
            AddForeignKey("dbo.Albums", "AddLone_Id", "dbo.AddLones", "Id");
        }
    }
}
