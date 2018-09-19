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
                ParentId = id != null ? id.Value : 0
            };
            
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "TopicName");
            return PartialView(category);
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName,ParentId,TopicId")] Category category)
        {
            if (ModelState.IsValid)
            {
     

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
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "TopicName");
            return PartialView(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName,ParentId,TopicId")] Category category)
        {
            if (ModelState.IsValid)
            {
                var topicname = db.Topics.Select(x => x.TopicName).ToList();
                category.Topic.TopicName = topicname.ToString();
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(category);
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
            var topicname = db.Topics.Select(x => x.TopicName).ToList();
            category.Topic.TopicName = topicname.ToString();
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "TopicName");
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
