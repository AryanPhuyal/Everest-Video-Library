using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Everest_Video_Library.Controllers.ViewModel;
using Everest_Video_Library.Models;
using Everest_Video_Library.Models.VideoLibrary;

namespace Everest_Video_Library.Controllers.VideoLibrary
{
    [AuthLog(Roles = "Manager")]
    public class LonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lones
        public ActionResult Index()
        {
            var lones = db.Lones.Include(l => l.Dvds).Include(l => l.Members);
            return View(lones.ToList());
        }

        // GET: Lones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lone lone = db.Lones.Find(id);
            if (lone == null)
            {
                return HttpNotFound();
            }
            return View(lone);
        }

        // GET: Lones/Create
        public ActionResult Create()
        {
            ViewBag.Error = Request.Cookies["Error"] != null ? Request.Cookies["Error"].Value : null;
            ViewBag.Success = Request.Cookies["Success"] != null ? Request.Cookies["Success"].Value : null;
            HttpCookie cokie = new HttpCookie("Error");
            cokie["Error"] = null;
            cokie["Success"] = null;
            Response.Cookies.Add(cokie);
            AddLone lone = new AddLone
            {
                Albums = db.Albums.ToList(),
                Members = db.Members.ToList()
            };

            return View(lone);
        }

        // POST: Lones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(int MemberId, int AlbumId)
        {
            HttpCookie cokie = new HttpCookie("Error");

            if (ModelState.IsValid)
            {

                Album album = db.Albums.Find(AlbumId);
                Member member = db.Members.Find(MemberId);
                int memberAge = Convert.ToInt32((DateTime.Today - member.DateOfBirth).TotalDays / 365);


                if (album.AgeContent & memberAge < 18)
                {
                    cokie["Error"] = "Age must be 18+ to view this content";
                    Response.Cookies.Add(cokie);

                    return RedirectToAction("Create");
                }
                if (album.NoOfStock < 1)
                {
                    cokie["Error"] = "Sorry! we dont have stock";
                    Response.Cookies.Add(cokie);

                    return RedirectToAction("Create");

                }
                var lone = (from lon in db.Lones
                            where
                            (lon.ReturnedDate == null
                                && lon.MemberId == MemberId)
                            select new
                            {
                                lon.DvdId,
                                lon.ReturnedDate,
                                lon.MemberId
                            }).ToList();


                int noOfDvdsMemberCanLone = member.Catagory.NoOfDvdRent;
                int noOfDaysMemberCanLone = member.Catagory.LoneDays;
                var count = lone.Count();

                if (noOfDvdsMemberCanLone <= lone.Count)
                {
                    cokie["Error"] = "Return Dvd first to take lone!";
                    Response.Cookies.Add(cokie);


                    return RedirectToAction("Create");
                }

                Dvd dvd =
                    db.Dvds.FirstOrDefault(X => X.AlbumId == AlbumId & X.OnStock);


                Lone newLone = new Lone
                {
                    DvdId = dvd.Id,
                    MemberId = member.Id,
                    LoneDate = DateTime.Today,
                    ReturnDate = DateTime.Today.AddDays(member.Catagory.LoneDays),
                };
                dvd.OnStock = false;
                album.NoOfStock -= album.NoOfStock;
                db.Lones.Add(newLone);
                db.SaveChanges();
                cokie["Success"] = "Sucessfully loned";
                Response.Cookies.Add(cokie);
                return RedirectToAction("Create");

            }

            cokie["Error"] = "Not Valid Input";
            Response.Cookies.Add(cokie);

            return RedirectToAction("Create");


        }

        // GET: Lones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lone lone = db.Lones.Find(id);
            if (lone == null)
            {
                return HttpNotFound();
            }
            ViewBag.DvdId = new SelectList(db.Dvds, "Id", "Id", lone.DvdId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", lone.MemberId);
            return View(lone);
        }

        // POST: Lones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,DvdId")] Lone lone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DvdId = new SelectList(db.Dvds, "Id", "Id", lone.DvdId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", lone.MemberId);
            return View(lone);
        }

        // GET: Lones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Albumdvd = db.Dvds.FirstOrDefault(X => X.Id == id);
            Albumdvd.OnStock = true;
            var lone = db.Lones.FirstOrDefault(X => X.DvdId == id);
            lone.ReturnedDate = DateTime.Today;
            if (DateTime.Today > lone.ReturnDate)
            {
                int daysMore = (DateTime.Today - (DateTime)lone.ReturnDate).Days;

                decimal finrPerday = lone.Members.Catagory.FinePerDays;
                lone.FineAmount = finrPerday * daysMore;
            }
            var album = db.Albums.FirstOrDefault(X => X.Id == lone.Dvds.AlbumId);
            album.NoOfStock += 1;
            db.SaveChanges();
            return Redirect("/Members");
        }

        // POST: Lones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Albumdvd = db.Dvds.FirstOrDefault(X => X.Id == id);
            Albumdvd.OnStock = true;
            var lone = db.Lones.FirstOrDefault(X => X.DvdId == id);
            lone.ReturnedDate = DateTime.Today;
            if (DateTime.Today > lone.ReturnDate)
            {
                int daysMore = (DateTime.Today - (DateTime)lone.ReturnDate).Days;

                decimal finrPerday = lone.Members.Catagory.FinePerDays;
                lone.FineAmount = finrPerday * daysMore;
            }
            var album = db.Albums.FirstOrDefault(X => X.Id == lone.Dvds.AlbumId);
            album.NoOfStock += 1;
            db.SaveChanges();
            return Redirect("/Members");
           
        }


        [Route("/{dvd:int?}/{member:int?}")]
        public ActionResult Return(int? dvd, int? member)
        {
            if (dvd == null & member == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Albumdvd = db.Dvds.FirstOrDefault(X => X.Id == dvd);
            Albumdvd.OnStock = true;
            var lone = db.Lones.FirstOrDefault(X => X.DvdId == dvd);
            lone.ReturnedDate = DateTime.Today;
            if (DateTime.Today > lone.ReturnDate)
            {
                int daysMore = (DateTime.Today - (DateTime)lone.ReturnDate).Days;

                decimal finrPerday = lone.Members.Catagory.FinePerDays;
                lone.FineAmount = finrPerday * daysMore;
            }
            var album = db.Albums.FirstOrDefault(X => X.Id == lone.Dvds.AlbumId);
            album.NoOfStock += 1;
            db.SaveChanges();
            return Redirect("/Members");
        }

        public ActionResult LatestLone()
        {
            var dayBefore = DateTime.Today.AddDays(-31);
            var lones = db.Lones.Where(X => X.LoneDate > dayBefore).ToList();

            return View(lones);
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
