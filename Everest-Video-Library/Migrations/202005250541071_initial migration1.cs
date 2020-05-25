namespace Everest_Video_Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "RoleMasterId", "dbo.RoleMasters");
            DropIndex("dbo.AspNetUsers", new[] { "RoleMasterId" });
            DropColumn("dbo.AspNetUsers", "RoleMasterId");
            DropTable("dbo.RoleMasters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "RoleMasterId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "RoleMasterId");
            AddForeignKey("dbo.AspNetUsers", "RoleMasterId", "dbo.RoleMasters", "Id", cascadeDelete: true);
        }
    }
}
