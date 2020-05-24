using Everest_Video_Library.Models;
using Everest_Video_Library.Models.VideoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everest_Video_Library.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [Route("/{onStock:int?}/{actor}")]
        public ActionResult Index(string onStock,string actor)
        {

            List<Album> allAlbum = new List<Album>();
            var albums = db.Albums.OrderBy(X=>X.ReleaseDate).ToList();
            var artistAlbum = db.ArtistAlbums.ToList();
            foreach (Album album in albums)
            {
                var artist = db.ArtistAlbums.Where(X => X.AlbumId == album.Id).ToList();
                album.ArtistAlbums = artist;
                allAlbum.Add(album);
            }

            if (actor != null && actor != "") 
            {
                foreach(Album album in allAlbum.ToList())
                {
                    if (album.ArtistAlbums.Count() == 0)
                    {
                        allAlbum.Remove(album);
                    }
                    foreach(ArtistAlbum a in album.ArtistAlbums.ToList())
                    {
                        if(a.Artists.LastName.ToLower() != actor.ToLower())
                        {
                            allAlbum.Remove(album);
                            break;
                        }
                    }
                }
            }
            if (onStock != null && onStock == "on")
            {
                foreach (Album album in allAlbum.ToList())
                {
                    if (album.NoOfStock <= 0)
                    {
                        allAlbum.Remove(album);
                    }
                }
            }
            return View(allAlbum);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}