namespace Everest_Video_Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddLones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDate = c.DateTime(nullable: false),
                        NoOfCopies = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        NoOfStock = c.Int(nullable: false),
                        CoverImagePath = c.String(),
                        AgeContent = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CatagoryId = c.Int(nullable: false),
                        ProducerId = c.Int(nullable: false),
                        StudioId = c.Int(nullable: false),
                        Description = c.String(),
                        Name = c.String(nullable: false, maxLength: 50),
                        AddLone_Id = c.Int(),
                        AlbumOfArtist_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Catagories", t => t.CatagoryId, cascadeDelete: true)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .ForeignKey("dbo.Studios", t => t.StudioId, cascadeDelete: true)
                .ForeignKey("dbo.AddLones", t => t.AddLone_Id)
                .ForeignKey("dbo.Artists", t => t.AlbumOfArtist_Id)
                .Index(t => t.CatagoryId)
                .Index(t => t.ProducerId)
                .Index(t => t.StudioId)
                .Index(t => t.AddLone_Id)
                .Index(t => t.AlbumOfArtist_Id);
            
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 500),
                        ImageUrl = c.String(),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatagoryId = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Address = c.String(maxLength: 100),
                        Gender = c.String(nullable: false, maxLength: 10),
                        AddLone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MemberCatagories", t => t.CatagoryId, cascadeDelete: true)
                .ForeignKey("dbo.AddLones", t => t.AddLone_Id)
                .Index(t => t.CatagoryId)
                .Index(t => t.AddLone_Id);
            
            CreateTable(
                "dbo.MemberCatagories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoneDays = c.Int(nullable: false),
                        FinePerDays = c.Int(nullable: false),
                        NoOfDvdRent = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Address = c.String(maxLength: 100),
                        Gender = c.String(nullable: false, maxLength: 10),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dvds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OnStock = c.Boolean(nullable: false),
                        AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.Lones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        DvdId = c.Int(nullable: false),
                        LoneDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        ReturnedDate = c.DateTime(),
                        FineAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dvds", t => t.DvdId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.DvdId);
            
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
                        RoleMasterId = c.Int(nullable: false),
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
                .ForeignKey("dbo.RoleMasters", t => t.RoleMasterId, cascadeDelete: true)
                .Index(t => t.RoleMasterId)
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
                "dbo.RoleMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "RoleMasterId", "dbo.RoleMasters");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Lones", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Lones", "DvdId", "dbo.Dvds");
            DropForeignKey("dbo.Dvds", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.ArtistAlbums", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Albums", "AlbumOfArtist_Id", "dbo.Artists");
            DropForeignKey("dbo.ArtistAlbums", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Members", "AddLone_Id", "dbo.AddLones");
            DropForeignKey("dbo.Members", "CatagoryId", "dbo.MemberCatagories");
            DropForeignKey("dbo.Albums", "AddLone_Id", "dbo.AddLones");
            DropForeignKey("dbo.Albums", "StudioId", "dbo.Studios");
            DropForeignKey("dbo.Albums", "ProducerId", "dbo.Producers");
            DropForeignKey("dbo.Albums", "CatagoryId", "dbo.Catagories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "RoleMasterId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Lones", new[] { "DvdId" });
            DropIndex("dbo.Lones", new[] { "MemberId" });
            DropIndex("dbo.Dvds", new[] { "AlbumId" });
            DropIndex("dbo.ArtistAlbums", new[] { "ArtistId" });
            DropIndex("dbo.ArtistAlbums", new[] { "AlbumId" });
            DropIndex("dbo.Members", new[] { "AddLone_Id" });
            DropIndex("dbo.Members", new[] { "CatagoryId" });
            DropIndex("dbo.Albums", new[] { "AlbumOfArtist_Id" });
            DropIndex("dbo.Albums", new[] { "AddLone_Id" });
            DropIndex("dbo.Albums", new[] { "StudioId" });
            DropIndex("dbo.Albums", new[] { "ProducerId" });
            DropIndex("dbo.Albums", new[] { "CatagoryId" });
            DropTable("dbo.RoleMasters");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Lones");
            DropTable("dbo.Dvds");
            DropTable("dbo.Artists");
            DropTable("dbo.ArtistAlbums");
            DropTable("dbo.MemberCatagories");
            DropTable("dbo.Members");
            DropTable("dbo.Studios");
            DropTable("dbo.Producers");
            DropTable("dbo.Catagories");
            DropTable("dbo.Albums");
            DropTable("dbo.AddLones");
        }
    }
}
