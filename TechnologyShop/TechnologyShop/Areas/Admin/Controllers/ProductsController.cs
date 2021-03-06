﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Areas.Admin.Models.ViewModel;
using TechnologyShop.Models;

namespace TechnologyShop.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private NewShopEntities db = new NewShopEntities();


        // GET: Admin/Products
        public ActionResult Index()
        {
            int user_id = int.Parse(User.Identity.Name);
         
        //var products = db.Products.Include(p => p.Category);
        ViewBag.topics = db.Topics.ToList();
            return View();
        }



        public ActionResult Category(int? cat)
        {
            int user_id = int.Parse(User.Identity.Name);
            var products = from p in db.Products select (p);
            if (cat > 0)
            {
                products = products.Where(x => x.CategoryId == cat);
                ViewBag.cat = cat;
            }
            return PartialView(products.ToList());
        }
        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView(product);
        }
        // GET: Admin/Products/UploadFile/5
        public ActionResult UploadFile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView();
        }
  
     
        // GET: Admin/Products/Create
        public ActionResult Create( )
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return PartialView();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Product product)
        {
            //dùng multiple thì khỏi khai bảo tham số
            if (ModelState.IsValid)
            {

                product.BarCode = MySecurity.RandomString(6);
                product.IsActive = true;
                db.Products.Add(product);
                db.SaveChanges();
                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)//nhớ for(i) không dùng foreach->ko chạy: thê mới quái
                    {

                        HttpPostedFileBase file = Request.Files[i];

                        string filename = DateTime.Now.Ticks + "_" + file.FileName.Split('/').Last();

                        string path = Server.MapPath("~/Uploads/Pictures") + "\\" + product.Id;
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                        file.SaveAs(path + "\\" + filename);

                        Picture p = new Picture()
                        {
                            ProductId = product.Id,
                            Url = filename
                        };

                        db.Pictures.Add(p);

                    }
                }
                catch { }
                db.SaveChanges();

                return Content("OK");


            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", product.CategoryId);
            return PartialView(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", product.CategoryId);
            return PartialView(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BarCode,CategoryId,ProductName,Unit,InputPrice,OutputPrice,PictureId,Discount,Description,IsActive")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)//nhớ for(i) không dùng foreach->ko chạy: thê mới quái
                    {

                        HttpPostedFileBase file = Request.Files[i];
                        if (!string.IsNullOrEmpty(file.FileName))
                        {
                            string filename = DateTime.Now.Ticks + "_" + file.FileName.Split('/').Last();

                            string path = Server.MapPath("~/Uploads/Pictures") + "\\" + product.Id;
                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                            file.SaveAs(path + "\\" + filename);

                            Picture p = new Picture()
                            {
                                ProductId = product.Id,
                                Url = filename
                            };

                            db.Pictures.Add(p);
                        }
                    }
                }
                catch { }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return Content("OK");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", product.CategoryId);
            return PartialView(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        [HttpPost]
        public ActionResult RemovePicture(int picid)
        {
            //...
            var pic = db.Pictures.Find(picid);
            if(pic != null){
         

                string path = Server.MapPath("~/Uploads/Pictures") + "\\" + pic.Product.Id;
           

                if (System.IO.File.Exists(path + "\\" + pic.Url))
                {
                    System.IO.File.Delete(path + "\\" + pic.Url);

                }
                db.Pictures.Remove(pic);
                db.SaveChanges();

                return Content("OK");
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult SetPictureDefault(int picid)
        {
            //...
            var pic = db.Pictures.Find(picid);
            if (pic != null)
            {
                var product = db.Products.Find(pic.ProductId);
                product.PictureId = pic.Id; // hinh dai dien
                db.SaveChanges();
                return Content("OK");
            }
            return HttpNotFound();
        }
    }
}
