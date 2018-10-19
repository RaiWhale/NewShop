using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;

namespace TechnologyShop.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private NewShopEntities db = new NewShopEntities();
        System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();

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

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return PartialView();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BarCode,CategoryId,ProductName,Unit,InputPrice,OutputPrice,Discount,Description,IsActive")] Product product)
        {
            //dùng multiple thì khỏi khai bảo tham số
            if (ModelState.IsValid)
            {
                try
                {
                    db.Products.Add(product);
                    db.SaveChanges();

                    //Pictures
                    //try catch chỗ này để lỡ hình bị lỗi thì không bung
                    for (int i = 0; i < Request.Files.Count; i++)//nhớ for(i) không dùng foreach->ko chạy: thê mới quái
                    {
                        try
                        {
                            HttpPostedFileBase file = Request.Files[i];
                            string filename = DateTime.Now.Ticks + "_" + file.FileName.Split('/').Last();
                            Picture picture = new Picture()
                            {
                                Url = filename,
                                ProductId = product.Id
                            };
                            string path = Server.MapPath("~/Uploads/Picture") + "\\" + product.Id;
                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                            file.SaveAs(path + "\\" + filename);//lệnh này nếu ko lưu đc sẽ bung catch, nên ko add -> ok
                            db.Pictures.Add(picture);
                            //nếu save chỗ đây thì dư
                        }
                        catch { }
                    }
                    db.SaveChanges();//savechange 1 lần cho  tất cả các hình->đc hok? ok -> nếu ở trên ko add dc thì dưới ko save.
                    //xong 

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }

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
        public ActionResult Edit([Bind(Include = "Id,BarCode,CategoryId,ProductName,Unit,InputPrice,OutputPrice,Discount,Description,IsActive")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                //Pictures
                //try catch chỗ này để lỡ hình bị lỗi thì không bung
                for (int i = 0; i < Request.Files.Count; i++)//nhớ for(i) không dùng foreach->ko chạy: thê mới quái
                {
                    try
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        string filename = DateTime.Now.Ticks + "_" + file.FileName.Split('/').Last();
                        Picture picture = new Picture()
                        {
                            Url = filename,
                            ProductId = product.Id
                        };
                        string path = Server.MapPath("~/Uploads/Picture") + "\\" + product.Id;
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        file.SaveAs(path + "\\" + filename);//lệnh này nếu ko lưu đc sẽ bung catch, nên ko add -> ok
                        db.Pictures.Add(picture);
                        //nếu save chỗ đây thì dư
                    }
                    catch { }
                }
                db.SaveChanges();//savechange 1 lần cho  tất cả các hình->đc hok? ok -> nếu ở trên ko add dc thì dưới ko save.
                                 //xong 
                return RedirectToAction("Index");
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
