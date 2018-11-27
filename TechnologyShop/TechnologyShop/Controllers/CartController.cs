using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;
using TechnologyShop.Models.ViewModel;

namespace TechnologyShop.Controllers
{
    public class CartController : Controller
    {
        NewShopEntities db = new NewShopEntities();

        // GET: Cart
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var cus = db.Customers.Find(int.Parse(User.Identity.Name));
                ViewBag.customer = AutoMapper.Mapper.Map<UpdateProfileVM>(cus);

            
            }
            return View();
        }

    

        [HttpPost]
        public ActionResult CheckOut(string CustomerName, string Email, string Phone, byte Gender, string Address, string cartlist)
        {
            //xu ly cartlist
            List<CartItemVM> cart_items = JsonConvert.DeserializeObject <List<CartItemVM>>(cartlist);
            if (cartlist.Count() == 0) return Content("Empty");

            //xu ly customer
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
            var ids = cart_items.Select(x => x.productid).ToList();

            foreach (var product in db.Products.Where(x => ids.Contains(x.Id)))
            {
                OrderDetail od = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Unit = product.Unit,
                    Price = product.OutputPrice,
                    Discount = product.Discount,
                    Tax = 0,
                    Quantity = cart_items.Where(x=>x.productid == product.Id).SingleOrDefault().quantity,
                    Note = ""
                };
                db.OrderDetails.Add(od);
            }
            db.SaveChanges();
            return Content("OK");
        }

        public ActionResult OrderComplete()
        {
            var cus = db.Customers.Find(int.Parse(User.Identity.Name));
            var order = db.Orders.Where(x => x.CustomerId == cus.Id);

            ViewBag.order = order;

            return View();
        }

    }
}