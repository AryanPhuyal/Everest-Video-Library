using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Everest_Video_Library.Models;

namespace Everest_Video_Library.Controllers.VideoLibrary
{
    public class DvdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dvds
        public ActionResult Index()
        {
            var dvds = db.Dvds.Include(d => d.Album);
            return View(dvds.ToList());
        }

        // GET: Dvds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dvd dvd = db.Dvds.Find(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        // GET: Dvds/Create
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "CoverImagePath");
            return View();
        }

        // POST: Dvds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OnStock,AlbumId")] Dvd dvd)
        {
            if (ModelState.IsValid)
            {
                db.Dvds.Add(dvd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "CoverImagePath", dvd.AlbumId);
            return View(dvd);
        }

        // GET: Dvds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dvd dvd = db.Dvds.Find(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "CoverImagePath", dvd.AlbumId);
            return View(dvd);
        }

        // POST: Dvds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OnStock,AlbumId")] Dvd dvd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dvd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "CoverImagePath", dvd.AlbumId);
            return View(dvd);
        }

        // GET: Dvds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dvd dvd = db.Dvds.Find(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        // POST: Dvds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dvd dvd = db.Dvds.Find(id);
            db.Dvds.Remove(dvd);
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
