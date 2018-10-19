using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;

namespace TechnologyShop.Controllers
{
    public class HomeController : Controller
    {
        NewShopEntities db = new NewShopEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.products_hot = db.Products.OrderByDescending(x => x.OutputPrice).Take(5).ToList();
            var topic_items = db.Topics.ToList();
            ViewBag.topic_items = topic_items;

            ViewBag.products_old = db.Products.Where(x => x.Discount >= 50).Take(10).ToList();
            //var topic_id = topic_items.Select(x => x.Id).ToList();
            //ViewBag.products_topic = db.Products.Where(x => topic_id.Contains(x.Category.TopicId)).Take(5).ToList();
            return View();
        }
    }
}
