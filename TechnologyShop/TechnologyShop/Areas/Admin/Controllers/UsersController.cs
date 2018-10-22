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
    public class UsersController : Controller
    {
        private NewShopEntities db = new NewShopEntities();

        // GET: Admin/Users
        public ActionResult Index()
        {
            var user_levels = db.UserLevels;
            var users = db.Users.Include(u => u.UserLevel);
            ViewBag.user_levels = user_levels.ToList();
            ViewBag.users = users.ToList();
            return PartialView();
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.UserLevelId = new SelectList(db.UserLevels, "Id", "UserLevelName");
            return PartialView();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = DateTime.Now.Ticks + "_" + pic.FileName.Split('/').Last();
                    user.CreatedDate = DateTime.Now;
                    user.ResetPasswordToken = "";
                    user.Password = MySecurity.EncryptPass(user.Password);

                    db.Users.Add(user);
                    db.SaveChanges();
                    return Content("OK");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }

            return PartialView(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LoginName,Password,FullName,Phone,Email,Address,Avatar,UserLevelId,CreatedDate,IsActive,ResetPasswordToken")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Content("OK");
            }
            return PartialView(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
