using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;
using TechnologyShop.Models.ViewModel;

namespace TechnologyShop.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        NewShopEntities db = new NewShopEntities();
        // GET: Admin/Dashboard
        public ActionResult Index(DateTime? tu_ngay, DateTime? den_ngay)
        {
            int user_id = int.Parse(User.Identity.Name);
            if (tu_ngay == null)
                tu_ngay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
            if (den_ngay == null)
                den_ngay = DateTime.Now;


            //Table Orders + OrderDetails + Sum order
            var orders_daily = db.Orders.Where(x => x.OrderDate >= tu_ngay && x.OrderDate <= den_ngay && x.User.Id == user_id)
            .Select(x => new OrderReportVM()
            {
                OrderId = x.Id,
                OrderCode = x.OrderCode,
                OrderDate = x.OrderDate,
                Employee = x.User.FullName,
                Customer = x.Customer.CustomerName,
                Total = x.OrderDetails.Count > 0 ? x.OrderDetails.Sum(y => y.Quantity * y.Price) : 0
            }).ToList();
            var sumtotal = orders_daily.Count() > 0 ? orders_daily.Sum(x => x.Total) : 0;

            var ids_daily = orders_daily.Select(x => x.OrderId).ToList();
            //Quantities daily
            var quantities_daily = db.OrderDetails.Where(x => ids_daily.Contains(x.OrderId)).GroupBy(x => new
            {
                x.ProductId,
                x.Product.ProductName,
                x.Order.OrderCode
            })
        .Select(y => new QuantityReportVM()
        {
            ProductId = y.Key.ProductId,
            ProductName = y.Key.ProductName,
            OrderCode = y.Key.OrderCode,
            SumQuantity = y.Sum(x => x.Quantity),
            SumTotal = y.Sum(x => x.Quantity * x.Price)
        }).ToList();


            ViewBag.latest_orders_daily = orders_daily.OrderByDescending(x => x.OrderDate).Take(10).ToList();
            ViewBag.quantities_daily = quantities_daily.OrderByDescending(x => x.OrderCode).ToList();
            ViewBag.sumtotal = sumtotal;

            return View();
        }
    }
}