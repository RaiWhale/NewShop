using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;

namespace TechnologyShop.Controllers
{
    public class ProductsController : Controller
    {
        NewShopEntities db = new NewShopEntities();
        // GET: Products
        public ActionResult Index(string cat)
        {
            var category = db.Categories.Where(x => x.CategoryName.Equals(cat)).FirstOrDefault();
            if (category != null)
            {
                ViewBag.CategoryName = category.CategoryName.ToUpper();
                ViewBag.products = db.Products.Where(x => x.CategoryId == category.Id).ToList();// neu cat = null show san pham noi bat
            }
            else
            {
                ViewBag.CategoryName = "Top Products";
                ViewBag.products = db.Products.OrderByDescending(x => x.Id).Take(20).ToList();
            }
            ViewBag.recent_products = db.Products.OrderByDescending(x => x.Id).Take(4).ToList();
            ViewBag.products_hot = db.Products.OrderByDescending(x => x.OutputPrice).Take(5).ToList();
            ViewBag.topics = db.Topics.ToList();

            return View();
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
            var product_detail = db.Products.Where(x => x.Id == id);
            ViewBag.product_detail = product_detail;
            

            var CID_Detail = product_detail.Select(x => x.CategoryId).SingleOrDefault();
            var related_products = db.Products.Where(x=>x.CategoryId ==CID_Detail).ToList();
            ViewBag.related_products = related_products;
            ViewBag.recent_products = db.Products.OrderByDescending(x => x.Id).Take(4).ToList();
         
     
            ViewBag.topics = db.Topics.ToList();
            ViewBag.categories = db.Categories.ToList();
         


            return View(product);
        }
    }
}