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
    public class InputsController : Controller
    {
        private NewShopEntities db = new NewShopEntities();

        // GET: Admin/Inputs
        public ActionResult Index()
        {
            var inputs = db.Inputs.Include(i => i.Supplier).Include(i => i.User);
            return PartialView(inputs.ToList());
        }

        // GET: Admin/Inputs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Input input = db.Inputs.Find(id);
            if (input == null)
            {
                return HttpNotFound();
            }
            return PartialView(input);
        }

        // GET: Admin/Inputs/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName");
            return PartialView();
        }

        // POST: Admin/Inputs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InputCode,InputDate,UserId,SupplierId,Tax,Status")] Input input)
        {
            if (ModelState.IsValid)
            {
                input.InputCode = MySecurity.RandomString(6);
                input.InputDate = DateTime.Now;

                db.Inputs.Add(input);
                db.SaveChanges();
                return Content("OK");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName", input.SupplierId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName", input.UserId);
            return PartialView(input);
        }

        // GET: Admin/Inputs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Input input = db.Inputs.Find(id);
            if (input == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName", input.SupplierId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName", input.UserId);
            return PartialView(input);
        }

        // POST: Admin/Inputs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InputCode,InputDate,UserId,SupplierId,Discount,Tax,Status")] Input input)
        {
            if (ModelState.IsValid)
            {
                db.Entry(input).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName", input.SupplierId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName", input.UserId);
            return PartialView(input);
        }

        // GET: Admin/Inputs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Input input = db.Inputs.Find(id);
            if (input == null)
            {
                return HttpNotFound();
            }
            return PartialView(input);
        }

        // POST: Admin/Inputs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Input input = db.Inputs.Find(id);
            db.Inputs.Remove(input);
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
