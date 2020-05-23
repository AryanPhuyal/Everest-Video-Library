using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Everest_Video_Library.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.VideoLibrary.Artist> Artists { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.VideoLibrary.Album> Albums { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.VideoLibrary.Catagory> Catagories { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.VideoLibrary.Producer> Producers { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.VideoLibrary.Studio> Studios { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.VideoLibrary.ArtistAlbum> ArtistAlbums { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.Dvd> Dvds { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.VideoLibrary.Lone> Lones { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.VideoLibrary.Member> Members { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Models.VideoLibrary.MemberCatagory> MemberCatagories { get; set; }

        public System.Data.Entity.DbSet<Everest_Video_Library.Controllers.ViewModel.AddLone> AddLones { get; set; }
    }
}