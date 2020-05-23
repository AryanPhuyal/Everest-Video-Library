using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Everest_Video_Library.Models;
using Everest_Video_Library.Models.VideoLibrary;

namespace Everest_Video_Library.Controllers.VideoLibrary
{
    public class AlbumsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Albums
        public ActionResult Index()
        {
            var albums = db.Albums.Include(a => a.Catagory).Include(a => a.Producer).Include(a => a.Studio);
            return View(albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {

            ViewBag.CatagoryId = new SelectList(db.Catagories, "Id", "Description");
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name");
            ViewBag.StudioId = new SelectList(db.Studios, "Id", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReleaseDate,NoOfCopies,Length,NoOfStock,CoverImage,AgeContent,Price,CatagoryId,ProducerId,StudioId,Name,Description")] Album album)
        {
            Utility.SaveToFile s = new Utility.SaveToFile()
            {
                Image= album.CoverImage,
                ServerPath = Server.MapPath("~/Images"),

            };
            string fileName = s.SaveToServer();
            album.CoverImagePath = fileName;
            album.NoOfStock = album.NoOfCopies;
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                for (int i=0; i < album.NoOfCopies; i++)
                {
                    Dvd dvd = new Dvd
                    {
                        AlbumId = album.Id,
                        OnStock = true,
                    };
                    db.Dvds.Add(dvd);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatagoryId = new SelectList(db.Catagories, "Id", "Description", album.CatagoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", album.ProducerId);
            ViewBag.StudioId = new SelectList(db.Studios, "Id", "Name", album.StudioId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatagoryId = new SelectList(db.Catagories, "Id", "Description", album.CatagoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", album.ProducerId);
            ViewBag.StudioId = new SelectList(db.Studios, "Id", "Name", album.StudioId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReleaseDate,NoOfCopies,Length,NoOfStock,CoverImagePath,AgeContent,Price,CatagoryId,ProducerId,StudioId,Name")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatagoryId = new SelectList(db.Catagories, "Id", "Description", album.CatagoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", album.ProducerId);
            ViewBag.StudioId = new SelectList(db.Studios, "Id", "Name", album.StudioId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
