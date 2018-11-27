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
    public class CategoriesController : Controller
    {
        private NewShopEntities db = new NewShopEntities();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            var categories = from u in db.Categories select(u);
            ViewBag.categories = categories.ToList();
            return View(db.Categories.ToList());
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "TopicName");
            return PartialView(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create(int? id)
        {
            var category = new Category()
            {
                TopicId = id != null ? id.Value : 0
            };
            
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "TopicName", id);
            return PartialView(category);
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName,TopicId")] Category category)
        {
            if (ModelState.IsValid)
            {

                category.UserId = int.Parse(User.Identity.Name);
                db.Categories.Add(category);
                db.SaveChanges();
                return Content("OK");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(int.Parse(User.Identity.Name));

            if (category.UserId != user.Id && !user.UserLevel.UserLevelName.Equals("Administrator"))
            {
                return PartialView("Error");
            }
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "TopicName", category.TopicId);
            return PartialView(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName,TopicId")] Category data)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(int.Parse(User.Identity.Name));


                var category = db.Categories.Find(data.Id);

                if (category.UserId != user.Id && !user.UserLevel.UserLevelName.Equals("Administrator"))
                {
                    return Content("Ban khong co quyen chinh sua");
                }
                category.CategoryName = data.CategoryName;
                category.TopicId = data.TopicId;
                //category.UserId = int.Parse(User.Identity.Name);///Truong hop admin edit thi lay admin gan vao
                db.SaveChanges();
                return Content("OK");
            }
            return PartialView(data);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
     
            return PartialView(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
