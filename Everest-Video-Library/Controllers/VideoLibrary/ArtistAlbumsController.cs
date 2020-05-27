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
    [AuthLog(Roles = "Manager")]


    public class ArtistAlbumsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArtistAlbums
        public ActionResult Index()
        {
            var artistAlbums = db.ArtistAlbums.Include(a => a.Albums).Include(a => a.Artists);
            return View(artistAlbums.ToList());
        }

        // GET: ArtistAlbums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ArtistAlbum> artistAlbum = db.ArtistAlbums
                .Include("Artists")
                .Include("Albums")
                .Where(X=>X.ArtistId==id).ToList();
            if (artistAlbum == null)
            {
                return HttpNotFound();
            }
            return View(artistAlbum);
        }

        // GET: ArtistAlbums/Create
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "Name");
            ViewBag.ArtistId = new SelectList(db.Artists, "Id", "FirstName");
            return View();
        }

        // POST: ArtistAlbums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AlbumId,ArtistId")] ArtistAlbum artistAlbum)
        {
            if (ModelState.IsValid)
            {
                db.ArtistAlbums.Add(artistAlbum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "CoverImagePath", artistAlbum.AlbumId);
            ViewBag.ArtistId = new SelectList(db.Artists, "Id", "FirstName", artistAlbum.ArtistId);
            return View(artistAlbum);
        }

        // GET: ArtistAlbums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistAlbum artistAlbum = db.ArtistAlbums.Find(id);
            if (artistAlbum == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "CoverImagePath", artistAlbum.AlbumId);
            ViewBag.ArtistId = new SelectList(db.Artists, "Id", "FirstName", artistAlbum.ArtistId);
            return View(artistAlbum);
        }

        // POST: ArtistAlbums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AlbumId,ArtistId")] ArtistAlbum artistAlbum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistAlbum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "CoverImagePath", artistAlbum.AlbumId);
            ViewBag.ArtistId = new SelectList(db.Artists, "Id", "FirstName", artistAlbum.ArtistId);
            return View(artistAlbum);
        }

        // GET: ArtistAlbums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistAlbum artistAlbum = db.ArtistAlbums.Find(id);
            if (artistAlbum == null)
            {
                return HttpNotFound();
            }
            return View(artistAlbum);
        }

        // POST: ArtistAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistAlbum artistAlbum = db.ArtistAlbums.Find(id);
            db.ArtistAlbums.Remove(artistAlbum);
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
