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


    public class MembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Members
        public ActionResult Index()
        {
            
            var members = db.Members.Include(m => m.Catagory).OrderBy(X=>X.FirstName);
            foreach(Member member in members)
            {
                member.Lones = db.Lones.Where(X => X.MemberId == member.Id);
            }
            return View(members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lones = db.Lones.Where(X => X.MemberId == id && DateTime.Compare(X.LoneDate,DateTime.Today)<=31).ToList();
            Member member = db.Members.Find(id);
            member.Lones = lones;


            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.CatagoryId = new SelectList(db.MemberCatagories, "Id", "Name");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CatagoryId,DateOfBirth,FirstName,LastName,Email,Address,Gender")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatagoryId = new SelectList(db.MemberCatagories, "Id", "Name", member.CatagoryId);
            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatagoryId = new SelectList(db.MemberCatagories, "Id", "Name", member.CatagoryId);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CatagoryId,DateOfBirth,FirstName,LastName,Email,Address,Gender")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatagoryId = new SelectList(db.MemberCatagories, "Id", "Name", member.CatagoryId);
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NotBorrowed()
        {
            List<Member> members = db.Members.ToList();
            foreach(Member member in members.ToList())
            {
                var before = DateTime.Today.AddDays(-31);
                var lones = db.Lones.Where(X => X.MemberId == member.Id && DateTime.Compare(before,X.LoneDate)<0).ToList();
                if(lones.Count != 0)
                {
                    members.Remove(member);
                }
            }

            return View(members);
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
