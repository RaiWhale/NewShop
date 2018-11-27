using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;
using PagedList;

namespace TechnologyShop.Controllers
{
    public class ProductsController : Controller
    {
        NewShopEntities db = new NewShopEntities();
        int pageSize = 5;
        // GET: Products
        public ActionResult Index(string cat, int? page, string search)
        {
            IQueryable<Product> products; //model chinh

            ViewBag.recent_products = db.Products.OrderByDescending(x => x.Id).Take(4).ToList();
            ViewBag.products_hot = db.Products.OrderByDescending(x => x.OutputPrice).Take(5).ToList();
            ViewBag.topics = db.Topics.ToList();


            //Filter by category
            var category = db.Categories.Where(x => x.CategoryName.Equals(cat)).FirstOrDefault();
            if (category != null)
            {
                ViewBag.CategoryName = category.CategoryName.ToUpper();
                products = db.Products.Where(x => x.CategoryId == category.Id).OrderByDescending(x => x.Id);// neu cat = null show san pham noi bat
            }
            else
            {
                ViewBag.CategoryName = "Tất cả sản phẩm";
                products = db.Products.OrderByDescending(x => x.Id);
            }

            //search
            if (search != null && search.Trim() != "")
            {
                products = products.Where(s => s.ProductName.ToLower().Contains(search.Trim().ToLower()));
            }
            
            ViewBag.search = search;
            ViewBag.cat = cat;

            //Paging
            int pageNumber = page ?? 1;

            return View(products.ToPagedList(pageNumber, pageSize));
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