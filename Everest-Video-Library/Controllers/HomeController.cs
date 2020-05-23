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
            IQueryable<Album> albums;
            if (onStock=="on" && actor==null)
            {
                albums = (from a in db.Albums
                          join aa in db.ArtistAlbums on a.Id equals aa.AlbumId
                          join ar in db.Artists on aa.ArtistId equals ar.Id
                          where a.NoOfStock >0
                          select new Album {
                          a
                          }
                          );
                return View(albums.ToList());

            }
            else if (onStock == "on"&&actor!=null )
            {
                albums = (from a in db.Albums
                          join aa in db.ArtistAlbums on a.Id equals aa.AlbumId
                          join ar in db.Artists on aa.ArtistId equals ar.Id
                          where a.NoOfStock > 0 && ar.LastName ==actor 
                          select new Album
                          {
                              Id = a.Id,
                              Name = a.Name,
                              Length = a.Length,
                              NoOfCopies = a.NoOfCopies,
                              NoOfStock = a.NoOfStock,
                              Price = a.Price,
                              Producer = a.Producer,
                              ReleaseDate = a.ReleaseDate,
                          });
            return View(albums.ToList());

            }
            else if (onStock == null && actor != null)
            {
                albums = (from a in db.Albums
                          join aa in db.ArtistAlbums on a.Id equals aa.AlbumId
                          join ar in db.Artists on aa.ArtistId equals ar.Id
                          where ar.LastName == actor
                          select new Album
                          {
                              Id = a.Id,
                              Name = a.Name,
                              Length = a.Length,
                              NoOfCopies = a.NoOfCopies,
                              NoOfStock = a.NoOfStock,
                              Price = a.Price,
                              Producer = a.Producer,
                              ReleaseDate = a.ReleaseDate,
                          });
                return View(albums.ToList());

            }

            else
            {
                albums = db.Albums;
                return View(albums.ToList());
            }

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