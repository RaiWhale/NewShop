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

        public ActionResult BuyNow(int? id, string CustomerName, string Email, string Phone, byte Gender, string Address, decimal quantity)
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
  
            
            Customer cus;
            if (User.Identity.IsAuthenticated)
            {
                cus = db.Customers.Find(int.Parse(User.Identity.Name));
                cus.Gender = Gender;
            }
            else
            {
                cus = new Customer()
                {
                    CustomerName = CustomerName,
                    Email = Email,
                    Password = "", //tu nghien cuu sau khi nhau
                    Phone = Phone,
                    Gender = Gender,
                    Address = Address
                };
                db.Customers.Add(cus);
                db.SaveChanges();
            }

            //Create new order
            var order = new Order
            {
                OrderCode = MySecurity.RandomString(6),
                CustomerId = cus.Id,
                OrderDate = DateTime.Now,
                Discount = 0,
                Tax = 0,
                Status = 1 //pending
            };
            db.Orders.Add(order);
            db.SaveChanges();


            //add products to order details
            var ids = db.Products.Where(x => x.Id == id);

            foreach (var prod in ids)
            {
                OrderDetail od = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Unit = product.Unit,
                    Price = product.OutputPrice,
                    Discount = product.Discount,
                    Tax = 0,
                    Quantity = quantity,
                    Note = ""
                };
                db.OrderDetails.Add(od);
            }
            db.SaveChanges();


            return PartialView(product);
        }
    }
}