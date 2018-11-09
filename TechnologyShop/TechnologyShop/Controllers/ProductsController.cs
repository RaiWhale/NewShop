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
        public ActionResult Index(string cat,int? page, string sort, string search)
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

                        int pageNumber = page ?? 1;
            var products = db.Products.AsQueryable();
            if (search != null && search.Trim() != "")
            {
                products = products.Where(s => s.ProductName.ToLower().Contains(search.Trim().ToLower()));
            }
            if (string.IsNullOrEmpty(sort))
            {
                sort = "id_asc";
            }
            ViewBag.SortName = "name_asc";
            switch (sort)
            {
                case "id_asc":
                    products = products.OrderBy(x => x.Id);
                    ViewBag.SortId = "id_desc";// Dùng cho lần bấm tiếp theo
                    break;
                case "id_desc":
                    products = products.OrderByDescending(x => x.Id);
                    ViewBag.SortId = "id_asc"; //Dùng cho lần bấm tiếp theo
                    break;
                case "name_asc":
                    products = products.OrderBy(x => x.ProductName);
                    ViewBag.SortId = "name_desc";// Dùng cho lần bấm tiếp theo
                    break;
                case "name_desc":
                    products = products.OrderByDescending(x => x.ProductName);
                    ViewBag.SortId = "name_asc"; //Dùng cho lần bấm tiếp theo
                    break;
            }
            ViewBag.CurrentSort = sort; //biến hiện hành giữ nguyên sắp xếp
            ViewBag.CurrentSearch = search;


            return View(products.ToPagedList(pageNumber,pageSize));
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