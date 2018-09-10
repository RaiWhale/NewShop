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

            ViewBag.products_hot = db.Products.OrderByDescending(x => x.OutputPrice).Take(5).ToList();
            
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
            return View(product);
        }
    }
}