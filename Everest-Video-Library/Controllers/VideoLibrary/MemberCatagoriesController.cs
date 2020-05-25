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
    [Authorize]

    public class MemberCatagoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MemberCatagories
        public ActionResult Index()
        {
            return View(db.MemberCatagories.ToList());
        }

        // GET: MemberCatagories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCatagory memberCatagory = db.MemberCatagories.Find(id);
            if (memberCatagory == null)
            {
                return HttpNotFound();
            }
            return View(memberCatagory);
        }

        // GET: MemberCatagories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberCatagories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LoneDays,FinePerDays,NoOfDvdRent,Name")] MemberCatagory memberCatagory)
        {
            if (ModelState.IsValid)
            {
                db.MemberCatagories.Add(memberCatagory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberCatagory);
        }

        // GET: MemberCatagories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCatagory memberCatagory = db.MemberCatagories.Find(id);
            if (memberCatagory == null)
            {
                return HttpNotFound();
            }
            return View(memberCatagory);
        }

        // POST: MemberCatagories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LoneDays,FinePerDays,NoOfDvdRent,Name")] MemberCatagory memberCatagory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberCatagory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberCatagory);
        }

        // GET: MemberCatagories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCatagory memberCatagory = db.MemberCatagories.Find(id);
            if (memberCatagory == null)
            {
                return HttpNotFound();
            }
            return View(memberCatagory);
        }

        // POST: MemberCatagories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberCatagory memberCatagory = db.MemberCatagories.Find(id);
            db.MemberCatagories.Remove(memberCatagory);
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
