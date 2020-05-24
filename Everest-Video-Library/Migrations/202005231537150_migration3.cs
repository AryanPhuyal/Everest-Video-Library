namespace Everest_Video_Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artists", "AlbumList_Id", "dbo.Albums");
            DropIndex("dbo.Artists", new[] { "AlbumList_Id" });
            DropColumn("dbo.Albums", "Discriminator");
            DropColumn("dbo.Artists", "AlbumList_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "AlbumList_Id", c => c.Int());
            AddColumn("dbo.Albums", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Artists", "AlbumList_Id");
            AddForeignKey("dbo.Artists", "AlbumList_Id", "dbo.Albums", "Id");
        }
    }
}
