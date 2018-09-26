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
            int user_id = int.Parse(User.Identity.Name);
            var user_levels = db.UserLevels;
            var userLevels = db.UserLevels.Include(u => u.Users);
            return PartialView(user_levels);
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
            return PartialView(userLevel);
        }

        // GET: Admin/UserLevels/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName");
            return PartialView();
        }

        // POST: Admin/UserLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserLevel userLevel)
        {
            if (ModelState.IsValid)
            {
                db.UserLevels.Add(userLevel);
                db.SaveChanges();
                return Content("OK");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName", userLevel.UserId);
            return PartialView(userLevel);
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
            return PartialView(userLevel);
        }

        // POST: Admin/UserLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserLevel data)
        {
            var userLevel = db.UserLevels.Find(data.Id);
            if (ModelState.IsValid)
            {
                userLevel.UserLevelName = data.UserLevelName;
                db.SaveChanges();
                return Content("OK");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName", userLevel.UserId);
            return PartialView(userLevel);
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
            return PartialView(userLevel);
        }

        // POST: Admin/UserLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserLevel userLevel = db.UserLevels.Find(id);
            db.UserLevels.Remove(userLevel);
            db.SaveChanges();
            return Content("OK");
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
