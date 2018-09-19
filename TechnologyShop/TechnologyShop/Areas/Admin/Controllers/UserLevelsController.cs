using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;

namespace TechnologyShop.Areas.Admin.Controllers
{
    [Authorize]
    public class UserLevelsController : Controller
    {
        private NewShopEntities db = new NewShopEntities();

        // GET: Admin/UserLevels
        public ActionResult Index()
        {
            var userLevels = db.UserLevels.Include(u => u.User);
            return View(userLevels.ToList());
        }

        // GET: Admin/UserLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLevel userLevel = db.UserLevels.Find(id);
            if (userLevel == null)
            {
                return HttpNotFound();
            }
            return View(userLevel);
        }

        // GET: Admin/UserLevels/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName");
            return View();
        }

        // POST: Admin/UserLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserLevelName,UserId")] UserLevel userLevel)
        {
            if (ModelState.IsValid)
            {
                db.UserLevels.Add(userLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName", userLevel.UserId);
            return View(userLevel);
        }

        // GET: Admin/UserLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLevel userLevel = db.UserLevels.Find(id);
            if (userLevel == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName", userLevel.UserId);
            return View(userLevel);
        }

        // POST: Admin/UserLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserLevelName,UserId")] UserLevel userLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName", userLevel.UserId);
            return View(userLevel);
        }

        // GET: Admin/UserLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLevel userLevel = db.UserLevels.Find(id);
            if (userLevel == null)
            {
                return HttpNotFound();
            }
            return View(userLevel);
        }

        // POST: Admin/UserLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserLevel userLevel = db.UserLevels.Find(id);
            db.UserLevels.Remove(userLevel);
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
